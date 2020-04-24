using System;
using Sloth.DB.Models;

namespace Sloth.Api.Models
{
    public class ChatMemberDto
    {
        public Guid UserId { get; set; }
        public ChatMemberStatus Status { get; set; }
        public string PublicKey { get; set; }
    }
}
