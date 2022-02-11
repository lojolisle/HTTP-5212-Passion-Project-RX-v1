using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HTTP_5212_Passion_Project_RX_v1.Startup))]
namespace HTTP_5212_Passion_Project_RX_v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
