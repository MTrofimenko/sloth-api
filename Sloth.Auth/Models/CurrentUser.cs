using System;
using System.Collections.Generic;
using System.Text;

namespace Sloth.Auth.Models
{
    public class CurrentUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
