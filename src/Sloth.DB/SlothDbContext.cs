using Microsoft.EntityFrameworkCore;
using Sloth.DB.Configuration;
using Sloth.DB.Configuration.ChatMemberSettings;
using Sloth.DB.Configuration.ChatSettings;
using Sloth.DB.Configuration.UserSettings;
using Sloth.DB.Models;
using Sloth.DB.Models.ChatMemberSettings;
using Sloth.DB.Models.ChatSettings;
using Sloth.DB.Models.UserSettings;

namespace Sloth.DB
{
    public class SlothDbContext : DbContext, ISlothDbContext
    {
        public SlothDbContext(DbContextOptions<SlothDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        //public DbSet<User> UserSessions { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        //public DbSet<File> Files { get; set; }

        // Settings: User
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<UserDefSettingValue> UserDefSettingValues { get; set; }
        public DbSet<UserSettingValue> UserSettingValues { get; set; }
        public DbSet<UserSettingLookupValue> UserSettingLookupValues { get; set; }

        // Settings: Chat
        public DbSet<ChatSetting> ChatSettings { get; set; }
        public DbSet<ChatDefSettingValue> ChatDefSettingValues { get; set; }
        public DbSet<ChatSettingValue> ChatSettingValues { get; set; }
        public DbSet<ChatSettingLookupValue> ChatSettingLookupValues { get; set; }

        // Settings: Chat Member
        public DbSet<ChatMemberSetting> ChatMemberSettings { get; set; }
        public DbSet<ChatMemberDefSettingValue> ChatMemberDefSettingValues { get; set; }
        public DbSet<ChatMemberSettingValue> ChatMemberSettingValues { get; set; }
        public DbSet<ChatMemberSettingLookupValue> ChatMemberSettingLookupValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMemberConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());

            // Settings
            modelBuilder.AddUserSettingConfiguration();
            modelBuilder.AddChatSettingConfiguration();
            modelBuilder.AddChatMemberSettingConfiguration();
        }
    }
}
