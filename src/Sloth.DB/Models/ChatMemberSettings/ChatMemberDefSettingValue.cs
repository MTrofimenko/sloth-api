using Sloth.DB.Models.Setting;

namespace Sloth.DB.Models.ChatMemberSettings
{
    public class ChatMemberDefSettingValue : SettingValue
    {
        // Navigation Properties
        public ChatMemberSetting Setting { get; set; }
        public ChatMemberSettingLookupValue LookupValue { get; set; }
    }
}
