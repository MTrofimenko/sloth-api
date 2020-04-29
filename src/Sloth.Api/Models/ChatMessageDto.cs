using System;

namespace Sloth.Api.Models
{
    public class ChatMessageDto
    {
        public string Message { get; set; }
        public Guid ChatMemberId { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public Guid? ForwardFromUserId { get; set; }
    }
}
