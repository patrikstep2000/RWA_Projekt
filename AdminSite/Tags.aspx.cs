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
    public partial class Tags : System.Web.UI.Page
    {
        private IList<Tag> tags;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["user"] != "admin")
            {
                Response.Redirect("Default.aspx");
            }
            tags = ((IRepo)Application["database"]).LoadTags();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptTags.DataSource = tags;
            rptTags.DataBind();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int tagID = int.Parse(btn.CommandArgument);

            gvAparments.DataSource = ((DBRepo)Application["database"]).LoadApartmentsByTagID(tagID);
            gvAparments.DataBind();
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTag.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            var tagID = int.Parse(button.CommandArgument);

            ((IRepo)Application["database"]).DeleteTag(tagID);
            Response.Redirect("Tags.aspx");
        }
    }
}