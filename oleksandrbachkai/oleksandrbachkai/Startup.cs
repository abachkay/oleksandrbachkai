using Microsoft.Owin;
using Owin;
using oleksandrbachkai.App_Start;


[assembly: OwinStartup(typeof(oleksandrbachkai.Startup))]

namespace oleksandrbachkai
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
