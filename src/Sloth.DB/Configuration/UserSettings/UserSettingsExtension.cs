using Microsoft.EntityFrameworkCore;

namespace Sloth.DB.Configuration.UserSettings
{
    public static class UserSettingsExtension
    {
        public static ModelBuilder AddUserSettingConfiguration(this ModelBuilder modelBuilder)
        {
            return modelBuilder
                .ApplyConfiguration(new UserSettingConfiguration())
                .ApplyConfiguration(new UserSettingValueConfiguration())
                .ApplyConfiguration(new UserDefSettingValueConfiguration())
                .ApplyConfiguration(new UserSettingLookupValueConfiguration());
        }
    }
}