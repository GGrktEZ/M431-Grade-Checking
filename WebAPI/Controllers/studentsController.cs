using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Services.Service;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using System.Linq;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class studentsController : ControllerBase
{
    private readonly IstudentsService _studentsService;
    private readonly AppDbContext _db;

    public studentsController(IstudentsService studentsService, AppDbContext db)
    {
        _studentsService = studentsService;
        _db = db;
    }

    // GET api/students?classId=2  ? nur Schüler dieser Klasse
    // GET api/students            ? alle Schüler (bestehendes Verhalten)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<studentsDto>>> GetAllOrByClass([FromQuery] int? classId)
    {
        if (classId is null)
        {
            // WICHTIG: richtiger Servicename -> GetAllstudentss()
            var all = _studentsService.GetAllstudentss();
            return Ok(all);
        }

        bool exists = await _db.classes.AnyAsync(c => c.class_id == classId.Value);
        if (!exists) return NotFound($"Class {classId} not found");

        var list = await _db.class_enrollments
            .Where(e => e.class_id == classId.Value)
            .Join(_db.students,
                  e => e.student_id,
                  s => s.student_id,
                  (e, s) => new studentsDto
                  {
                      student_id = s.student_id,
                      first_name = s.first_name,
                      last_name = s.last_name,
                      email = s.email
                  })
            .OrderBy(x => x.last_name).ThenBy(x => x.first_name)
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public ActionResult<studentsDto?> GetstudentsById(int id)
    {
        studentsDto? studentsDto = _studentsService.GetstudentsById(id);
        return studentsDto == null ? NotFound() : Ok(studentsDto);
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
