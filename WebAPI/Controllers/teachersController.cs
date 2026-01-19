using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Shared.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class teachersController : ControllerBase
    {
        private readonly string _connectionString;

        public teachersController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        // ===============================
        // MODULE DER LEHRPERSON
        // GET api/teachers/{id}/modules
        // ===============================
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

        // ==================================
        // PROREKTOREN DER LEHRPERSON
        // GET api/teachers/{id}/prorectors
        // ==================================
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
