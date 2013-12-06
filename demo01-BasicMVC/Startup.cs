using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(demo01_BasicMVC.Startup))]
namespace demo01_BasicMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
