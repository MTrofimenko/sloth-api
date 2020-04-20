using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sloth.DB.Models;
using Sloth.DB.Models.ChatMemberSettings;
using Sloth.DB.Models.ChatSettings;
using Sloth.DB.Models.UserSettings;

namespace Sloth.DB
{
    public interface ISlothDbContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        //DbSet<User> UserSessions { get; set; }
        DbSet<Chat> Chats { get; set; }
        DbSet<ChatMember> ChatMembers { get; set; }
        DbSet<ChatMessage> ChatMessages { get; set; }
        //DbSet<File> Files { get; set; }

        // Settings: User
        DbSet<UserSetting> UserSettings { get; set; }
        DbSet<UserDefSettingValue> UserDefSettingValues { get; set; }
        DbSet<UserSettingValue> UserSettingValues { get; set; }
        DbSet<UserSettingLookupValue> UserSettingLookupValues { get; set; }

        // Settings: Chat
        DbSet<ChatSetting> ChatSettings { get; set; }
        DbSet<ChatDefSettingValue> ChatDefSettingValues { get; set; }
        DbSet<ChatSettingValue> ChatSettingValues { get; set; }
        DbSet<ChatSettingLookupValue> ChatSettingLookupValues { get; set; }

        // Settings: Chat Member
        DbSet<ChatMemberSetting> ChatMemberSettings { get; set; }
        DbSet<ChatMemberDefSettingValue> ChatMemberDefSettingValues { get; set; }
        DbSet<ChatMemberSettingValue> ChatMemberSettingValues { get; set; }
        DbSet<ChatMemberSettingLookupValue> ChatMemberSettingLookupValues { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
