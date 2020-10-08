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
            ViewBag.GuestId = AccessGuestIdList();
            ViewBag.MenuItemId = AccessMenuIdList();

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
            var orderedMenuItemId = model.MenuItemId;

            var item = service.OrderMenuItemDetail(orderedMenuItemId);

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
            ViewBag.MenuItemId = CallMenuIdList(detail);

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

        // Helper method for GuestId Dropdown to display with first and last name
        private List<SelectListItem> AccessGuestIdList()
        {
            var service = new GuestService(User.Identity.GetUserId());
            List<SelectListItem> guests = new List<SelectListItem>();
            foreach (var guest in service.GetGuestByFullName())
                guests.Add(
                    new SelectListItem { 
                        Text = guest.FirstName + " " + guest.LastName, // Realize I have FullName but wanted to practice concatenation
                        Value = guest.GuestId.ToString() 
                    });
            return guests;
        }

        // Helper method for MenuItemId dropdown to display with name fo menuItem
        private List<SelectListItem> AccessMenuIdList()
        {
            var service = new MenuItemService(User.Identity.GetUserId());
            List<SelectListItem> menuItems = new List<SelectListItem>();
            foreach (var menuItem in service.GetMenuItemList())
                menuItems.Add(
                    new SelectListItem
                    {
                        Text = menuItem.Name,
                        Value = menuItem.MenuItemId.ToString()
                    });
            return menuItems;
        }

        //Helper method below is used in Edit to display the last item ordered by default upon first edit
        private SelectList CallMenuIdList(OrderDetail order)
        {
            //List<SelectListItem> menuItems = new List<SelectListItem>();
           
            //foreach (var menuItem in service.GetMenuItemByName())
            //    menuItems.Add(new SelectListItem { Text = menuItem.Name, Value = menuItem.MenuItemId.ToString() });
            //return menuItems;


            // This way is neat. Requires a little bit more coding in the EditView, but much less code, keeping the other two to contrast how else something can be done. It also returns the default ordered value upon edit.
            var service = new MenuItemService(User.Identity.GetUserId());
            return new SelectList(service.GetMenuItemList(), "MenuItemId", "Name", order.MenuItemId);
        }

        //private IEnumerable<OrderDetail> GetMenuItemId()
        //{
        //    var service = new OrderService(User.Identity.GetUserId());
        //    var id = service.AccessMenuItemID();
        //    return id;
        //    //List<SelectListItem> menuItemIds = new List<SelectListItem>();
        //    //foreach (var menuItemId in service.GetMenuItemExtraDetail())
        //    //    menuItemIds.Add(new SelectListItem { Text = menuItemId.MenuItemId, Value = menuItemId.MenuItemId.ToString() });
        //    //return menuItemIds;
        //}

        private OrderService CreateOrderService()
        {
            var userId = User.Identity.GetUserId();
            var service = new OrderService(userId);
            return service;
        }

    }
}