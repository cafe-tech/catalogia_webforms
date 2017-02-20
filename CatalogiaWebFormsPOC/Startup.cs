using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CatalogiaWebForms.Startup))]
namespace CatalogiaWebForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
