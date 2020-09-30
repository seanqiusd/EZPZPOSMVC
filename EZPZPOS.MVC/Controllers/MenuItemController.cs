﻿using EZPZPOS.Models.MenuItemModels;
using EZPZPOS.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EZPZPOS.MVC.Controllers
{
    [Authorize]
    public class MenuItemController : Controller
    {
        // GET: MenuItem
        [AllowAnonymous] // very cool annotation
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var service = new MenuItemService(userId);
            var model = service.GetMenuItems();
            
            return View(model);
        }

        //GET: MenuItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuItemCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateMenuItemService();

            if (service.CreateMenuItem(model))
            {
                TempData["SaveResult"] = "Your Menu Item Was Created.";
                return RedirectToAction("Index");
            };

            return View(model);
        }

        // GET: MenuItem/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateMenuItemService();
            var model = svc.GetMenuItemById(id);

            return View(model);
        }

        // GET: MenuItem/Edit/{id} aka Update
        public ActionResult Edit(int id)
        {
            var service = CreateMenuItemService();
            var detail = service.GetMenuItemById(id);
            var model =
                new MenuItemEdit
                {
                    Name = detail.Name,
                    Description = detail.Description,
                    Category = detail.Category,
                    Price = detail.Price,
                    ServingsInStock = detail.ServingsInStock
                };
            return View(model);
        }



        private MenuItemService CreateMenuItemService()
        {
            var userId = User.Identity.GetUserId();
            var service = new MenuItemService(userId);
            return service;
        }
    }
}