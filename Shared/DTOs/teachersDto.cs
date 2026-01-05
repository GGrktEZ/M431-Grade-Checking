using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class teachersDto
{
    [Key]
    public int teacher_id { get; set; }

    [Required]
    [MaxLength(100)]
    public string first_name { get; set; }

    [Required]
    [MaxLength(100)]
    public string last_name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string email { get; set; }

    public int? department_id_1 { get; set; }

    public int? department_id_2 { get; set; }
}
