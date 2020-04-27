using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sloth.Api.Models;

namespace Sloth.Api.Services
{
    public interface IChatMessageService
    {
        Task<IEnumerable<ChatMessageDto>> GetChatMessagesAsync(Guid chatId, Guid userId);
        Task SaveChatMessageAsync(Guid chatId, CreateChatMessageRequest messageRequest, Guid userId);
    }
}
