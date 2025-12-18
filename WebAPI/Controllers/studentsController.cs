using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Services.Service;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class studentsController : ControllerBase
{
    private readonly IstudentsService _studentsService;

    /// <summary>
    /// Constructor to enable the dependency injection of <see cref="studentsService"/>.
    /// </summary>
    /// <param name="studentsService">The reference to the studentsService.</param>
    public studentsController(IstudentsService studentsService)
    {
        _studentsService = studentsService;
    }
    [HttpGet("{id}")]
    public ActionResult<studentsDto?> GetstudentsById(int id)
    {
        studentsDto? studentsDto = _studentsService.GetstudentsById(id);
        return studentsDto == null ? NotFound() : Ok(studentsDto);
    }
    [HttpGet]
    public ActionResult<IEnumerable<studentsDto>> GetAllstudentss()
    {
        return Ok(_studentsService.GetAllstudentss());
    }
    [HttpPost]
    public ActionResult Addstudents(CreatestudentsDto students)
    {
        int id = _studentsService.Addstudents(students);
        return CreatedAtAction(nameof(GetstudentsById), new { id }, id);
    }
    [HttpPut]
    public ActionResult Updatestudents(int id, UpdatestudentsDto students)
    {
        return _studentsService.Updatestudents(id, students) ? NoContent() : NotFound();
    }
    [HttpDelete]
    public ActionResult Deletestudents(int id)
    {
        return _studentsService.Deletestudents(id) ? NoContent() : NotFound();
    }

}