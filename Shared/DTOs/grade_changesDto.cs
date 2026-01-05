using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class grade_changesDto
{
    public int change_id { get; set; }

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

    [Required]
    [MaxLength(10)]
    public string old_grade_value { get; set; } = "";

    [Required]
    [MaxLength(10)]
    public string new_grade_value { get; set; } = "";

    public string? comment { get; set; }
}

public class Creategrade_changesDto
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

    [Required]
    [MaxLength(10)]
public string old_grade_value { get; set; } = "";

 [Required]
    [MaxLength(10)]
    public string new_grade_value { get; set; } = "";

    public string? comment { get; set; }
}

public class Updategrade_changesDto
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

    [Required]
    [MaxLength(10)]
    public string old_grade_value { get; set; } = "";

    [Required]
    [MaxLength(10)]
    public string new_grade_value { get; set; } = "";

    public string? comment { get; set; }
}
