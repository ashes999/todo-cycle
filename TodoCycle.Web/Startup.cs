using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodoCycle.Web.Startup))]
namespace TodoCycle.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
