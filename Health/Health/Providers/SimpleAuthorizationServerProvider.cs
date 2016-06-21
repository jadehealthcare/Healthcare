using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Health.Model.ViewModels;
using Health.Model.DBContext;

namespace Health.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
           
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });


            UserRegistration userRegistration = new UserRegistration();
            
                //IdentityUser user = await userRegistration.FindUser(context.UserName, context.Password);
                //IdentityUser user = null;
                //if (context.UserName == userRegistration.Email && context.Password == userRegistration.Password)
                //{
                //    user = new IdentityUser()
                //    {
                //        UserName = userRegistration.Email
                //    };
                //}
                //if (user == null)
                //{
                //    context.SetError("invalid_grant", "The user name or password is incorrect.");
                //    return;
                //}
           

            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", "user"));

            //context.Validated(identity);

        }
    }
}