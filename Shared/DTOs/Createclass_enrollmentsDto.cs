using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class Createclass_enrollmentsDto
{
    [Required]
    public int class_id { get; set; }

    [Required]
    public int student_id { get; set; }
}
