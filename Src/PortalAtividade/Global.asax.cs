using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using PortalAtividade.Model;

namespace PortalAtividade
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public void Session_Start(object sender, EventArgs e)
        {
            SessionManager thisSession = new SessionManager(Session.SessionID);
            Session.Add("CurrentSession", thisSession);
        }

        public void Session_End(object sender, EventArgs e)
        {
            Session.Abandon();
        }
    }
}