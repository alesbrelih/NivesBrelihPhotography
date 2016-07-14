using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NivesBrelihPhotography.Startup))]
namespace NivesBrelihPhotography
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
