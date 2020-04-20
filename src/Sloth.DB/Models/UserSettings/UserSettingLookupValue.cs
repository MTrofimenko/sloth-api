using System;

namespace Sloth.DB.Models.UserSettings
{
    public class UserSettingLookupValue : BaseEntity
    {
        public Guid SettingId { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public UserSetting Setting { get; set; }
    }
}
