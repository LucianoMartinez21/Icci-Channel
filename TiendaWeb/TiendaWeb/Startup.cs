using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TiendaWeb.Startup))]
namespace TiendaWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
