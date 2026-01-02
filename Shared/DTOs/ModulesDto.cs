using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class ModuleDto
{
    [Key]
    public int module_id { get; set; }

    [Required]
    [MaxLength(50)]
    public string module_code { get; set; } = "";

    [Required]
    [MaxLength(150)]
    public string module_name { get; set; } = "";

    public string? description { get; set; }
}
