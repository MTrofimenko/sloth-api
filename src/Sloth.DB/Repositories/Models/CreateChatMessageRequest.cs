using System;
using System.Runtime.Serialization;

namespace Sloth.DB.Repositories.Models
{
    public class CreateChatMessageRequest
    {
        [IgnoreDataMember]
        public Guid ChatId { get; set; }
        [IgnoreDataMember]
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public Guid? ForwardFromUserId { get; set; }
    }
}
