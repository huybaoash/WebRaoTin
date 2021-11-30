using GleamTech.DocumentUltimate;
using GleamTech.DocumentUltimate.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebRaoTin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //DocumentUltimateConfiguration.Current.LicenseKey = "QQJDJLJP34...";
            //DocumentUltimateWebConfiguration.Current.CacheLocation = "~/App_Data/DocumentCache";
        }
    }
}
