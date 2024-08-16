using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortalAtividade.Startup))]
namespace PortalAtividade
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            //ConfigureAuth(app);
        }
    }
}
