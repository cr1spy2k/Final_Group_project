using AFakeProductIdentificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AFakeProductIdentificationSystem.Models.Transaction;

namespace AFakeProductIdentificationSystem.Controllers
{
    public class HomeController : Controller
    {
        public static string minerAddress = "miner1";
        const string adminAddress = "admin";
        const string user1Address = "user1";
        const string user2Address = "user2";

        public static BlockChain blockChain = new BlockChain(proofOfWorkDifficulty: 2, miningReward: 10);
        public static bool isLoaded = false;
        public static void Load()
        {
            using (var ProductDB = new FakeRealProductSystemEntities())
            {
                var _product = ProductDB.Products.ToList();
                foreach (var item in _product)
                {

                    blockChain.MineBlock(minerAddress, item.pr_id);
                }
            }
        }

        public ActionResult Index()
        {

            if (!HomeController.isLoaded)
            {
                HomeController.Load();
                HomeController.isLoaded = true;
            }

            ViewBag.AllChainContent = blockChain.GetHomeInfor();
            ViewBag.V = blockChain.IsValidChain();

            return View("Index");
        }

        public ActionResult About()
        {

            if (!HomeController.isLoaded)
            {
                HomeController.Load();
                HomeController.isLoaded = true;
            }

            ViewBag.Message = "A Fake Product Identification System - AFPIS";



            return View("About");
        }
    }
}