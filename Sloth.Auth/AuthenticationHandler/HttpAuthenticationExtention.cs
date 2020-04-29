using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Sloth.Auth.AuthenticationHandler
{
    public static class HttpAuthenticationExtention
    {
        public static AuthenticationHeaderValue GetAuthenticationHeaderValue(this IHeaderDictionary headers)
        {
            var authHeader = headers[HeaderNames.Authorization];
            return AuthenticationHeaderValue.Parse(authHeader);
        }
    }
}
