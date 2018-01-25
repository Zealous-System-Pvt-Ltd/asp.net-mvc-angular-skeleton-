// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicAuthProvider.cs" company="zealous">
//   MIT License
// </copyright>
// <summary>
//   The basic authentication provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Demo.Business.Contracts;
using Demo.Data.SecurityDomainModel;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.ServiceLocation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Demo.Web.AuthProviders
{
    /// <summary>
    /// The basic authentication provider.
    /// </summary>
    public class BasicAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var securityManager = ServiceLocator.Current.GetInstance<ISecurityManager>();
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            var result = await securityManager.ValidateUser(new LoginModel { UserName = context.UserName, Password = context.Password });
            if (!result.Exceptions.Any())
            {
                context.Validated((ClaimsIdentity)result.Response);
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}