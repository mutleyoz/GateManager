using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GateManager.Startup))]
namespace GateManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
