using Microsoft.Owin;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;

[assembly: OwinStartup(typeof(Movies.Server.SelfHost.Configuration.Startup))]
namespace Movies.Server.SelfHost.Configuration
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config);
        }
    }
}
