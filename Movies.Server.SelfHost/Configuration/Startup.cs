using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using Movies.Server.SelfHost.Configuration;

[assembly: OwinStartup(typeof(Movies.Server.SelfHost.Configuration.Startup))]
namespace Movies.Server.SelfHost.Configuration
{
    /// <summary>
    /// Class that implementes the Owin SelfHost server configurations.    
    /// </summary>
    public class Startup
    {
        #region Public methods
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseCors(CorsOptions.AllowAll);

            ActivateAccessTokensGeneration(app);

            app.UseWebApi(config);
        }
        #endregion

        #region Private methods
        private void ActivateAccessTokensGeneration(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new AccessTokenProvider()
            };
            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
        #endregion
    }
}
