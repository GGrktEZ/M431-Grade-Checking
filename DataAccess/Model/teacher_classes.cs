using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model;

[PrimaryKey(nameof(teacher_id), nameof(class_id), nameof(module_id))]
public class teacher_classes
{
    [Required]
    public int teacher_id { get; set; }

    [Required]
    public int class_id { get; set; }

    [Required]
    public int module_id { get; set; }
}
