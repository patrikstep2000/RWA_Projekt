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
    public partial class EditApartment : System.Web.UI.Page
    {
        private static IList<City> _allCities;
        private static IList<Status> _allStatuses;
        private static IList<Owner> _allOwners;
        private static IList<Tag> _allTags;
        private static ISet<Tag> _selectedtags;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["user"] != "admin")
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                _allTags = ((IRepo)Application["database"]).LoadTags();
                _allCities = ((IRepo)Application["database"]).GetCities();
                _allStatuses = ((IRepo)Application["database"]).GetStatus();
                _allOwners = ((IRepo)Application["database"]).GetOwners();
                
                if (Session["apartment_id"] != null)
                {
                    int selected_id = (int)Session["apartment_id"];
                    _selectedtags = new HashSet<Tag>(((IRepo)Application["database"]).LoadTagsForApartment(selected_id));
                    _selectedtags.ToList().ForEach(t => _allTags.Remove(t));
                    AppendData();
                    rptTags.DataSource = _selectedtags;
                    rptTags.DataBind();
                    FillForm(selected_id); 
                }
            }
        }

        private void FillForm(int id)
        {
            Apartment a = ((IRepo)Application["database"]).GetApartmentById(id);

            txtName.Text = a.Name;
            txtEngName.Text = a.NameEng;
            txtAddress.Text = a.Address;
            txtBeachDistance.Text = a.BeachDistance.ToString();
            txtChildren.Text = a.MaxChildren.ToString();
            txtAdults.Text = a.MaxAdults.ToString();
            txtPrice.Text = a.Price.ToString();
            txtRoomsCount.Text = a.TotalRooms.ToString();
            ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByText(a.City));
            ddlOwner.SelectedIndex = ddlOwner.Items.IndexOf(ddlOwner.Items.FindByText(a.Owner));
            ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByText(a.Status));
        }

        private void AppendData()
        {
            ddlTags.DataSource = _allTags;
            ddlTags.DataValueField = "Id";
            ddlTags.DataTextField = "Name";
            ddlTags.DataBind();

            ddlCity.DataSource = _allCities;
            ddlCity.DataValueField = "Id";
            ddlCity.DataTextField = "Name";
            ddlCity.DataBind();

            ddlStatus.DataSource = _allStatuses;
            ddlStatus.DataValueField = "Id";
            ddlStatus.DataTextField = "Name";
            ddlStatus.DataBind();

            ddlOwner.DataSource = _allOwners;
            ddlOwner.DataValueField = "Id";
            ddlOwner.DataTextField = "Name";
            ddlOwner.DataBind();
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            Tag t = _allTags.FirstOrDefault(x => x.Name == ddlTags.SelectedItem.Text);
            if (_selectedtags.Add(t))
            {
                _allTags.Remove(t);
            }

            ddlTags.DataSource = _allTags;
            ddlTags.DataValueField = "Id";
            ddlTags.DataTextField = "Name";
            ddlTags.DataBind();

            rptTags.DataSource = _selectedtags;
            rptTags.DataBind();
        }

        protected void btnOdustani_Click(object sender, EventArgs e)
        {
            _selectedtags.Clear();
            _allCities.Clear();
            _allOwners.Clear();
            _allStatuses.Clear();
            _selectedtags.Clear();
            Response.Redirect("Apartments.aspx");
        }

        protected void btnSpremi_Click(object sender, EventArgs e)
        {
            ((IRepo)Application["database"]).SaveApartment(new Apartment
            {
                Id = (int)Session["apartment_id"],
                Name = txtName.Text,
                NameEng = txtEngName.Text,
                Address = txtAddress.Text,
                MaxAdults = int.Parse(txtAdults.Text),
                MaxChildren = int.Parse(txtChildren.Text),
                City = ddlCity.SelectedItem.Text,
                Owner = ddlOwner.SelectedItem.Text,
                Status = ddlStatus.SelectedItem.Text,
                Price = decimal.Parse(txtPrice.Text),
                TotalRooms = int.Parse(txtRoomsCount.Text)
            });
            _selectedtags.ToList().ForEach(t => ((IRepo)Application["database"]).AddTaggedApartment(txtName.Text, t.Name));
            _allTags.ToList().ForEach(t => ((IRepo)Application["database"]).DeleteTaggedApartment(txtName.Text, t.Name));
            _selectedtags.Clear();
            Session["apartment_id"] = null;
            Response.Redirect("Apartments.aspx");
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            string name = button.CommandArgument;
            Tag t = _selectedtags.FirstOrDefault(x => x.Name == name);
            _allTags.Add(t);
            _selectedtags.Remove(t);
            
            ddlTags.DataSource = _allTags;
            ddlTags.DataValueField = "Id";
            ddlTags.DataTextField = "Name";
            ddlTags.DataBind();
            

            rptTags.DataSource = _selectedtags;
            rptTags.DataBind();
        }
    }
}