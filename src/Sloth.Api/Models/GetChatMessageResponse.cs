using System.Collections.Generic;
using System.Linq;

namespace Sloth.Api.Models
{
    public class GetChatMessageResponse
    {
        public IEnumerable<ChatMessageDto> ChatMessages = Enumerable.Empty<ChatMessageDto>();
    }
}
