using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model;

public class class_enrollments
{
    [Key]
    public int enrollment_id { get; set; }

    [Required]
    public int class_id { get; set; }

    [Required]
    public int student_id { get; set; }


}