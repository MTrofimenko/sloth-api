using System;
using System.Collections.Generic;

namespace Sloth.Api.Models
{
    public class CreateChatDto
    {
        public string Name { get; set; }
        public IEnumerable<Guid> MemberIds { get; set; }
        public string CreatorPublicKey { get; set; }
    }
}
