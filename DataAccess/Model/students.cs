using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model;

public class students
{
    [Key]
    public int student_id { get; set; }

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