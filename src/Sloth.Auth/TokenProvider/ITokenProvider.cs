using Microsoft.IdentityModel.Tokens;
using Sloth.DB.Models;
using System;
using System.Security.Claims;

namespace Sloth.Auth.TokenProvider
{
    public interface ITokenProvider
    {
        DateTime TokenExpires { get; }
        DateTime RefreshTokenExpires { get; }
        string CreateToken(User user);
        ClaimsPrincipal IntrospectToken(string token, out SecurityToken validatedToken);
        string CreateRefreshToken();
    }
}
