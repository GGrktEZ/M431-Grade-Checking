using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class class_enrollmentsDto
{
    [Key]
    public int enrollment_id { get; set; }

  [Required]
    public int class_id { get; set; }

    [Required]
    public int student_id { get; set; }
}
