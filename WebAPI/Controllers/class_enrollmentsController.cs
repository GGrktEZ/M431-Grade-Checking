using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Services.Service;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class class_enrollmentsController : ControllerBase
{
    private readonly Iclass_enrollmentsService _class_enrollmentsService;

    /// <summary>
    /// Constructor to enable the dependency injection of <see cref="class_enrollmentsService"/>.
    /// </summary>
    /// <param name="class_enrollmentsService">The reference to the class_enrollmentsService.</param>
    public class_enrollmentsController(Iclass_enrollmentsService class_enrollmentsService)
    {
        _class_enrollmentsService = class_enrollmentsService;
    }
    [HttpGet("{id}")]
    public ActionResult<class_enrollmentsDto?> Getclass_enrollmentsById(int id)
    {
        class_enrollmentsDto? class_enrollmentsDto = _class_enrollmentsService.Getclass_enrollmentsById(id);
        return class_enrollmentsDto == null ? NotFound() : Ok(class_enrollmentsDto);
    }
    [HttpGet]
    public ActionResult<IEnumerable<class_enrollmentsDto>> GetAllclass_enrollmentss()
    {
        return Ok(_class_enrollmentsService.GetAllclass_enrollmentss());
    }
    [HttpPost]
    public ActionResult Addclass_enrollments(Createclass_enrollmentsDto class_enrollments)
    {
        int id = _class_enrollmentsService.Addclass_enrollments(class_enrollments);
        return CreatedAtAction(nameof(Getclass_enrollmentsById), new { id }, id);
    }
    [HttpPut]
    public ActionResult Updateclass_enrollments(int id, Updateclass_enrollmentsDto class_enrollments)
    {
        return _class_enrollmentsService.Updateclass_enrollments(id, class_enrollments) ? NoContent() : NotFound();
    }
    [HttpDelete]
    public ActionResult Deleteclass_enrollments(int id)
    {
        return _class_enrollmentsService.Deleteclass_enrollments(id) ? NoContent() : NotFound();
    }

}