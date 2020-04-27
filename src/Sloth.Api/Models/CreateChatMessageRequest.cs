using System;

namespace Sloth.Api.Models
{
    public class CreateChatMessageRequest
    {
        public string Message { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public Guid? ForwardFromUserId { get; set; }
    }
}
