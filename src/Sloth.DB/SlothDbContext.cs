using Microsoft.EntityFrameworkCore;
using Sloth.DB.Configuration;
using Sloth.DB.Models;

namespace Sloth.DB
{
    public class SlothDbContext : DbContext, ISlothDbContext
    {
        public SlothDbContext(DbContextOptions<SlothDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        //public DbSet<User> UserSessions { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set; }
        //public DbSet<ChatMessage> ChatMessages { get; set; }
        //public DbSet<File> Files { get; set; }

        // Settings: Chat Member
        //public DbSet<ChatMemberSetting> ChatMemberSettings { get; set; }
        //public DbSet<ChatMemberDefSettingValue> ChatMemberDefSettingValues { get; set; }
        //public DbSet<ChatMemberSettingValue> ChatMemberSettingValues { get; set; }
        //public DbSet<ChatMemberSettingLookupValue> ChatMemberSettingLookupValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMemberConfiguration());

            // TODO: create and add other configurations
        }
    }
}
