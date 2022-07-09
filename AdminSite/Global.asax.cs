using RWALib.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace AdminSite
{
    public class Global : System.Web.HttpApplication
    {
        private readonly IRepo repo;

        public Global()
        {
            repo = RepoFactory.GetRepo();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["database"] = repo;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}