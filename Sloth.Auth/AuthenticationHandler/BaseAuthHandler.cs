using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Sloth.Auth.Models;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Sloth.Auth.AuthenticationHandler
{
    public class BaseAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthService _authService;
        private readonly SlothAuthenticationOptions _authenticationOptions;
        public BaseAuthHandler(
           IAuthService authService,
           IOptionsMonitor<AuthenticationSchemeOptions> options,
           IOptions<SlothAuthenticationOptions> authenticationOptions,
           ILoggerFactory logger,
           UrlEncoder encoder,
           ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _authService = authService;
            _authenticationOptions = authenticationOptions.Value;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                Logger.LogInformation("No Authorization header found");
                return AuthenticateResult.NoResult();
            }

            try
            {
                //var authenticationToken = Request.Headers.GetAuthenticationHeaderValue().Parameter;
                var authenticationToken = Request.Headers[HeaderNames.Authorization];
                ClaimsPrincipal introspectionResponse = await _authService.IntrospectTokenAsync(authenticationToken);

                return HandleRequestResult.Success(new AuthenticationTicket(new ClaimsPrincipal(introspectionResponse), Scheme.Name));
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, "Authentication failed");

                return AuthenticateResult.Fail(ex);
            }
        }
    }
}
