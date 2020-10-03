using EZPZPOS.Data;
using EZPZPOS.Models.OrderModels;
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
    public class OrderController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Order
        public ActionResult Index()
        {
            //List<Order> orderList = _db.Orders.ToList();
            //List<Order> orderedOrderList = orderList.OrderBy(ord => ord.OrderDateTimeUtc).ToList();
            //var model = new OrderListItem[0].OrderBy(o => o.OrderDateTimeUtc).ToList();

            var userId = User.Identity.GetUserId();
            var service = new OrderService(userId);
            var model = service.GetOrders();

            return View(model);
        }

        //GET: Order/Create
        public ActionResult Create()
        {
            var guests = new SelectList(_db.Guests.ToList(), "GuestId", "FullName");
            ViewBag.Guests = guests;

            //var kindOfOrders = new SelectList(_db.Orders.ToList(), "TypeOfOrder", "TypeOfOrder");
            //ViewBag.Orders = kindOfOrders; This for some reason will display the very highest position enum twice and that's it

            var menuItems = new SelectList(_db.MenuItems.ToList(), "MenuItemId", "Name");
            ViewBag.MenuItems = menuItems;


            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateOrderService();

            if (service.CreateOrder(model))
            {
                TempData["SaveResult"] = "Your Order Was Created.";
                return RedirectToAction("Index");
            };
            // Likley will need to add some logic here to update the ServingsInStock
            return View(model);
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

            var menuItems = new SelectList(_db.MenuItems.ToList(), "MenuItemId", "Name");
            ViewBag.MenuItems = menuItems;

            var model =
                new OrderEdit
                {
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



        private OrderService CreateOrderService()
        {
            var userId = User.Identity.GetUserId();
            var service = new OrderService(userId);
            return service;
        }

    }
}