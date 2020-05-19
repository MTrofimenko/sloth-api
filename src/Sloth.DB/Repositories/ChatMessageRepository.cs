using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sloth.Common.Exceptions;
using Sloth.DB.Models;
using Sloth.DB.Repositories.Models;

namespace Sloth.DB.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly ISlothDbContext _dbContext;

        public ChatMessageRepository(ISlothDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> SaveChatMessageAsync(CreateChatMessageRequest messageRequest)
        {
            var chatMember = _dbContext.ChatMembers
                .Include(x => x.Chat)
                .FirstOrDefault(x => x.ChatId == messageRequest.ChatId && x.UserId == messageRequest.UserId);

            if (chatMember == null)
            {
                throw new SlothEntityNotFoundException($"Couldn't save message, because chat member wasn't found for chat {messageRequest.ChatId}.");
            }

            if (chatMember.Chat.Status != ChatStatus.Active || chatMember.Status != ChatMemberStatus.Active)
            {
                throw new SlothException($"Chat {messageRequest.ChatId} is not active or member {chatMember.Id} is not active.");
            }

            var message = new ChatMessage()
            {
                ChatMemberId = chatMember.Id,
                Message = messageRequest.Message,
                ReplyToMessageId = messageRequest.ReplyToMessageId, // TODO: add checks that message exists
                ForwardFromUserId = messageRequest.ForwardFromUserId //TODO: add checks that user exists
            };

            await _dbContext.ChatMessages.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return message.Id;
        }

        public async Task<IEnumerable<ChatMessage>> GetChatMessagesAsync(Guid chatId, Guid userId) // TODO: implement message deletion
        {
            var members = await _dbContext.ChatMembers
                .Where(x => x.ChatId == chatId)
                .ToArrayAsync();

            var userIds = members.Select(x => x.UserId).ToArray();
            var memberIds = members.Select(x => x.Id).ToArray();

            if (!members.Any() || !userIds.Contains(userId))
            {
                throw new SlothException("No members were found for the chat or user doesn't belong to it.");
            }

            var messages = await _dbContext.ChatMessages
                .Include(x => x.Sender)
                .Where(x => memberIds.Contains(x.ChatMemberId))
                .ToArrayAsync();

            return messages;
        }

        public async Task<ChatMessage> GetChatMessageByIdAsync(Guid chatMessageId)
        {
            var message = await _dbContext.ChatMessages
                .Include(x => x.Sender)
                .FirstOrDefaultAsync(x => x.Id == chatMessageId);

            return message;
        }
    }
}
