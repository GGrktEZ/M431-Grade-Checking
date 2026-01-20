using DataAccess.Model;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Service;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthService> _logger;
    private readonly IEmailService _emailService;

    public AuthService(
        IAuthRepository authRepository,
        IConfiguration config,
        ILogger<AuthService> logger,
        IEmailService emailService)
    {
        _authRepository = authRepository;
        _config = config;
        _logger = logger;
        _emailService = emailService;
    }

    // Bestehend: Direkt-Login (JWT sofort)
    public LoginResponseDto? VerifyEmail(LoginRequestDto loginReq)
    {
        string email = (loginReq.email ?? "").Trim().ToLowerInvariant();

        var teacher = _authRepository.GetTeacherByEmail(email);

        if (teacher == null)
            return null;

        if (string.IsNullOrWhiteSpace(teacher.password_hash))
            return null;

        if (!PasswordHasher.VerifyPassword(loginReq.password, teacher.password_hash))
            return null;

        string token = GenerateJwtToken(teacher);

        return new LoginResponseDto
        {
            Token = token,
            teacher_id = teacher.teacher_id
        };
    }

    // Bestehend: Registration
    public async Task<RegisterTeacherResponseDto> RegisterTeacherAsync(RegisterTeacherRequestDto req)
    {
        // 1) Teacher muss existieren
        teachers? teacher = _authRepository.GetTeacherByEmail(req.email);
        if (teacher == null)
        {
            return new RegisterTeacherResponseDto
            {
                success = false,
                message = "E-Mail ist nicht als Lehrperson erfasst."
            };
        }

        // 2) Bereits registriert?
        if (!string.IsNullOrEmpty(teacher.password_hash))
        {
            return new RegisterTeacherResponseDto
            {
                success = false,
                message = "Diese Lehrperson ist bereits registriert."
            };
        }

        // 3) Passwort hashen (du nutzt bereits Argon2)
        string pwHash = PasswordHasher.HashPassword(req.password);

        // 4) Token erstellen + Hash speichern
        string rawToken = RegistrationToken.CreateRawToken();
        string tokenHash = RegistrationToken.HashToken(rawToken);

        DateTime nowUtc = DateTime.UtcNow;
        DateTime expiresUtc = nowUtc.AddYears(1); // 1 Jahr gueltig

        bool updated = _authRepository.TryRegisterExistingTeacher(
            req.email, pwHash, tokenHash, expiresUtc, nowUtc);

        if (!updated)
        {
            return new RegisterTeacherResponseDto
            {
                success = false,
                message = "Registrierung nicht moeglich."
            };
        }

        // 5) Bestaetigungslink bauen (kommt aus appsettings)
        string baseUrl = _config["Registration:ConfirmUrlBase"] ?? "";
        string link = $"{baseUrl}?email={Uri.EscapeDataString(req.email)}&token={Uri.EscapeDataString(rawToken)}";

        string subject = "E-Mail bestaetigen";
        string body =
            "Bitte bestaetige deine E-Mail-Adresse mit folgendem Link:\n\n" +
            link + "\n\n" +
            "Der Link ist 1 Jahr gueltig.";

        await _emailService.SendAsync(req.email, subject, body);

        return new RegisterTeacherResponseDto
        {
            success = true,
            message = "Registrierung erfolgreich. Bitte E-Mail bestaetigen."
        };
    }

    // =========================
    // NEU: 2FA Login per Mail
    // =========================

    // Schritt 1: Passwort pruefen, danach Mail mit Login-Link senden
    public async Task<StartLoginResponseDto> StartEmail2FaLoginAsync(LoginRequestDto loginReq)
    {
        try
        {
            string email = (loginReq.email ?? "").Trim().ToLowerInvariant();

            var teacher = _authRepository.GetTeacherByEmail(email);
            if (teacher == null)
            {
                return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };
            }

            // Nur aktivierte Accounts duerfen 2FA Login starten
            if (!teacher.email_confirmed)
            {
                return new StartLoginResponseDto { success = false, message = "E-Mail ist nicht bestaetigt." };
            }

            if (string.IsNullOrWhiteSpace(teacher.password_hash))
            {
                return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };
            }

            if (!PasswordHasher.VerifyPassword(loginReq.password, teacher.password_hash))
            {
                return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };
            }

            // One-time Login Token erstellen
            string rawToken = LoginToken.CreateRawToken();
            string tokenHash = LoginToken.HashToken(rawToken);
            DateTime expiresUtc = DateTime.UtcNow.AddMinutes(10);

            // Token speichern
            bool ok = _authRepository.SetLoginToken(email, tokenHash, expiresUtc);
            if (!ok)
            {
                return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };
            }

            // Link bauen (aus appsettings)
            // z.B. https://localhost:7297/api/auth/confirm-login
            string baseUrl = _config["Auth:ConfirmLoginUrlBase"] ?? "";
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return new StartLoginResponseDto
                {
                    success = false,
                    message = "Server ist nicht korrekt konfiguriert (ConfirmLoginUrlBase fehlt)."
                };
            }

            string link =
                $"{baseUrl}?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(rawToken)}";

            await _emailService.SendAsync(
                email,
                "Login bestaetigen",
                "Bitte bestaetige deinen Login ueber folgenden Link (10 Minuten gueltig):\n\n" +
                link + "\n\n" +
                "Wenn du das nicht warst, ignoriere diese Mail."
            );

            return new StartLoginResponseDto
            {
                success = true,
                message = "Bestaetigungs-Mail wurde gesendet. Bitte Link klicken."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "StartEmail2FaLoginAsync failed");
            return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };
        }
    }

    // Schritt 2: Link klicken, Token pruefen, Token loeschen, JWT ausgeben
    public Task<LoginResponseDto?> ConfirmEmail2FaLoginAsync(string emailRaw, string tokenRaw)
    {
        try
        {
            string email = (emailRaw ?? "").Trim().ToLowerInvariant();
            string tokenHash = LoginToken.HashToken(tokenRaw ?? "");

            var teacher = _authRepository.GetTeacherByEmailAndLoginTokenHash(email, tokenHash);
            if (teacher == null)
                return Task.FromResult<LoginResponseDto?>(null);

            if (teacher.login_token_expires_at == null || teacher.login_token_expires_at < DateTime.UtcNow)
                return Task.FromResult<LoginResponseDto?>(null);

            // One-time: Token loeschen
            _authRepository.ClearLoginToken(email);

            // JWT erstellen
            string jwt = GenerateJwtToken(teacher);

            return Task.FromResult<LoginResponseDto?>(new LoginResponseDto
            {
                Token = jwt,
                teacher_id = teacher.teacher_id
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ConfirmEmail2FaLoginAsync failed");
            return Task.FromResult<LoginResponseDto?>(null);
        }
    }

    private string GenerateJwtToken(teachers teacher)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, teacher.teacher_id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, teacher.email),
            new Claim(JwtRegisteredClaimNames.GivenName, teacher.first_name),
            new Claim(JwtRegisteredClaimNames.FamilyName, teacher.last_name)
        };

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public async Task<StartLoginResponseDto> StartEmailCodeLoginAsync(StartLoginRequestDto req)
    {
        try
        {
            string email = (req.email ?? "").Trim().ToLowerInvariant();

            var teacher = _authRepository.GetTeacherByEmail(email);
            if (teacher == null)
                return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };

            if (!teacher.email_confirmed)
                return new StartLoginResponseDto { success = false, message = "E-Mail ist nicht bestaetigt." };

            if (string.IsNullOrWhiteSpace(teacher.password_hash))
                return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };

            if (!PasswordHasher.VerifyPassword(req.password, teacher.password_hash))
                return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };

            string code = LoginCode.Generate6DigitCode();
            string codeHash = LoginCode.Hash(code);
            DateTime expiresUtc = DateTime.UtcNow.AddMinutes(10);

            bool ok = _authRepository.SetLoginToken(email, codeHash, expiresUtc);
            if (!ok)
                return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };

            await _emailService.SendAsync(
                email,
                "Login-Code",
                $"Dein Login-Code lautet: {code}\n\nGueltig fuer 10 Minuten."
            );

            return new StartLoginResponseDto { success = true, message = "Code wurde gesendet. Bitte eingeben." };
        }
        catch
        {
            return new StartLoginResponseDto { success = false, message = "Login fehlgeschlagen." };
        }
    }

    public async Task<LoginResponseDto?> ConfirmEmailCodeLoginAsync(ConfirmLoginRequestDto req)
    {
        try
        {
            string email = (req.email ?? "").Trim().ToLowerInvariant();
            string code = (req.code ?? "").Trim();

            if (code.Length != 6)
                return null;

            string codeHash = LoginCode.Hash(code);

            var teacher = _authRepository.GetTeacherByEmailAndLoginTokenHash(email, codeHash);
            if (teacher == null)
                return null;

            if (teacher.login_token_expires_at == null || teacher.login_token_expires_at < DateTime.UtcNow)
                return null;

            _authRepository.ClearLoginToken(email);

            string jwt = GenerateJwtToken(teacher);

            return new LoginResponseDto
            {
                Token = jwt,
                teacher_id = teacher.teacher_id
            };
        }
        catch
        {
            return null;
        }
    }

}
