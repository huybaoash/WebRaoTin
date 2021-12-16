using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebRaoTin.Startup))]
namespace WebRaoTin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //thêm dong code này nhé các bạn
            app.MapSignalR();
        }
    }
}
