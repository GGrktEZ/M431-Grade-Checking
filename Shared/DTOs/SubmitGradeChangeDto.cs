using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class SubmitGradeChangeDto
{
    [Required]
    public int class_id { get; set; }

    [Required]
    public int student_id { get; set; }

    [Required]
 public int module_id { get; set; }

    [Required]
    public int teacher_id { get; set; }

    [Required]
    public int prorector_id { get; set; }

    [Required]
    [MaxLength(150)]
    public string assessment_title { get; set; } = "";

    [MaxLength(10)]
    public string? old_grade_value { get; set; }

    [Required]
 [MaxLength(10)]
    public string new_grade_value { get; set; } = "";

    public string? comment { get; set; }

    public bool has_future_grade { get; set; }
}

public class GradeChangeSubmissionResultDto
{
    public bool success { get; set; }
    public string message { get; set; } = "";
    public bool emailSent { get; set; }
}
