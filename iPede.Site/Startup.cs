using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iPede.Site.Startup))]
namespace iPede.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
