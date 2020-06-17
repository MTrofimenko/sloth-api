using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sloth.Common.Exceptions;
using Sloth.DB.Models;

namespace Sloth.DB.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ISlothDbContext _dbContext;

        public ChatRepository(ISlothDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Chat> CreateChatAsync(string chatName)
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

        public async Task CreateChatMemberAsync(Guid chatId, Guid memberId, ChatMemberStatus status = ChatMemberStatus.Pending, string publicKey = null)
        {
            try
            {
                var member = new ChatMember()
                {
                    UserId = memberId,
                    ChatId = chatId,
                    Status = status,
                    PublicKey = publicKey
                };

                await _dbContext.ChatMembers.AddAsync(member);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new SlothException("Creating chat member failed", ex);
            }
        }

        public async Task<Chat> GetChatByIdAsync(Guid chatId)
        {
            var chat = await _dbContext.Chats
                .Include(x => x.Members)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == chatId);

            if (chat == null)
            {
                throw new SlothEntityNotFoundException($"Chat with id {chatId} wasn't found.");
            }

            return chat;
        }

        public async Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId)
        {
            var chatIds = await _dbContext.ChatMembers
                .Include(c => c.Chat)
                .Where(x => x.UserId == userId && x.Chat.Status != ChatStatus.Deleted && x.Chat.Status != ChatStatus.Aborted &&
                            (x.Status != ChatMemberStatus.Aborted ||
                             x.Status != ChatMemberStatus.Removed))
                .Select(x => x.ChatId)
                .ToListAsync();

            var chats = await _dbContext.Chats
                    .Include(c => c.Members)
                    .ThenInclude(c => c.User)
                    .Where(x => chatIds.Contains(x.Id))
                    .ToArrayAsync();

            return chats;
        }

        public async Task<ChatMember> GetChatMemberAsync(Guid chatId, Guid userId)
        {
            var chatMember = await _dbContext.ChatMembers
                .FirstOrDefaultAsync(x => x.ChatId == chatId && x.UserId == userId);

            if (chatMember == null)
            {
                throw new SlothEntityNotFoundException($"Chat member for chatId {chatId} was not found.");
            }

            return chatMember;
        }
    }
}
