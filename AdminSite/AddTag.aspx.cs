﻿using RWALib.Models;
using RWALib.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class AddTag : System.Web.UI.Page
    {
        private IList<TagType> _tagTypes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["user"] != "admin")
            {
                Response.Redirect("Default.aspx");
            }
            _tagTypes = ((IRepo)Application["database"]).LoadTagTypes();
            if (!IsPostBack)
            {
                AppendData();
            }
        }

        private void AppendData()
        {
            ddlType.DataSource = _tagTypes;
            ddlType.DataValueField = "Id";
            ddlType.DataTextField = "Name";
            ddlType.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).AddTag(new Tag
            {
                Name = txtName.Text,
                NameEng = txtEngName.Text,
                Type = ddlType.SelectedItem.Text
            });
            Response.Redirect("Tags.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tags.aspx");
        }
    }
}