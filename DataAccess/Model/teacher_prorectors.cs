using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model;

public class teacher_prorectors
{
    [Key]
    public int teacher_id { get; set; }

    [Required]
  public int prorector_id_1 { get; set; }

    public int? prorector_id_2 { get; set; }
}
