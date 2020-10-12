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
            var model = service.GetGuests().OrderBy(g => g.LastName);

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

            return View(model);

        }

        // GET: Guest/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateGuestService();
            var model = svc.GetGuestById(id);

            return View(model);
        }

        // GET: Guest/Edit/{id} aka Update
        public ActionResult Edit(int id)
        {
            var service = CreateGuestService();
            var detail = service.GetGuestById(id);
            var model =
                new GuestEdit
                {
                    GuestId = detail.GuestId,
                    FirstName = detail.FirstName,
                    LastName= detail.LastName,
                    ContactNumber = detail.ContactNumber,
                    FullAddress = detail.FullAddress,
                    Notes = detail.Notes,
                };
            return View(model);
        }

        // POST: Guest/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GuestEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.GuestId != id)
            {
                ModelState.AddModelError("", "Guest Id Mismatch");
                return View(model);
            }

            var service = CreateGuestService();

            if (service.UpdateGuest(model))
            {
                TempData["SaveResult"] = "Your Guest Was Updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Guest Could Not Be Updated.");
            return View(model);
        }


        // GET: Guest/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateGuestService();
            var model = svc.GetGuestById(id);

            return View(model);
        }

        // POST: Guest/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateGuestService();

            service.DeleteGuest(id);

            TempData["SaveResult"] = "Your Guest Was Deleted";

            return RedirectToAction("Index");
        }

        private GuestService CreateGuestService()
        {
            var userId = User.Identity.GetUserId();
            var service = new GuestService(userId);
            return service;
        }
    }
}