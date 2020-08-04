using Microsoft.Owin.Security.OAuth;
using Movies.Business;
using Movies.Server.SelfHost.Common;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movies.Server.SelfHost.Configuration
{
    /// <summary>
    /// Class that provides the authentication token for the registered users
    /// </summary>
    public class AccessTokenProvider : OAuthAuthorizationServerProvider
    {
        #region Fields
        private readonly UserBusiness _userBusiness = new UserBusiness();
        #endregion

        #region Public methods
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (_userBusiness.Login(context.UserName, context.Password))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                context.Validated(identity);
            }
            else
            {
                context.SetError(Consts.C_INVALID_ACCESS, Consts.C_INVALID_ACCESS_MESSAGE);
                return;
            }
        }
        #endregion
    }
}