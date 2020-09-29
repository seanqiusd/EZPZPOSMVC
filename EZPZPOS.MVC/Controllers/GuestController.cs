using EZPZPOS.Models.GuestModels;
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
            var model = new GuestListItem[0];
            return View(model);
        }

        //GET: Guest/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}