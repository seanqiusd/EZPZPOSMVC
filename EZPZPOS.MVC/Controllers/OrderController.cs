using EZPZPOS.Data;
using EZPZPOS.Models.OrderModels;
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

            var model = new OrderListItem[0];
            return View(model);
        }

        //GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreate model)
        {
            //if (!ModelState.IsValid) return View(model);

            //var service = CreateOrderService();

            //if (service.CreateOrder(model))
            //{
            //    TempData["SaveResult"] = "Your Order Was Created.";
            //    return RedirectToAction("Index");
            //};

            //return View(model);

            if (ModelState.IsValid)
            {

            }
            return View(model);
        }


    }
}