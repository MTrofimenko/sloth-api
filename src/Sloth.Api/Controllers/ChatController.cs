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
        public async Task<IActionResult> CreateChat([FromBody] CreateChatRequest request)
        {
            var chatId = await _service.CreateChatAsync(request, HttpContext.GetCurrentUserId());

            return Ok(chatId);
        }

        [HttpPost]
        [Route("{chatId}/accept")]
        public async Task<IActionResult> AcceptChat(Guid chatId, [FromBody] ChatActionRequest request)
        {
            await _service.ConfirmChatAsync(chatId, HttpContext.GetCurrentUserId(), request.PublicKey);

            return Ok();
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
