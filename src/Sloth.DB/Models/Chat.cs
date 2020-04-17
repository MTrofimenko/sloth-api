using System;
using System.Collections.Generic;

namespace Sloth.DB.Models
{
    public class Chat: BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid LogoId { get; set; }

        // Navigation properties

        public File Logo { get; set; }

        public ICollection<ChatMember> Members { get; set; } = new HashSet<ChatMember>();
    }
}
