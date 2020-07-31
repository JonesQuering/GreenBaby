using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HGCapNewTemp.Startup))]
namespace HGCapNewTemp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
