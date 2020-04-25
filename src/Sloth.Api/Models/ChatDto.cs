using System;
using System.Collections.Generic;
using System.Linq;
using Sloth.DB.Models;

namespace Sloth.Api.Models
{
    public class ChatDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ChatStatus Status { get; set; }

        public IEnumerable<ChatMemberDto> Members = Enumerable.Empty<ChatMemberDto>();
    }
}
