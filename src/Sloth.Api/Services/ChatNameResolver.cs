using System;
using System.Linq;
using Sloth.Common.Exceptions;
using Sloth.DB.Models;

namespace Sloth.Api.Services
{
    public class ChatNameResolver : IChatNameResolver
    {
        public string GetChatNameForUser(Chat chat, Guid userId)
        {
            if (!string.IsNullOrWhiteSpace(chat.Name))
            {
                return chat.Name;
            }

            var interlocutor = chat.Members.FirstOrDefault(x => x.UserId != userId)?.User;
            if (interlocutor == null)
            {
                throw new SlothException($"Can't find interlocutor for chat {chat.Id} and userId {userId}.");
            }

            return $"{interlocutor.FirstName} {interlocutor.LastName}".Trim();
        }
    }
}
