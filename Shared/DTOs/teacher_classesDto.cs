using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class teacher_classesDto
{
    [Required]
    public int teacher_id { get; set; }

    [Required]
    public int class_id { get; set; }

    [Required]
    public int module_id { get; set; }
}

public class Createteacher_classesDto
{
    [Required]
    public int teacher_id { get; set; }

    [Required]
    public int class_id { get; set; }

    [Required]
    public int module_id { get; set; }
}

public class Updateteacher_classesDto
{
    [Required]
    public int teacher_id { get; set; }

    [Required]
    public int class_id { get; set; }
}
