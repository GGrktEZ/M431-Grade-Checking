using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class RegisterExistingTeacherDto
    {
        [Required, EmailAddress, MaxLength(150)]
        public string email { get; set; } = "";

        [Required, EmailAddress, MaxLength(150)]
        [Compare(nameof(email))]
        public string email_confirm { get; set; } = "";

        [Required, MinLength(6)]
        public string password { get; set; } = "";

        [Required]
        [Compare(nameof(password))]
        public string password_confirm { get; set; } = "";
    }

}
