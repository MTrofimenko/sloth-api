using System;
using System.Collections.Generic;
using System.Text;

namespace Sloth.Auth.Models
{
    public class AuthResponse
    {
        public string Login { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
        public string RefreshToken { get; set; }
    }
}
