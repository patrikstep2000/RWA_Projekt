using RWALib.Models;
using RWALib.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UserSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Session_Start(object sender, EventArgs e)
        {
            if (Request.Cookies["user_email"] != null && Request.Cookies["user_pass"] != null)
            {
                string email = Request.Cookies["user_email"].Value;
                string password = Request.Cookies["user_pass"].Value;
                User u = RepoFactory.GetRepo().AuthUserWithoutHash(email, password);

                if (u != null)
                {
                    Session["user"] = u;
                }
            }
        }
    }
}
