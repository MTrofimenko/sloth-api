using System;
using Sloth.DB.Models.Setting;

namespace Sloth.DB.Models.ChatSettings
{
    public class ChatSettingValue : SettingValue
    {
        public Guid ChatId { get; set; }

        // Navigation properties
        public Chat Chat { get; set; }
        public ChatSetting Setting { get; set; }
        public ChatSettingLookupValue LookupValue { get; set; }
    }
}
