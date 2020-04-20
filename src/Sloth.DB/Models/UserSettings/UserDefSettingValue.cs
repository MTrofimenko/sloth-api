using Sloth.DB.Models.Setting;

namespace Sloth.DB.Models.UserSettings
{
    public class UserDefSettingValue : SettingValue
    {
        // Navigation Properties
        public UserSetting Setting { get; set; }
        public UserSettingLookupValue LookupValue { get; set; }
    }
}
