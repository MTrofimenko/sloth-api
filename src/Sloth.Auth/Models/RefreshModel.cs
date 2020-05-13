
using System.ComponentModel.DataAnnotations;

namespace Sloth.Auth.Models
{
    public class RefreshModel
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
