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

    // ... dein bestehendes VerifyEmail + GenerateJwtToken bleibt unverändert ...
    public LoginResponseDto? VerifyEmail(LoginRequestDto loginReq)
    {
        var teacher = _authRepository.GetTeacherByEmail(loginReq.email);

        if (teacher == null)
            return null;

        if (string.IsNullOrWhiteSpace(teacher.password_hash))
            return null;

        if (!PasswordHasher.VerifyPassword(loginReq.password, teacher.password_hash))
            return null;

        string token = GenerateJwtToken(teacher);

        return new LoginResponseDto { Token = token };
    }

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

        // 5) Bestätigungslink bauen (kommt aus appsettings)
        // Beispiel: https://localhost:7051/confirm?email=...&token=...
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

    private string GenerateJwtToken(teachers teacher)
    {
        // unverändert
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
}
