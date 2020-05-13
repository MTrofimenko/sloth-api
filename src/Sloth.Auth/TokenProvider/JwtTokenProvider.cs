using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sloth.Auth.Models;
using Sloth.DB.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Sloth.Auth.TokenProvider
{
    public class JwtTokenProvider : ITokenProvider
    {
        private readonly SlothAuthenticationOptions _authenticationOptions;
        public JwtTokenProvider(IOptions<SlothAuthenticationOptions> authenticationOptions) {
            _authenticationOptions = authenticationOptions.Value;
        }
        public DateTime TokenExpires => DateTime.UtcNow.AddMinutes(_authenticationOptions.TokenTimeout);
        public DateTime RefreshTokenExpires => DateTime.UtcNow.AddMinutes(_authenticationOptions.RefreshTokenTimeout);

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Sid, user.Id.ToString())
                }),
                Expires = TokenExpires,
                Issuer = _authenticationOptions.ApiName,
                Audience = _authenticationOptions.ClientId,
                SigningCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public ClaimsPrincipal IntrospectToken(string token, out SecurityToken validatedToken)
        {
            var validationParameters = _authenticationOptions.TokenValidationParameters.Clone();
            validationParameters.ValidateLifetime = false;
            return new JwtSecurityTokenHandler().ValidateToken(token.Replace("Bearer ", ""), validationParameters, out validatedToken);
        }
    }
}
