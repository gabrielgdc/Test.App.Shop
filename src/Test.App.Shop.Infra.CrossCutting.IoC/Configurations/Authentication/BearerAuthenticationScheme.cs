using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

namespace Test.App.Shop.Infra.CrossCutting.IoC.Configurations.Authentication;

public class BearerAuthenticationScheme : AuthenticationHandler<BearerAuthenticationSchemeOptions>
{
    public BearerAuthenticationScheme(
        IOptionsMonitor<BearerAuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
    ) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            string authorizationHeader = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            if (!authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            var token = authorizationHeader.Replace("Bearer", "").Trim();

            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            var readToken = tokenHandler.ReadJwtToken(token);

            var claims = new List<Claim>
            {
                new("UserId", readToken.Payload.Sub),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (Exception e)
        {
            Logger.LogCritical("Ocorreu um erro ao autenticar o usuário #### Exception: {0} ####", e.ToString());
            return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
        }
    }
}

public class BearerAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
}
