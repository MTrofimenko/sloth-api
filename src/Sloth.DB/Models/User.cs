using System;

namespace Sloth.DB.Models
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Guid? LogoId { get; set; }

        // Navigation Properties
        public File Logo { get; set; }
    }
}
