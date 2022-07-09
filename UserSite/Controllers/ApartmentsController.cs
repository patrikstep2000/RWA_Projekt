﻿using RWALib.Models;
using RWALib.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserSite.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IRepo repo;

        public ApartmentsController()
        {
            repo = RepoFactory.GetRepo();
        }

        // GET: Apartments
        public ActionResult Index()
        {
            List<Apartment> apartments = repo.LoadApartments().ToList();

            if (Request.Cookies["city"] == null || Request.Cookies["adults"] == null || Request.Cookies["children"] == null || Request.Cookies["rooms"] == null || Request.Cookies["sort"] == null)
            {
                apartments.ToList().ForEach(x =>
                {
                    if (x.DeletedAt != "" || x.Status != "Slobodno")
                    {
                        apartments.Remove(x);
                    }
                });
                SetDefaultViewBag();
            }
            else
            {
                try
                {
                    FilterApartments(apartments, Request.Cookies["city"].Value, int.Parse(Request.Cookies["adults"].Value), int.Parse(Request.Cookies["children"].Value), int.Parse(Request.Cookies["rooms"].Value), int.Parse(Request.Cookies["sort"].Value));
                }
                catch
                {
                    apartments.ToList().ForEach(x =>
                    {
                        if (x.DeletedAt != "" || x.Status != "Slobodno")
                        {
                            apartments.Remove(x);
                        }
                    });
                    SetDefaultViewBag();
                }
            }

            ViewBag.Cities = repo.GetCities();
            
            return View(apartments);
        }

        public ActionResult Details(int id)
        {
            Apartment a = repo.GetApartmentById(id);
            ViewBag.Tags = repo.LoadTagsForApartment(id);
            return View(a);
        }

        [HttpPost]
        public ActionResult Filter(string city, int adults, int children, int rooms, int sort)
        {
            List<Apartment> apartments = repo.LoadApartments().ToList();

            FilterApartments(apartments, city, adults, children, rooms, sort);

            SetOrCreateCookie("city", city);
            SetOrCreateCookie("adults", adults.ToString());
            SetOrCreateCookie("children", children.ToString());
            SetOrCreateCookie("rooms", rooms.ToString());
            SetOrCreateCookie("sort", sort.ToString());

            return PartialView("_ListApartment", apartments);
        }        

        public ActionResult Reset()
        {
            DeleteCookie("city");
            DeleteCookie("adults");
            DeleteCookie("children");
            DeleteCookie("rooms");
            DeleteCookie("sort");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult BookExisting(int userId, int apartmentId, string details)
        {
            try
            {
                repo.AddReservationForExistingUser(userId, apartmentId, details);
            }
            catch
            {
                return Json("Booking unsuccesful");
            }
            return Content("Your booking was sent to the administrator.");
        }

        [HttpPost]
        public ActionResult BookNonExisting(int apartmentId, string details, string username, string userEmail, string phone)
        {
            try
            {
                repo.AddReservationForNonExistingUser(apartmentId, details, username, userEmail, phone);
            }
            catch
            {
                return Json("Booking unsuccesful");
            }
            return Json("Your booking was sent to the administrator.");
        }

        private void DeleteCookie(string name)
        {
            if (Request.Cookies[name] != null)
            {
                Response.Cookies[name].Expires = DateTime.Now.AddDays(-1);
            }
        }

        private void FilterApartments(List<Apartment> apartments, string city, int adults, int children, int rooms, int sort)
        {
            apartments.ToList().ForEach(a =>
            {
                if (a.DeletedAt != "" || a.Status != "Slobodno" || a.City != city || a.MaxAdults != adults || a.MaxChildren != children || a.TotalRooms != rooms)
                {
                    apartments.Remove(a);
                }
            });
            switch (sort)
            {
                case 1:
                    apartments.Sort((a, b) => a.Price.CompareTo(b.Price));
                    break;
                case 2:
                    apartments.Sort((a, b) => -a.Price.CompareTo(b.Price));
                    break;
                default:
                    apartments.Sort((a, b) => a.Id.CompareTo(b.Id));
                    break;
            }
            ViewBag.City = city;
            ViewBag.Adults = adults;
            ViewBag.Children = children;
            ViewBag.Rooms = rooms;
            ViewBag.Sort = sort;
        }

        private void SetOrCreateCookie(string name, string value)
        {
            if (Request.Cookies[name] != null)
            {
                Response.Cookies[name].Value = value;
            }
            else
            {
                CreateCookie(name, value);
            }
        }

        private void CreateCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(cookie);
        }

        private void SetDefaultViewBag()
        {
            ViewBag.City = "Zagreb";
            ViewBag.Adults = 1;
            ViewBag.Children = 0;
            ViewBag.Rooms = 1;
            ViewBag.Sort = 0;
        }
    }
}