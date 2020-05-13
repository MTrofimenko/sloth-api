using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sloth.Auth.Models
{
    public class SlothAuthenticationOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SwaggerClientId { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public int TokenTimeout { get; set; }
        public int RefreshTokenTimeout { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ApiSecret));
        }
    }
}
