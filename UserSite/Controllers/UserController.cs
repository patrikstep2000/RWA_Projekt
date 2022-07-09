using RWALib.Models;
using RWALib.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserSite.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepo repo;

        public UserController()
        {
            repo = RepoFactory.GetRepo();
        }

        // GET: User
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Apartments");
            }
            return View((User)Session["user"]);
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["user_email"] != null)
            {
                Response.Cookies["user_email"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["user_pass"] != null)
            {
                Response.Cookies["user_pass"].Expires = DateTime.Now.AddDays(-1);
            }
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index", "Apartments");
        }

        // GET: User/Login
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.IsValid = true;
            return View();
        }

        //POST: User/Login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            User u = repo.AuthUser(email, password);

            if(u == null)
            {
                ViewBag.IsValid = false;
                return View("Login");
            }
            ViewBag.IsAuthorized = true;
            Session["user"] = u;

            HttpCookie cookie_email = new HttpCookie("user_email");
            cookie_email.Value = u.Email;
            cookie_email.Expires = DateTime.Now.AddDays(1);

            HttpCookie cookie_pass = new HttpCookie("user_pass");
            cookie_pass.Value = u.PasswordHash;
            cookie_pass.Expires = DateTime.Now.AddDays(1);

            Response.SetCookie(cookie_email);
            Response.SetCookie(cookie_pass);

            return RedirectToAction("Index", "Apartments");
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.IsValid = true;
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User u)
        {
            try
            {
                repo.AddUser(u);

                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                ViewBag.IsValid = false;
                return View();
            }
        }

        // GET: User/Edit
        public ActionResult Edit()
        {
            if(Session["user"] == null) return RedirectToAction("Index", "Home");

            ViewBag.IsValid = true;
            return View((User)Session["user"]);
        }

        // POST: User/Edit
        [HttpPost]
        public ActionResult Edit(User u)
        {
            try
            {
                repo.SaveUser(u);
                Session["user"] = u;

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.IsValid = false;
                return View();
            }
        }

        // POST: User/Delete
        public ActionResult Delete()
        {
            try
            {
                User u = (User)Session["user"];
                repo.DeleteUser(u.Id);
                return RedirectToAction("Logout");
            }
            catch
            {
                return RedirectToAction("Index", "Error", new {@error="Nije moguce izbrisati korisnika."});
            }
        }
    }
}
