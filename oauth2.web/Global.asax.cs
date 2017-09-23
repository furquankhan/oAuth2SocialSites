using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using oauth2;
using oauth2.services;

namespace oauth2.web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            web.Extensions.ExtensionMethods.installpath = Server.MapPath("~");
            // Code that runs on application startup
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AuthConfig.RegisterOpenAuth();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            GridConfig.loadconfig();
        }

        void Application_End(object sender, EventArgs e)
        {

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }
    }
}
