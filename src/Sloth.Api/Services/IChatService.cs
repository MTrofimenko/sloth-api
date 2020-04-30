using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sloth.Api.Models;

namespace Sloth.Api.Services
{
    public interface IChatService
    {
        Task<Guid> CreateChatAsync(CreateChatRequest request, Guid userId);
        Task<IEnumerable<ChatDto>> GetChatsAsync(Guid userId);
        Task ConfirmChatAsync(Guid chatId, Guid userId, string publicKey);
        Task DeclineChatAsync(Guid chatId, Guid userId);
    }
}