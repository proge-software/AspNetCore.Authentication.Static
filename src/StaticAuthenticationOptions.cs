using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;

namespace ProgeSoftware.AspNetCore.Authentication.Static
{

    /// <summary>
    /// Options class provides information needed to control Static Authentication handler behavior
    /// </summary>
    public class StaticAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string DefaultIdentity { get; set; }

        public Dictionary<string, Identity> Identities { get; set; } = new Dictionary<string, Identity>();

        public string SelectionKey { get; set; } = "ASPNET_AUTH";

        public StaticAuthenticationMethod SelectionMethod { get; set; } = StaticAuthenticationMethod.QueryString;

        /// <summary>
        /// The object provided by the application to process events raised by the static authentication handler.
        /// The application may implement the interface fully, or it may create an instance of StaticAuthenticationEvents
        /// and assign delegates only to the events it wants to process.
        /// </summary>
        public new StaticAuthenticationEvents Events
        {
            get { return (StaticAuthenticationEvents)base.Events; }
            set { base.Events = value; }
        }

        public class Identity : AuthenticationSchemeOptions
        {
            /// <summary>
            /// Gets or sets the AuthenticationType for the authenticated identity.
            /// The default is "Static".
            /// </summary>
            public string AuthenticationType { get; set; } = "Static";

            /// <summary>
            /// Gets or sets the NameIdentifier for the authenticated identity.
            /// The default is an empty Guid.
            /// </summary>
            public string NameIdentifier { get; set; } = Guid.Empty.ToString();

            /// <summary>
            /// Gets or sets the Name to set for the authenticated identity.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the additional Claims to set for the authenticated identity.
            /// </summary>
            public List<Claim> Claims { get; set; }

            public class Claim
            {
                //
                // Summary:
                //     Gets or sets the claim type of the claim.
                //
                // Returns:
                //     The claim type.
                public string Type { get; set; }

                //
                // Summary:
                //     Gets or sets the issuer of the claim.
                //
                // Returns:
                //     A name that refers to the issuer of the claim.
                public string Issuer { get; set; }

                //
                // Summary:
                //     Gets or sets the value type of the claim.
                //
                // Returns:
                //     The claim value type.
                public string ValueType { get; set; }

                //
                // Summary:
                //     Gets or sets the value of the claim.
                //
                // Returns:
                //     The claim value.
                public string Value { get; set; }
            }
        }
    }
}
