using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodoCycle.Startup))]
namespace TodoCycle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
