using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodoCycle.WebMvc.Startup))]
namespace TodoCycle.WebMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
