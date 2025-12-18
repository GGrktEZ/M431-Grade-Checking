using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Services.Service;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class gradesController : ControllerBase
{
    private readonly IgradesService _gradesService;

    /// <summary>
    /// Constructor to enable the dependency injection of <see cref="gradesService"/>.
    /// </summary>
    /// <param name="gradesService">The reference to the gradesService.</param>
    public gradesController(IgradesService gradesService)
    {
        _gradesService = gradesService;
    }
    [HttpGet("{id}")]
    public ActionResult<gradesDto?> GetgradesById(int id)
    {
        gradesDto? gradesDto = _gradesService.GetgradesById(id);
        return gradesDto == null ? NotFound() : Ok(gradesDto);
    }
    [HttpGet]
    public ActionResult<IEnumerable<gradesDto>> GetAllgradess()
    {
        return Ok(_gradesService.GetAllgradess());
    }
    [HttpPost]
    public ActionResult Addgrades(CreategradesDto grades)
    {
        int id = _gradesService.Addgrades(grades);
        return CreatedAtAction(nameof(GetgradesById), new { id }, id);
    }
    [HttpPut]
    public ActionResult Updategrades(int id, UpdategradesDto grades)
    {
        return _gradesService.Updategrades(id, grades) ? NoContent() : NotFound();
    }
    [HttpDelete]
    public ActionResult Deletegrades(int id)
    {
        return _gradesService.Deletegrades(id) ? NoContent() : NotFound();
    }

}