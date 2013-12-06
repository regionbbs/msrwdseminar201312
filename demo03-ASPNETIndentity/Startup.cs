using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(demo03_ASPNETIndentity.Startup))]
namespace demo03_ASPNETIndentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
