using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sloth.DB.Models;
using Sloth.DB.Models.ChatMemberSettings;

namespace Sloth.DB
{
    public interface ISlothDbContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        //DbSet<User> UserSessions { get; set; }
        DbSet<Chat> Chats { get; set; }
        DbSet<ChatMember> ChatMembers { get; set; }
        //DbSet<ChatMessage> ChatMessages { get; set; }
        //DbSet<File> Files { get; set; }

        // Settings: Chat Member
        //DbSet<ChatMemberSetting> ChatMemberSettings { get; set; }
        //DbSet<ChatMemberDefSettingValue> ChatMemberDefSettingValues { get; set; }
        //DbSet<ChatMemberSettingValue> ChatMemberSettingValues { get; set; }
        //DbSet<ChatMemberSettingLookupValue> ChatMemberSettingLookupValues { get; set; }

        // TODO:  add User settings, Chat settings

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
