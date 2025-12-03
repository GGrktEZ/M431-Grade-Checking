using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Service;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class teachersController : ControllerBase
{
    private readonly IteachersService _teachersService;

    /// <summary>
    /// Constructor to enable the dependency injection of <see cref="teachersService"/>.
    /// </summary>
    /// <param name="teachersService">The reference to the teachersService.</param>
    public teachersController(IteachersService teachersService)
    {
        _teachersService = teachersService;
    }
    [HttpGet("{id}")]
    public ActionResult<teachersDto?> GetteachersById(int id)
    {
        teachersDto? teachersDto = _teachersService.GetteachersById(id);
        return teachersDto == null ? NotFound() : Ok(teachersDto);
    }
    [HttpGet]
    public ActionResult<IEnumerable<teachersDto>> GetAllteacherss()
    {
        return Ok(_teachersService.GetAllteacherss());
    }
    [HttpPost]
    public ActionResult Addteachers(CreateteachersDto teachers)
    {
        int id = _teachersService.Addteachers(teachers);
        return CreatedAtAction(nameof(GetteachersById), new { id }, id);
    }
    [HttpPut]
    public ActionResult Updateteachers(int id, UpdateteachersDto teachers)
    {
        return _teachersService.Updateteachers(id, teachers) ? NoContent() : NotFound();
    }
    [HttpDelete]
    public ActionResult Deleteteachers(int id)
    {
        return _teachersService.Deleteteachers(id) ? NoContent() : NotFound();
    }

}