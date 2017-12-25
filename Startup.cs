using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(online_store.Startup))]
namespace online_store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
