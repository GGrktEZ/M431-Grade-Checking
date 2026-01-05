using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class departmentsDto
{
    public int department_id { get; set; }

    [Required]
    [MaxLength(200)]
    public string department_name { get; set; } = "";
}

public class CreatedepartmentsDto
{
    [Required]
    [MaxLength(200)]
  public string department_name { get; set; } = "";
}

public class UpdatedepartmentsDto
{
    [Required]
    [MaxLength(200)]
    public string department_name { get; set; } = "";
}
