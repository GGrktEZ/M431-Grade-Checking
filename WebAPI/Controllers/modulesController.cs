using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Shared.DTOs;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class modulesController : ControllerBase
{
    private readonly AppDbContext _db;

    public modulesController(AppDbContext db)
    {
        _db = db;
    }

    // GET api/modules/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ModuleDto>> GetById(int id)
    {
        var m = await _db.modules.AsNoTracking()
            .Where(x => x.module_id == id)
            .Select(x => new ModuleDto
            {
                module_id = x.module_id,
                module_code = x.module_code,
                module_name = x.module_name,
                description = x.description
            })
            .FirstOrDefaultAsync();

        return m is null ? NotFound() : Ok(m);
    }
}
