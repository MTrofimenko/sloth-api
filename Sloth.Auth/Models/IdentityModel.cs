using System.ComponentModel.DataAnnotations;

namespace Sloth.Auth.Models
{
    public class IdentityModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
