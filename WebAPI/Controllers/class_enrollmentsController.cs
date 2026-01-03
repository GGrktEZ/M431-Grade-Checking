using Microsoft.AspNetCore.Mvc;
using Services.Service;
using Shared.DTOs;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class class_enrollmentsController : ControllerBase
{
    private readonly Iclass_enrollmentsService _enrollmentsService;

    public class_enrollmentsController(Iclass_enrollmentsService enrollmentsService)
    {
        _enrollmentsService = enrollmentsService;
    }

    [HttpGet("{id}")]
    public ActionResult<class_enrollmentsDto?> Getclass_enrollmentsById(int id)
    {
        var dto = _enrollmentsService.Getclass_enrollmentsById(id);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<class_enrollmentsDto>> GetAllclass_enrollmentss()
    {
        return Ok(_enrollmentsService.GetAllclass_enrollmentss());
    }

    [HttpPost]
    public ActionResult Addclass_enrollments(Createclass_enrollmentsDto enrollment)
    {
        int id = _enrollmentsService.Addclass_enrollments(enrollment);
        return CreatedAtAction(nameof(Getclass_enrollmentsById), new { id }, id);
    }

    [HttpPut]
    public ActionResult Updateclass_enrollments(int id, Updateclass_enrollmentsDto enrollment)
    {
        return _enrollmentsService.Updateclass_enrollments(id, enrollment) ? NoContent() : NotFound();
    }

    [HttpDelete]
    public ActionResult Deleteclass_enrollments(int id)
    {
        return _enrollmentsService.Deleteclass_enrollments(id) ? NoContent() : NotFound();
    }
}
