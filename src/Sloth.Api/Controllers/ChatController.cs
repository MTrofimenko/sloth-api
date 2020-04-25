using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sloth.Api.Models;
using Sloth.Api.Services;

namespace Sloth.Api.Controllers
{
    [ApiController]
    [Route("api/chat")]
    // Authorize
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;
        private static readonly Guid AliceId = new Guid("D97E9580-BDD7-4083-9188-B341C7A51788");
        private static readonly Guid BobId = new Guid("486ED1F6-8A01-483A-B3F0-0DAFAFF0741B");

        public ChatController(IChatService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ChatsResponse> GetChats()
        {
            var chats = await _service.GetChatsAsync(AliceId);

            return new ChatsResponse()
            {
                Chats = chats
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDto request)
        {
            var chatId = await _service.CreateChatAsync(request, AliceId);

            return Ok(chatId);
        }

        [HttpPost]
        [Route("{chatId}/confirm")]
        public async Task<IActionResult> ConfirmChat(Guid chatId, [FromBody] ChatActionDto request)
        {
            await _service.ConfirmChatAsync(chatId, BobId, request.PublicKey);

            return Ok();
        }

        [HttpPost]
        [Route("{chatId}/decline")]
        public async Task<IActionResult> DeclineChat(Guid chatId)
        {
            await _service.DeclineChatAsync(chatId, AliceId);

            return Ok();
        }
    }
}
