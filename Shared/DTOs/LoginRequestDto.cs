using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    [MaxLength(150)]
  public string email { get; set; }
    
    [Required]
    public string password { get; set; }
}
