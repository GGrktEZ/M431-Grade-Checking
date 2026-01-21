using Shared.DTOs;

namespace Services.Service;

public interface IGradeChangeNotificationService
{
 /// <summary>
    /// Submits a grade change and sends notification email to prorector.
    /// </summary>
    /// <param name="dto">The grade change data.</param>
    /// <returns>Result indicating success and email status.</returns>
    Task<GradeChangeSubmissionResultDto> SubmitGradeChangeAsync(SubmitGradeChangeDto dto);
}
