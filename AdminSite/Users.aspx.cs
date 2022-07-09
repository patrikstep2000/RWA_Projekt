using RWALib.Models;
using RWALib.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class Users : System.Web.UI.Page
    {
        private IList<User> _users;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null || (string)Session["user"] != "admin")
            {
                Response.Redirect("Default.aspx");
            }
            _users = ((IRepo)Application["database"]).LoadUsers();
            Repeater.DataSource = _users;
            Repeater.DataBind();
        }
    }
}