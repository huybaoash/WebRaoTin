using System.Web.Mvc;

namespace WebRaoTin.Areas.Admin
{
    
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new
                {
                    action = "Index",
                    controller = "Home",
                    id = UrlParameter.Optional
                },
                 new[] { "WebRaoTin.Areas.Admin.Controllers" }
            );
        }
    }
}