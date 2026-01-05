using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model;

public class departments
{
    [Key]
    public int department_id { get; set; }

    [Required]
    [MaxLength(200)]
    public string department_name { get; set; } = "";
}
