using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sloth.Api.Exceptions;
using Sloth.Api.Models;
using Sloth.DB;
using Sloth.DB.Models;

namespace Sloth.Api.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly ISlothDbContext _dbContext;

        public ChatMessageService(ISlothDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ChatMessageDto>> GetChatMessagesAsync(Guid chatId, Guid userId) // TODO: implement message deletion
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
                .Select(x => new ChatMessageDto()
                {
                    Id = x.Id,
                    UserId = x.Sender.UserId,
                    ChatMemberId = x.ChatMemberId,
                    ForwardFromUserId = x.ForwardFromUserId,
                    Message = x.Message,
                    ReplyToMessageId = x.ReplyToMessageId,
                    // SendDate = x.CreatedOn.Value TODO: check why is not set on db
                }).ToArrayAsync();

            return messages;
        }

        public async Task<Guid> SaveChatMessageAsync(Guid chatId, CreateChatMessageRequest messageRequest, Guid userId)
        {
            var chatMember = _dbContext.ChatMembers
                .Include(x=> x.Chat)
                .FirstOrDefault(x => x.ChatId == chatId && x.UserId == userId);

            if (chatMember == null)
            {
                throw new SlothEntityNotFoundException($"Couldn't save message, because chat member wasn't found for chat {chatId}.");
            }

            if (chatMember.Chat.Status != ChatStatus.Active || chatMember.Status != ChatMemberStatus.Active)
            {
                throw new SlothException($"Chat {chatId} is not active or member {chatMember.Id} is not active.");
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
    }
}