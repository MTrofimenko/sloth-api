using System;
using System.Collections.Generic;

namespace Sloth.Api.Models
{
    public class CreateChatRequest
    {
        public string Name { get; set; }
        public IEnumerable<Guid> MemberIds { get; set; }
        public string CreatorPublicKey { get; set; }
    }
}
