using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GradProj.Startup))]
namespace GradProj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
