using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sloth.Api.Models;
using Sloth.Common.Exceptions;
using Sloth.DB;
using Sloth.DB.Models;
using Sloth.DB.Repositories;

namespace Sloth.Api.Services
{
    public class ChatService : IChatService
    {
        private readonly ISlothDbContext _dbContext;
        private readonly IChatRepository _repository;
        private readonly IChatNameResolver _nameResolver;
        private readonly IMapper _mapper;

        public ChatService(ISlothDbContext dbContext, IMapper mapper, IChatRepository repository, IChatNameResolver nameResolver)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _repository = repository;
            _nameResolver = nameResolver;
        }

        public async Task<Guid> CreateChatAsync(CreateChatRequest request, Guid userId)
        {
            var chat = await _repository.CreateChatAsync(request.Name);

            foreach (var memberId in request.MemberIds)
            {
                await _repository.CreateChatMemberAsync(chat.Id, memberId);
            }

            // Add creator as a member 
            await _repository.CreateChatMemberAsync(chat.Id, userId, ChatMemberStatus.Active, request.CreatorPublicKey);

            return chat.Id;
        }

        public async Task<ChatDto> GetChatByIdAsync(Guid chatId, Guid userId)
        {
            var chat = await _repository.GetChatByIdAsync(chatId);

            return ToChatDto(chat, userId);
        }

        public async Task<IEnumerable<ChatDto>> GetChatsAsync(Guid userId)
        {
            var chats = (await _repository.GetChatsByUserIdAsync(userId))
                .Select(x=> ToChatDto(x, userId))
                .ToArray();

            return chats;
        }

        public async Task ConfirmChatAsync(Guid chatId, Guid userId, string publicKey)
        {
            var (chat, chatMember) = await GetPendingChatEntities(chatId, userId);

            chatMember.Status = ChatMemberStatus.Active;
            chatMember.PublicKey = publicKey;

            chat.Status = ChatStatus.Active;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeclineChatAsync(Guid chatId, Guid userId)
        {
            var (chat, chatMember) = await GetPendingChatEntities(chatId, userId);

            chatMember.Status = ChatMemberStatus.Aborted;
            chat.Status = ChatStatus.Aborted;

            await _dbContext.SaveChangesAsync();
        }

        private async Task<(Chat chat, ChatMember chatMember)> GetPendingChatEntities(Guid chatId, Guid userId)
        {
            var chat = await _repository.GetChatByIdAsync(chatId);

            if (chat.Status != ChatStatus.Pending)
            {
                throw new SlothException($"Chat {chatId} is not in pending status.");
            }

            var chatMember = await _repository.GetChatMemberAsync(chatId, userId);

            if (chatMember.Status != ChatMemberStatus.Pending)
            {
                throw new SlothException($"Chat member {chatMember.Id} for chatId {chatId} was not in pending state.");
            }

            return (chat, chatMember);
        }

        private ChatDto ToChatDto(Chat chat, Guid userId)
        {
            var chatName = _nameResolver.GetChatNameForUser(chat, userId);

            return new ChatDto()
            {
                Id = chat.Id,
                Name = chatName,
                Status = chat.Status,
                Members = chat.Members
                    .Where(x => x.Status != ChatMemberStatus.Removed)
                    .Select(x=> _mapper.Map<ChatMemberDto>(x))
            };
        }
    }
}