using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ProgeSoftware.AspNetCore.Authentication.Static
{
    public class StaticAuthenticationHandler : AuthenticationHandler<StaticAuthenticationOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaticAuthenticationHandler(IOptionsMonitor<StaticAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IHttpContextAccessor httpContextAccessor)
            : base(options, logger, encoder, clock)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var options = Options.Identities[GetIdentityKey()];
            var identity = new ClaimsIdentity(authenticationType: options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, options.NameIdentifier));
            identity.AddClaim(new Claim(ClaimTypes.Name, options.Name));
            var claims = new List<Claim>();
            foreach (var claim in options.Claims)
            {
                if (claim.ValueType != null && claim.Issuer != null)
                    claims.Add(new Claim(claim.Type, claim.Value, claim.ValueType, claim.Issuer));
                else if (claim.ValueType != null)
                    claims.Add(new Claim(claim.Type, claim.Value, claim.ValueType));
                else
                    claims.Add(new Claim(claim.Type, claim.Value));
            };
            identity.AddClaims(claims);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, StaticAuthenticationDefaults.AuthenticationScheme);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private string GetIdentityKey()
        {
            var key = Options.DefaultIdentity;
            try
            {
                switch (Options.SelectionMethod)
                {
                    case StaticAuthenticationMethod.QueryString:
                        if (_httpContextAccessor.HttpContext.Request.Query.ContainsKey(Options.SelectionKey))
                        {
                            key = _httpContextAccessor.HttpContext.Request.Query[Options.SelectionKey].ToString();
                        }
                        break;
                    case StaticAuthenticationMethod.Form:
                        if (_httpContextAccessor.HttpContext.Request.Form.ContainsKey(Options.SelectionKey))
                        {
                            key = _httpContextAccessor.HttpContext.Request.Form[Options.SelectionKey].ToString();
                        }
                        break;
                    case StaticAuthenticationMethod.Header:
                        if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey(Options.SelectionKey))
                        {
                            key = _httpContextAccessor.HttpContext.Request.Headers[Options.SelectionKey].ToString();
                        }
                        break;
                }
            }
            catch { }
            if (string.IsNullOrEmpty(key))
            {
                key = Options.Identities.FirstOrDefault().Key;
            }
            return key;
        }
    }
}
