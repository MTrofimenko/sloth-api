using System.Collections.Generic;

namespace Sloth.Api.Models
{
    public class ChatsResponse
    {
        public IEnumerable<ChatDto> Chats { get; set; }
    }
}
