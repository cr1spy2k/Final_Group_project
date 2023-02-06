using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AFakeProductIdentificationSystem.Models;


namespace AFakeProductIdentificationSystem.Controllers
{
    public class CheckQRController : Controller
    {
        public ActionResult Index(string id)
        {
            if (!HomeController.isLoaded)
            {
                HomeController.Load();
                HomeController.isLoaded = true;
            }

            using (var context = new FakeRealProductSystemEntities())
            {
                var _product = (from p in context.Products where (p.pr_id == id) select p).ToList();
                ViewBag.ListProduct = _product;
            }
            ViewBag.CheckChain = HomeController.blockChain.IsValidChain();
            return View("Index");
        }


    }
}