using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class CreateteachersDto
{
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

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string password { get; set; }
}
