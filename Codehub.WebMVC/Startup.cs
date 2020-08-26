using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Codehub.WebMVC.Startup))]
namespace Codehub.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
