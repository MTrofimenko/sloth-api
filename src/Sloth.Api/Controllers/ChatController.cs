using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sloth.Api.Extensions;
using Sloth.Api.Models;
using Sloth.Api.Services;

namespace Sloth.Api.Controllers
{
    [ApiController]
    [Route("api/chat")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;

        public ChatController(IChatService service)
        {
            _service = service;            
        }

        [HttpGet]
        public async Task<ChatsResponse> GetChats()
        {
            var chats = await _service.GetChatsAsync(HttpContext.GetCurrentUserId());

            return new ChatsResponse()
            {
                Chats = chats
            };
        }

        [HttpPost]
        public async Task<ChatDto> CreateChat([FromBody] CreateChatRequest request)
        {
            var userId = HttpContext.GetCurrentUserId();

            var chatId = await _service.CreateChatAsync(request, userId);
            var chatDto = await _service.GetChatByIdAsync(chatId, userId);

            return chatDto;
        }

        [HttpPost]
        [Route("{chatId}/accept")]
        public async Task<ChatDto> AcceptChat(Guid chatId, [FromBody] ChatActionRequest request)
        {
            var userId = HttpContext.GetCurrentUserId();
            await _service.ConfirmChatAsync(chatId, userId, request.PublicKey);

            var chatDto = await _service.GetChatByIdAsync(chatId, userId);

            return chatDto;
        }

        [HttpPost]
        [Route("{chatId}/decline")]
        public async Task<IActionResult> DeclineChat(Guid chatId)
        {
            await _service.DeclineChatAsync(chatId, HttpContext.GetCurrentUserId());

            return Ok();
        }
    }
}
