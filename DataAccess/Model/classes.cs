using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model;

public class classes
{
    [Key]
    public int class_id { get; set; }

    [Required]
    [MaxLength(150)]
    public string class_name { get; set; }

    public string description { get; set; }

    [Required]
    public int teacher_id { get; set; }


}