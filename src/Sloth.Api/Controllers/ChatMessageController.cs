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
    [Route("api/{chatId}/chat-message")]
    [Authorize]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _service;
       
        public ChatMessageController(IChatMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<GetChatMessageResponse> GetMessagesAsync(Guid chatId)
        {
            var messages = await _service.GetChatMessagesAsync(chatId, HttpContext.GetCurrentUserId());

            return new GetChatMessageResponse()
            {
                ChatMessages = messages
            };
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync(Guid chatId, [FromBody] CreateChatMessageRequest request)
        {
            var messageId = await _service.SaveChatMessageAsync(chatId, request, HttpContext.GetCurrentUserId());

            return Ok(messageId);
        }
    }
}
