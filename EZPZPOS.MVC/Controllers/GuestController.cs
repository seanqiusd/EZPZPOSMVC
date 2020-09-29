using EZPZPOS.Models.GuestModels;
using EZPZPOS.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZPZPOS.MVC.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var service = new GuestService(userId);
            var model = service.GetGuests();

            return View(model);
        }

        //GET: Guest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GuestCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateGuestService();

            if (service.CreateGuest(model))
            {
                TempData["SaveResult"] = "Your Guest Was Created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Guest could not be created.");

            return View(model);

        }

        // GET: Guest/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateGuestService();
            var model = svc.GetGuestById(id);

            return View(model);
        }


        private GuestService CreateGuestService()
        {
            var userId = User.Identity.GetUserId();
            var service = new GuestService(userId);
            return service;
        }
    }
}