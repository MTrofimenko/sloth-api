using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sloth.DB.Models;
using Sloth.DB.Repositories.Models;

namespace Sloth.DB.Repositories
{
    public interface IChatMessageRepository
    {
        Task<Guid> SaveChatMessageAsync(CreateChatMessageRequest messageRequest);
        Task<IEnumerable<ChatMessage>> GetChatMessagesAsync(Guid chatId, Guid userId);
        Task<ChatMessage> GetChatMessageByIdAsync(Guid chatMessageId);
    }
}
