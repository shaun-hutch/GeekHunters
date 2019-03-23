using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using GeekHunters.Web.Code;

namespace GeekHunters.Web
{
    public class Global : HttpApplication
    {
        public static string dbPath;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            dbPath = HttpContext.Current.Server.MapPath("~");
        }
    }
}