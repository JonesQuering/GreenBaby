using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HG100.Startup))]
namespace HG100
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
