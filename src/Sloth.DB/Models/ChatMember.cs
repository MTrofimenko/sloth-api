using System;
using System.Collections.Generic;
using Sloth.DB.Models.ChatMemberSettings;

namespace Sloth.DB.Models
{
    public class ChatMember : BaseEntity
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public ChatMemberStatus Status { get; set; }
        public string PublicKey { get; set; }

        // Navigation Properties
        public Chat Chat { get; set; }
        public User User { get; set; }

        public ICollection<ChatMessage> Messages { get; set; } = new HashSet<ChatMessage>();
        public ICollection<ChatMemberSettingValue> SettingValues { get; set; } = new HashSet<ChatMemberSettingValue>();
    }
}
