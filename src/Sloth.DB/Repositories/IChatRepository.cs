using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sloth.DB.Models;

namespace Sloth.DB.Repositories
{
    public interface IChatRepository
    {
        Task<Chat> CreateChatAsync(string chatName);
        Task CreateChatMemberAsync(Guid chatId, Guid memberId, ChatMemberStatus status = ChatMemberStatus.Pending, string publicKey = null);
        Task<Chat> GetChatByIdAsync(Guid chatId);
        Task<ChatMember> GetChatMemberAsync(Guid chatId, Guid userId);
        Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId);
    }
}
