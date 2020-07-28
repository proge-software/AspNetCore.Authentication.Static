using Microsoft.AspNetCore.Authentication;
using ProgeSoftware.AspNetCore.Authentication.Static;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StaticAuthenticationExtensions
    {
        public static AuthenticationBuilder AddStatic(this AuthenticationBuilder builder)
            => builder.AddStatic(StaticAuthenticationDefaults.AuthenticationScheme);

        public static AuthenticationBuilder AddStatic(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddStatic(authenticationScheme, configureOptions: null);

        public static AuthenticationBuilder AddStatic(this AuthenticationBuilder builder, Action<StaticAuthenticationOptions> configureOptions)
            => builder.AddStatic(StaticAuthenticationDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddStatic(this AuthenticationBuilder builder, string authenticationScheme, Action<StaticAuthenticationOptions> configureOptions)
            => builder.AddStatic(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddStatic(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<StaticAuthenticationOptions> configureOptions)
        {
            builder.Services.AddHttpContextAccessor();
            return builder.AddScheme<StaticAuthenticationOptions, StaticAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
