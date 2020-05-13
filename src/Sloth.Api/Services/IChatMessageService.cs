using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sloth.Api.Models;

namespace Sloth.Api.Services
{
    public interface IChatMessageService
    {
        Task<IEnumerable<ChatMessageDto>> GetChatMessagesAsync(Guid chatId, Guid userId);
        Task<Guid> SaveChatMessageAsync(Guid chatId, CreateChatMessageRequest messageRequest, Guid userId);
    }
}
