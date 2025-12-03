using System.ComponentModel.DataAnnotations;

namespace Services.DTO;

public class UpdategradesDto
{

    [Required]
    public int enrollment_id { get; set; }

    [Required]
    [MaxLength(10)]
    public string grade_value { get; set; }

    [Required]
    public DateTime grade_timestamp { get; set; }

    public string comment { get; set; }


}