using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EZPZPOS.MVC.Startup))]
namespace EZPZPOS.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
