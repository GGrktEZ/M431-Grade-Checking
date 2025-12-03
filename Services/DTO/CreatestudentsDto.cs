using System.ComponentModel.DataAnnotations;

namespace Services.DTO;

public class CreatestudentsDto
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


}