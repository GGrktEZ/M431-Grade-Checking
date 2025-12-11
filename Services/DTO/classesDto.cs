using System.ComponentModel.DataAnnotations;

//dies ist der Beste Kommentar EUW
namespace Services.DTO;

public class classesDto
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