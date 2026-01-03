using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Services.Service;
using Microsoft.EntityFrameworkCore;
using DataAccess;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class classesController : ControllerBase
{
    private readonly IclassesService _classesService;

    /// <summary>
    /// Constructor to enable the dependency injection of <see cref="classesService"/>.
    /// </summary>
    /// <param name="classesService">The reference to the classesService.</param>
    public classesController(IclassesService classesService)
    {
        _classesService = classesService;
    }

    // ================= Bestehende CRUD-Endpunkte =================

    [HttpGet("{id}")]
    public ActionResult<classesDto?> GetclassesById(int id)
    {
        classesDto? classesDto = _classesService.GetclassesById(id);
        return classesDto == null ? NotFound() : Ok(classesDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<classesDto>> GetAllclassess()
    {
        return Ok(_classesService.GetAllclassess());
    }

    [HttpPost]
    public ActionResult Addclasses(CreateclassesDto classes)
    {
        int id = _classesService.Addclasses(classes);
        return CreatedAtAction(nameof(GetclassesById), new { id }, id);
    }

    [HttpPut]
    public ActionResult Updateclasses(int id, UpdateclassesDto classes)
    {
        return _classesService.Updateclasses(id, classes) ? NoContent() : NotFound();
    }

    [HttpDelete]
    public ActionResult Deleteclasses(int id)
    {
        return _classesService.Deleteclasses(id) ? NoContent() : NotFound();
    }

    // ================= Zusätzliche Endpunkte, die das Frontend aufruft =================

    /// <summary>
    /// GET api/classes/{classId}/students
    /// Liefert alle Schüler, die in der Klasse eingeschrieben sind (für das Schüler-Dropdown).
    /// </summary>
    [HttpGet("{classId:int}/students")]
    public async Task<ActionResult<IEnumerable<studentsDto>>> GetStudentsForClass(
        int classId,
        [FromServices] AppDbContext db)
    {
        bool classExists = await db.classes.AnyAsync(c => c.class_id == classId);
        if (!classExists) return NotFound();

        var students = await db.class_enrollments
            .Where(e => e.class_id == classId)
            .Join(db.students,
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

        return Ok(students);
    }

    /// <summary>
    /// GET api/classes/{classId}/students/{studentId}/lastgrade
    /// Liefert Enrollment-ID und die letzte erfasste Note für den Schüler in der Klasse.
    /// </summary>
    [HttpGet("{classId:int}/students/{studentId:int}/lastgrade")]
    public async Task<ActionResult<EnrollmentAndLastGradeDto>> GetLastGradeForStudentInClass(
        int classId,
        int studentId,
        [FromServices] AppDbContext db)
    {
        int? enrollmentId = await db.class_enrollments
            .Where(e => e.class_id == classId && e.student_id == studentId)
            .Select(e => (int?)e.enrollment_id)
            .SingleOrDefaultAsync();

        if (enrollmentId is null) return NotFound();

        string? lastGrade = await db.grades
            .Where(g => g.enrollment_id == enrollmentId.Value)
            .OrderByDescending(g => g.grade_timestamp)
            .Select(g => g.grade_value)
            .FirstOrDefaultAsync();

        var dto = new EnrollmentAndLastGradeDto
        {
            enrollment_id = enrollmentId.Value,
            last_grade_value = lastGrade
        };

        return Ok(dto);
    }
}
