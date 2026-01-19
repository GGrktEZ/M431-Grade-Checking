using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class CreateteachersDto
{
    [Required]
    [MaxLength(100)]
    public string first_name { get; set; } = "";

    [Required]
    [MaxLength(100)]
    public string last_name { get; set; } = "";

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string email { get; set; } = "";

    // NEW: Email bestaetigen
    [Required]
    [EmailAddress]
    [MaxLength(150)]
    [Compare(nameof(email), ErrorMessage = "Email-Adressen stimmen nicht ueberein")]
    public string email_confirm { get; set; } = "";

    public int? department_id_1 { get; set; }

    public int? department_id_2 { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string password { get; set; } = "";

    // NEW: Passwort bestaetigen
    [Required]
    [Compare(nameof(password), ErrorMessage = "Passwoerter stimmen nicht ueberein")]
    public string password_confirm { get; set; } = "";
}
