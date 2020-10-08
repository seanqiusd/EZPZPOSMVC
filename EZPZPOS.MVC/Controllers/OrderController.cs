using EZPZPOS.Data;
using EZPZPOS.Data.Migrations;
using EZPZPOS.Models.OrderModels;
using EZPZPOS.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace EZPZPOS.MVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        //private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Order
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var service = new OrderService(userId);
            var model = service.GetOrders().OrderByDescending(o => o.OrderDateTimeUtc);

            return View(model);
        }

        //GET: Order/Create
        public ActionResult Create()
        {
            // Trying this stuff
            // ViewBag for GuestId and MenuId
            ViewBag.GuestId = CallGuestIdList();
            ViewBag.MenuItemId = CallMenuIdList();

            //My stuff that works
            //var guests = new SelectList(_db.Guests.ToList(), "GuestId", "FullName");
            //ViewBag.Guests = guests;

            //var menuItems = new SelectList(_db.MenuItems.ToList(), "MenuItemId", "Name");
            //ViewBag.MenuItems = menuItems;


            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateOrderService();
            var item = service.AccessMenuItemID();
            if (item.IsAvailable.Equals(false))
            {
                return HttpNotFound("Sorry, This Item Is Out Of Order At The Moment. Please Select Something Else.");
            }
            else if (item.ServingsInStock < model.Quantity)
            {
                return HttpNotFound("Sorry, We Don't Have Enough Of This Item.");
            }
            else
            {
                if (service.CreateOrder(model))
                {
                    TempData["SaveResult"] = "Your Order Was Created.";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }



            //MenuItem item = _db.MenuItems.Find(model.MenuItemId); This Works
            //if (item.IsAvailable.Equals(false))
            //{
            //    return HttpNotFound("Sorry, This Item Is Out Of Order At The Moment. Please Select Something Else.");
            //}
            //else if (item.ServingsInStock < model.Quantity)
            //{
            //    return HttpNotFound("Sorry, We Don't Have Enough Of This Item.");
            //}
            //else
            //{
            //    if (service.CreateOrder(model))
            //    {
            //        TempData["SaveResult"] = "Your Order Was Created.";
            //        return RedirectToAction("Index");
            //    }
            //    else
            //        return View(model);
            //}

            //if (service.CreateOrder(model))
            //{
            //    TempData["SaveResult"] = "Your Order Was Created.";
            //    return RedirectToAction("Index");
            //};

            //return View(model);
        }

        // GET: Order/Details/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateOrderService();
            var model = svc.GetOrderById(id);

            return View(model);
        }

        // GET: Order/Edit/{id} aka Update
        public ActionResult Edit(int id)
        {
            var service = CreateOrderService();
            var detail = service.GetOrderById(id);

            // Trying this
            ViewBag.MenuItemId = CallMenuIdList();

            //var menuItems = new SelectList(_db.MenuItems.ToList(), "MenuItemId", "Name"); Works, but trying something
            //ViewBag.MenuItems = menuItems;

            var model =
                new OrderEdit
                {
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    OrderId = detail.OrderId,
                    TypeOfOrder = detail.TypeOfOrder,
                    MenuItemId = detail.MenuItemId,
                    Quantity = detail.Quantity,
                    Notes = detail.Notes
                };
            return View(model);
        }

        // POST: Order/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.OrderId != id)
            {
                ModelState.AddModelError("", "Order Id Mismatch");
                return View(model);
            }

            var service = CreateOrderService();

            if (service.UpdateOrder(model))
            {
                TempData["SaveResult"] = "Your Order Was Updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Order Could Not Be Updated.");
            return View(model);
        }

        // GET: Order/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateOrderService();
            var model = svc.GetOrderById(id);

            return View(model);
        }

        // POST: Order/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateOrderService();

            service.DeleteOrder(id);

            TempData["SaveResult"] = "Your Order Was Deleted";

            return RedirectToAction("Index");
        }

        private List<SelectListItem> CallGuestIdList()
        {
            var service = new GuestService(User.Identity.GetUserId());
            List<SelectListItem> guests = new List<SelectListItem>();
            foreach (var guest in service.GetGuestByFullName())
                guests.Add(new SelectListItem { Text = guest.FirstName + " " + guest.LastName, Value = guest.GuestId.ToString() });
            return guests;
        }

        private List<SelectListItem> CallMenuIdList()
        {
            var service = new MenuItemService(User.Identity.GetUserId());
            List<SelectListItem> menuItems = new List<SelectListItem>();
            foreach (var menuItem in service.GetMenuItemByName())
                menuItems.Add(new SelectListItem { Text = menuItem.Name, Value = menuItem.MenuItemId.ToString() });
            return menuItems;
        }

        private IEnumerable<OrderDetail> GetMenuItemId()
        {
            var service = new OrderService(User.Identity.GetUserId());
            var id = service.AccessMenuItemID();
            return id;
            //List<SelectListItem> menuItemIds = new List<SelectListItem>();
            //foreach (var menuItemId in service.GetMenuItemExtraDetail())
            //    menuItemIds.Add(new SelectListItem { Text = menuItemId.MenuItemId, Value = menuItemId.MenuItemId.ToString() });
            //return menuItemIds;
        }

        private OrderService CreateOrderService()
        {
            var userId = User.Identity.GetUserId();
            var service = new OrderService(userId);
            return service;
        }

    }
}