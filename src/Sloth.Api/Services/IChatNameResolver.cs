using System;
using Sloth.DB.Models;

namespace Sloth.Api.Services
{
    public interface IChatNameResolver
    {
        string GetChatNameForUser(Chat chat, Guid userId);
    }
}
