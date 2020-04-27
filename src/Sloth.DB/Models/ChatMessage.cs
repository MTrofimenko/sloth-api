using System;
namespace Sloth.DB.Models
{
    public class ChatMessage : BaseEntity
    {
        public string Message { get; set; }
        public Guid ChatMemberId { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public Guid? ForwardFromUserId { get; set; }

        // Navigation Properties
        public ChatMember Sender { get; set; }
        public ChatMessage ReplyToMessage { get; set; }
        public User ForwardFromUser { get; set; }
    }
}
