using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class UpdateclassesDto
{
    [Required]
    [MaxLength(150)]
    public string class_name { get; set; }

    public string description { get; set; }
}
