using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.Service;

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

}