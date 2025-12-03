using System.ComponentModel.DataAnnotations;

namespace Services.DTO;

public class UpdateclassesDto
{
    [Required]
    [MaxLength(150)]
    public string class_name { get; set; }

    public string description { get; set; }

    [Required]
    public int teacher_id { get; set; }
}