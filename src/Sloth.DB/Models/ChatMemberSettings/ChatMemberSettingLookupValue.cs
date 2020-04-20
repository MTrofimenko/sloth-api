using System;

namespace Sloth.DB.Models.ChatMemberSettings
{
    public class ChatMemberSettingLookupValue : BaseEntity
    {
        public Guid SettingId { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public ChatMemberSetting Setting { get; set; }
    }
}
