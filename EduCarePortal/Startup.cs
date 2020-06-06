using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EduCarePortal.Startup))]
namespace EduCarePortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
