using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Shared.DTOs;
using Services.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class teachersController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public teachersController(IConfiguration config, IEmailService emailService)
        {
            _config = config;
            _connectionString = config.GetConnectionString("DefaultConnection")!;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterTeacher([FromBody] RegisterExistingTeacherDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Ungueltige Eingaben.");

            if (!string.Equals(dto.email?.Trim(), dto.email_confirm?.Trim(), StringComparison.OrdinalIgnoreCase))
                return BadRequest("Email-Adressen stimmen nicht ueberein.");

            if (!string.Equals(dto.password, dto.password_confirm, StringComparison.Ordinal))
                return BadRequest("Passwoerter stimmen nicht ueberein.");

            string email = dto.email.Trim().ToLowerInvariant();

            string rawToken = RegistrationToken.CreateRawToken();
            string tokenHash = RegistrationToken.HashToken(rawToken);

            string passwordHash = PasswordHasher.HashPassword(dto.password);

            DateTime nowUtc = DateTime.UtcNow;
            DateTime expiresUtc = nowUtc.AddYears(1);

            // WICHTIG: password_hash darf bei email_confirmed = 0 ueberschrieben werden,
            // sonst "haengt" eine kaputte Erstregistrierung fest.
            const string sql = @"
UPDATE teachers
SET
    password_hash = @password_hash,
    email_confirmed = 0,
    email_confirmation_token_hash = @token_hash,
    email_confirmation_expires_at = @expires_at,
    registration_requested_at = @requested_at,
    email_confirmed_at = NULL
WHERE
    email = @email
    AND (
        password_hash IS NULL
        OR email_confirmed = 0
    );
";

            int rows;

            await using (var conn = new MySqlConnector.MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                await using var cmd = new MySqlConnector.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@password_hash", passwordHash);
                cmd.Parameters.AddWithValue("@token_hash", tokenHash);
                cmd.Parameters.AddWithValue("@expires_at", expiresUtc);
                cmd.Parameters.AddWithValue("@requested_at", nowUtc);
                cmd.Parameters.AddWithValue("@email", email);

                rows = await cmd.ExecuteNonQueryAsync();
            }

            if (rows == 0)
                return BadRequest("Registrierung nicht moeglich (Benutzer existiert nicht oder ist bereits bestaetigt).");

            string baseUrl = _config["Registration:ConfirmUrlBase"] ?? "";
            if (string.IsNullOrWhiteSpace(baseUrl))
                return StatusCode(500, "ConfirmUrlBase ist nicht konfiguriert.");

            string link = $"{baseUrl}?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(rawToken)}";

            try
            {
                await _emailService.SendAsync(
                    email,
                    "E-Mail bestaetigen",
                    $"Bitte bestaetige deine E-Mail-Adresse mit folgendem Link:\n\n{link}\n\nDer Link ist 1 Jahr gueltig."
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Registrierung gespeichert, aber E-Mail Versand fehlgeschlagen: {ex.Message}");
            }

            return Ok("Registrierung erfolgreich. Bitte E-Mail bestaetigen.");
        }


        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
                return BadRequest("Ungueltige Anfrage.");

            string tokenHash = RegistrationToken.HashToken(token);
            DateTime nowUtc = DateTime.UtcNow;

            const string sql = @"
UPDATE teachers
SET
    email_confirmed = 1,
    email_confirmed_at = @confirmed_at,
    email_confirmation_token_hash = NULL,
    email_confirmation_expires_at = NULL
WHERE
    email = @email
    AND email_confirmed = 0
    AND email_confirmation_token_hash = @token_hash
    AND email_confirmation_expires_at IS NOT NULL
    AND email_confirmation_expires_at >= @now;
";

            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@confirmed_at", nowUtc);
            cmd.Parameters.AddWithValue("@email", email.Trim().ToLowerInvariant());
            cmd.Parameters.AddWithValue("@token_hash", tokenHash);
            cmd.Parameters.AddWithValue("@now", nowUtc);

            int rows = await cmd.ExecuteNonQueryAsync();

            if (rows == 0)
                return BadRequest("Token ungueltig oder abgelaufen.");

            return Ok("E-Mail erfolgreich bestaetigt.");
        }

        [HttpGet("{teacherId}/modules")]
        public async Task<ActionResult<List<ModuleDto>>> GetModulesForTeacher(int teacherId)
        {
            var result = new List<ModuleDto>();

            const string sql = @"
SELECT m.module_id, m.module_code, m.module_name, m.description
FROM teacher_modules tm
JOIN modules m ON m.module_id = tm.module_id
WHERE tm.teacher_id = @teacherId
ORDER BY m.module_code;
";

            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@teacherId", teacherId);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new ModuleDto
                {
                    module_id = reader.GetInt32("module_id"),
                    module_code = reader.GetString("module_code"),
                    module_name = reader.GetString("module_name"),
                    description = reader.IsDBNull(reader.GetOrdinal("description"))
                        ? null
                        : reader.GetString("description")
                });
            }

            return Ok(result);
        }

        [HttpGet("{teacherId}/prorectors")]
        public async Task<ActionResult<List<prorectorsDto>>> GetProrectorsForTeacher(int teacherId)
        {
            var result = new List<prorectorsDto>();

            const string sql = @"
SELECT DISTINCT
    p.prorector_id,
    p.first_name,
    p.last_name,
    p.email,
    p.department_id_1,
    p.department_id_2
FROM teacher_prorectors tp
JOIN prorectors p
  ON p.prorector_id = tp.prorector_id_1
  OR p.prorector_id = tp.prorector_id_2
WHERE tp.teacher_id = @teacherId;
";

            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@teacherId", teacherId);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new prorectorsDto
                {
                    prorector_id = reader.GetInt32("prorector_id"),
                    first_name = reader.GetString("first_name"),
                    last_name = reader.GetString("last_name"),
                    email = reader.GetString("email"),
                    department_id_1 = reader.GetInt32("department_id_1"),
                    department_id_2 = reader.IsDBNull(reader.GetOrdinal("department_id_2"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("department_id_2"))
                });
            }

            return Ok(result);
        }
    }
}
