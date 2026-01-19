using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class RegisterTeacherRequestDto
{
    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string email { get; set; } = "";

    [Required]
    [MinLength(6)]
    public string password { get; set; } = "";

    [Required]
    [Compare(nameof(password), ErrorMessage = "Passwords do not match")]
    public string passwordConfirm { get; set; } = "";
}
