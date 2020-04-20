using Microsoft.EntityFrameworkCore;

namespace Sloth.DB.Configuration.ChatSettings
{
    public static class ChatSettingsExtension
    {
        public static ModelBuilder AddChatSettingConfiguration(this ModelBuilder modelBuilder)
        {
            return modelBuilder
                .ApplyConfiguration(new ChatSettingConfiguration())
                .ApplyConfiguration(new ChatSettingValueConfiguration())
                .ApplyConfiguration(new ChatDefSettingValueConfiguration())
                .ApplyConfiguration(new ChatSettingLookupValueConfiguration());
        }
    }
}