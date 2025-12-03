using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model;

public class grades
{
    [Key]
    public int grade_id { get; set; }

    [Required]
    public int enrollment_id { get; set; }

    [Required]
    [MaxLength(10)]
    public string grade_value { get; set; }

    [Required]
    [Column("grade_timestamp")]
    public DateTime grade_timestamp { get; set; }

    public string comment { get; set; }


}