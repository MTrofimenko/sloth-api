using Microsoft.EntityFrameworkCore;

namespace Sloth.DB.Configuration.ChatMemberSettings
{
    public static class ChatMemberSettingsExtension
    {
        public static ModelBuilder AddChatMemberSettingConfiguration(this ModelBuilder modelBuilder)
        {
            return modelBuilder
                .ApplyConfiguration(new ChatMemberSettingConfiguration())
                .ApplyConfiguration(new ChatMemberSettingValueConfiguration())
                .ApplyConfiguration(new ChatMemberDefSettingValueConfiguration())
                .ApplyConfiguration(new ChatMemberSettingLookupValueConfiguration());
        }
    }
}