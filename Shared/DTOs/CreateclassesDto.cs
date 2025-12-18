using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class CreateclassesDto
{
    [Required]
    [MaxLength(150)]
public string class_name { get; set; }

    public string description { get; set; }

    [Required]
    public int teacher_id { get; set; }
}
