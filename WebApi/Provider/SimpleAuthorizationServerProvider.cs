using System.Security.Claims;
using System.Threading.Tasks;
using Business_Logic.Handlers;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace WebApi.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //This is where you get the token, it checks the active directory and if the user exists it returns a token.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var adHandler = new ActiveDirectoryHandler();
            var user = adHandler.ValidateUserInAd(context.UserName, context.Password, "itm.local");

            if (user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);
            }
            else
            {
                context.SetError("Failed to validate user.");
            }
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
        }
    }
}