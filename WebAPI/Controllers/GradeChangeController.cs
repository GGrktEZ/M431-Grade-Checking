using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Services.Service;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradeChangeController : ControllerBase
{
    private readonly IGradeChangeNotificationService _gradeChangeNotificationService;

    public GradeChangeController(IGradeChangeNotificationService gradeChangeNotificationService)
  {
        _gradeChangeNotificationService = gradeChangeNotificationService;
    }

    /// <summary>
 /// Submit a grade change and send notification email to prorector.
 /// </summary>
    /// <param name="dto">The grade change submission data.</param>
    /// <returns>Result with success status and email notification status.</returns>
    [HttpPost]
    public async Task<ActionResult<GradeChangeSubmissionResultDto>> SubmitGradeChange([FromBody] SubmitGradeChangeDto dto)
{
 if (!ModelState.IsValid)
        {
       return BadRequest(new GradeChangeSubmissionResultDto
 {
      success = false,
             message = "Ungültige Eingabedaten.",
         emailSent = false
       });
 }

        var result = await _gradeChangeNotificationService.SubmitGradeChangeAsync(dto);

       if (!result.success)
        {
     return BadRequest(result);
        }

        return Ok(result);
    }
}
