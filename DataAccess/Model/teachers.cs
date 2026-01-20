using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model;

public class teachers
{
    [Key]
    public int teacher_id { get; set; }

    [Required, MaxLength(100)]
    public string first_name { get; set; }

    [Required, MaxLength(100)]
    public string last_name { get; set; }

    [Required, EmailAddress, MaxLength(150)]
    public string email { get; set; }

    public int? department_id_1 { get; set; }
    public int? department_id_2 { get; set; }

    [MaxLength(500)]
    public string? password_hash { get; set; }

    // Registration / Email confirmation
    public bool email_confirmed { get; set; } = false;

    [MaxLength(500)]
    public string? email_confirmation_token_hash { get; set; }

    public DateTime? email_confirmation_expires_at { get; set; }
    public DateTime? registration_requested_at { get; set; }
    public DateTime? email_confirmed_at { get; set; }

    // NEU: 2FA Login per Mail (one-time)
    [MaxLength(500)]
    public string? login_token_hash { get; set; }

    public DateTime? login_token_expires_at { get; set; }
}
