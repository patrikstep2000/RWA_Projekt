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
    public partial class Apartments : System.Web.UI.Page
    {
        private static IList<City> _allCities;
        private static IList<Status> _allStatuses;
        private static IList<Apartment> _apartments;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            
            if (!IsPostBack)
            {
                _allCities = ((IRepo)Application["database"]).GetCities();
                _allStatuses = ((IRepo)Application["database"]).GetStatus();
                AppendData();
            }
            FilterApartments();
        }

        private void AppendData()
        {
            _allCities.Insert(0, new City
            {
                Id=9999,
                Name="Odaberi"
            });
            ddlCityFilter.DataSource = _allCities;
            ddlCityFilter.DataTextField = "Name";
            ddlCityFilter.DataValueField = "Id";
            ddlCityFilter.DataBind();

            _allStatuses.Insert(0, new Status
            {
                Id = 9999,
                Name = "Odaberi"
            });
            ddlStatusFilter.DataSource = _allStatuses;
            ddlStatusFilter.DataTextField = "Name";
            ddlStatusFilter.DataValueField = "Id";
            ddlStatusFilter.DataBind();
        }

        private void RemoveDeletedApartments()
        {
            _apartments.ToList().ForEach(x =>
            {
                if (x.DeletedAt != null) _apartments.Remove(x);
            });
        }


        protected void btnUredi_Click(object sender, EventArgs e)
        {
            pnlApartments.Visible = false;

            Button btn = (Button)sender;
            int selectedId = int.Parse(btn.CommandArgument);

            Session["apartment_id"] = selectedId;
            Response.Redirect("EditApartment.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddApartment.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            ((IRepo)Application["database"]).DeleteApartment(id);
            Response.Redirect("Apartments.aspx");
        }

        private void FilterApartments()
        {
            string status = ddlStatusFilter.SelectedItem.Text;
            string city = ddlCityFilter.SelectedItem.Text;
            _apartments = ((IRepo)Application["database"]).LoadApartmentsByCityAndStatus(status, city);

            RemoveDeletedApartments();
            Repeater.DataSource = _apartments;
            Repeater.DataBind();
        }
    }
}