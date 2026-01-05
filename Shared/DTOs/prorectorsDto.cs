using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class prorectorsDto
{
    public int prorector_id { get; set; }

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

    [Required]
 public int department_id_1 { get; set; }

    public int? department_id_2 { get; set; }
}

public class CreateprorectorsDto
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

    [Required]
    public int department_id_1 { get; set; }

    public int? department_id_2 { get; set; }
}

public class UpdateprorectorsDto
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

    [Required]
    public int department_id_1 { get; set; }

    public int? department_id_2 { get; set; }
}
