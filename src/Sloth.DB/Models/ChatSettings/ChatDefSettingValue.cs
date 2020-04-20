using Sloth.DB.Models.Setting;

namespace Sloth.DB.Models.ChatSettings
{
    public class ChatDefSettingValue : SettingValue
    {
        // Navigation Properties
        public ChatSetting Setting { get; set; }
        public ChatSettingLookupValue LookupValue { get; set; }
    }
}
