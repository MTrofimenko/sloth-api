using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sloth.Auth.Models
{
    public class SlothAuthenticationOptions
    {
        private TokenValidationParameters _tokenValidationParameters;
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SwaggerClientId { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public int TokenTimeout { get; set; }
        public int RefreshTokenTimeout { get; set; }
        public TokenValidationParameters TokenValidationParameters { get {
                if (_tokenValidationParameters == null) {
                    _tokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = GetSymmetricSecurityKey(),

                        ValidateIssuer = true,
                        ValidIssuer = ApiName,

                        ValidateAudience = true,
                        ValidAudiences = new[] { ClientId, SwaggerClientId },

                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                }
                return _tokenValidationParameters;
            } }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ApiSecret));
        }
    }
}
