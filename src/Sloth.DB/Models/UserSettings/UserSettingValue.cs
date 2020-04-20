using System;
using Sloth.DB.Models.Setting;

namespace Sloth.DB.Models.UserSettings
{
    public class UserSettingValue : SettingValue
    {
        public Guid UserId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public UserSetting Setting { get; set; }
        public UserSettingLookupValue LookupValue { get; set; }
    }
}
