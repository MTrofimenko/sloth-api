using System;

namespace Sloth.DB.Models
{
    public class SessionRefreshToken : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredTime { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties

        public User User { get; set; }
    }
}
