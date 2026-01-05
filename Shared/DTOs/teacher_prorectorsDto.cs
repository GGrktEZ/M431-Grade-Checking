using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class teacher_prorectorsDto
{
    public int teacher_id { get; set; }

  [Required]
    public int prorector_id_1 { get; set; }

 public int? prorector_id_2 { get; set; }
}

public class Createteacher_prorectorsDto
{
    [Required]
    public int teacher_id { get; set; }

  [Required]
    public int prorector_id_1 { get; set; }

    public int? prorector_id_2 { get; set; }
}

public class Updateteacher_prorectorsDto
{
    [Required]
    public int prorector_id_1 { get; set; }

    public int? prorector_id_2 { get; set; }
}
