using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Sloth.Api.Extensions
{
    public static  class HttpContextExtension
    {
        public static Guid GetCurrentUserId(this HttpContext httpContext) {
            return new Guid(httpContext.User.FindFirstValue(ClaimTypes.Sid));
        }
    }
}
