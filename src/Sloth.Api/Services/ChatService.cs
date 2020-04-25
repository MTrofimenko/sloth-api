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
    public class ChatService : IChatService
    {
        private readonly ISlothDbContext _dbContext;

        public ChatService(ISlothDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateChatAsync(CreateChatDto request, Guid userId)
        {
            var chat = await CreateChatAsync(request.Name);

            foreach (var memberId in request.MemberIds)
            {
                await CreateChatMemberAsync(chat.Id, memberId);
            }

            // Add creator as a member 
            await CreateChatMemberAsync(chat.Id, userId, ChatMemberStatus.Active, request.CreatorPublicKey);

            await _dbContext.SaveChangesAsync();

            return chat.Id;
        }

        public async Task<IEnumerable<ChatDto>> GetChatsAsync(Guid userId)
        {
            var chatIds = await _dbContext.ChatMembers
                .Include(c => c.Chat)
                .Where(x => x.UserId == userId && (x.Chat.Status != ChatStatus.Deleted && x.Chat.Status != ChatStatus.Aborted) &&
                            (x.Status != ChatMemberStatus.Aborted ||
                             x.Status != ChatMemberStatus.Removed))
                .Select(x => x.ChatId)
                .ToListAsync();

            var chats = (await _dbContext.Chats
                .Include(c => c.Members)
                .ThenInclude(c => c.User)
                .Where(x => chatIds.Contains(x.Id))
                .ToArrayAsync())
                .Select(x=> ToChatDto(x, userId));

            return chats;
        }

        public async Task ConfirmChatAsync(Guid chatId, Guid userId, string publicKey)
        {
            var chat = GetChatById(chatId);
            var chatMember = GetChatMember(chat, userId);

            // TODO: might intermediate status needed
            chatMember.Status = ChatMemberStatus.Active;
            chatMember.PublicKey = publicKey;

            chat.Status = ChatStatus.Active;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeclineChatAsync(Guid chatId, Guid userId)
        {
            var chat = GetChatById(chatId);

            var chatMember = GetChatMember(chat, userId);

            // TODO: might intermediate status needed
            chatMember.Status = ChatMemberStatus.Aborted;
            chat.Status = ChatStatus.Aborted;

            await _dbContext.SaveChangesAsync();
        }


        private static ChatMember GetChatMember(Chat chat, Guid userId)
        {
            var currentMember = chat.Members.FirstOrDefault(x => x.UserId == userId); 
            if (currentMember == null)
            {
                throw new SlothEntityNotFoundException($"Chat member for chatId {chat.Id} was not found.");
            }

            return currentMember;
        }

        private static string GetChatName(Chat chat, Guid userId)
        {
            if (!string.IsNullOrWhiteSpace(chat.Name))
            {
                return chat.Name;
            } 

            var interlocutor = chat.Members.FirstOrDefault(x => x.UserId != userId)?.User;
            if (interlocutor == null)
            {
                throw new SlothException($"Can't find interlocutor for chat {chat.Id} and userId {userId}.");
            }

            return $"{interlocutor?.FirstName} {interlocutor?.LastName}".Trim();
        }

        private static ChatDto ToChatDto(Chat chat, Guid userId)
        {
            var chatName = GetChatName(chat, userId);

            return new ChatDto()
            {
                Id = chat.Id,
                Name = chatName,
                Status = chat.Status,
                Members = chat.Members
                    .Where(x => x.Status != ChatMemberStatus.Removed)
                    .Select(ToChatMemberDto)
            };
        }

        private static ChatMemberDto ToChatMemberDto(ChatMember y)
        {
            return new ChatMemberDto()
            {
                UserId = y.UserId,
                Status = y.Status,
                PublicKey = y.PublicKey
            };
        }

        private Chat GetChatById(Guid chatId)
        {
            var chat = _dbContext.Chats.Include(x => x.Members).FirstOrDefault(x => x.Id == chatId);
            if (chat == null)
            {
                throw new SlothEntityNotFoundException($"Chat with id {chatId} wasn't found.");
            }

            return chat;
        }

        private async Task CreateChatMemberAsync(Guid chatId, Guid memberId, ChatMemberStatus status = ChatMemberStatus.Pending, string publicKey = null)
        {
            var member = new ChatMember()
            {
                UserId = memberId,
                ChatId = chatId,
                Status = status,
                PublicKey = publicKey
            };

            await _dbContext.ChatMembers.AddAsync(member);
        }

        private async Task<Chat> CreateChatAsync(string chatName)
        {
            try
            {
                var chat = new Chat()
                {
                    Name = chatName,
                    Status = ChatStatus.Pending
                    // TODO: add Created By Column
                };

                await _dbContext.Chats.AddAsync(chat);
                await _dbContext.SaveChangesAsync();

                return chat;
            }
            catch (DbUpdateException ex)
            {
                throw new SlothException("Creating chat failed", ex);
            }
        }
    }
}