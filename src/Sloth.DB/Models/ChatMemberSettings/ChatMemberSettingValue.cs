using System;
using Sloth.DB.Models.Setting;

namespace Sloth.DB.Models.ChatMemberSettings
{
    public class ChatMemberSettingValue : SettingValue
    {
        public Guid ChatMemberId { get; set; }

        // Navigation properties
        public ChatMember Member { get; set; }
        public ChatMemberSetting Setting { get; set; }
        public ChatMemberSettingLookupValue LookupValue { get; set; }
    }
}
