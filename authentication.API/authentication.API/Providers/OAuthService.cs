using authentication.API.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace authentication.API.Providers
{
    public class OAuthService : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
        context.Validated();
    }

    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {

        //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

        using (AuthRepository _repo = new AuthRepository())
        {
            string message = "";
            bool result = _repo.Login(context.UserName, context.Password, ref message);

            if (!result)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
        }

        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
        identity.AddClaim(new Claim("sub", context.UserName));
        context.Validated(identity);

    }
}
}