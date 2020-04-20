using System;

namespace Sloth.DB.Models.ChatSettings
{
    public class ChatSettingLookupValue : BaseEntity
    {
        public Guid SettingId { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public ChatSetting Setting { get; set; }
    }
}
