using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sloth.Api.Extensions;
using Sloth.Api.Models;
using Sloth.DB.Repositories;
using Sloth.DB.Repositories.Models;

namespace Sloth.Api.Controllers
{
    [ApiController]
    [Route("api/{chatId}/chat-message")]
    [Authorize]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageRepository _repository;
        private readonly IMapper _mapper;

        public ChatMessageController( IMapper mapper, IChatMessageRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<GetChatMessageResponse> GetMessagesAsync(Guid chatId)
        {
            var messages = (await _repository.GetChatMessagesAsync(chatId, HttpContext.GetCurrentUserId())).ToArray();
            var messagesDto = messages.Select(x => _mapper.Map<ChatMessageDto>(x)).ToArray();

            return new GetChatMessageResponse()
            {
                ChatMessages = messagesDto
            };
        }

        [HttpPost]
        public async Task<ChatMessageDto> SendMessageAsync(Guid chatId, [FromBody] CreateChatMessageRequest request)
        {
            request.UserId = HttpContext.GetCurrentUserId();
            request.ChatId = chatId;

            var messageId = await _repository.SaveChatMessageAsync(request);
            var message = await _repository.GetChatMessageByIdAsync(messageId);

            return _mapper.Map<ChatMessageDto>(message);
        }
    }
}
