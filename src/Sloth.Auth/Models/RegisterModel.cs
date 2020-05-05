using System.ComponentModel.DataAnnotations;

namespace Sloth.Auth.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
