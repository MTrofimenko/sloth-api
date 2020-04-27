using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sloth.Api.Models;
using Sloth.Api.Services;

namespace Sloth.Api.Controllers
{
    [ApiController]
    [Route("api/{chatId}/chat-message")]
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageService _service;
        private static readonly Guid AliceId = new Guid("D97E9580-BDD7-4083-9188-B341C7A51788");
       
        public ChatMessageController(IChatMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<GetChatMessageResponse> GetMessagesAsync(Guid chatId)
        {
            var messages = await _service.GetChatMessagesAsync(chatId, AliceId);

            return new GetChatMessageResponse()
            {
                ChatMessages = messages
            };
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync(Guid chatId, [FromBody] CreateChatMessageRequest request)
        {
            await _service.SaveChatMessageAsync(chatId, request, AliceId);

            return Ok();
        }
    }
}
