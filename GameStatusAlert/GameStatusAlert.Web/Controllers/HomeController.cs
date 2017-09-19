using GameStatusAlert.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStatusAlert.Web.Controllers
{
    public class HomeController : Controller
    {
        //(812) 727-3802
        private static Dictionary<string, string> regions = 
            new Dictionary<string, string>() {
                ["NA"] = "NA1",
                ["BR"] = "BR1",
                ["EUNE"] = "EUN1",
                ["EUW"] = "EUW1",
                ["JP"] = "JP1",
                ["KR"] = "KR",
                ["LAN"] = "LA1",
                ["LAS"] = "LA2",
                ["OCE"] = "OC1",
                ["TR"] = "TR1",
                ["RU"] = "RU",
                ["PBE"] = "PBE1",
            };
        // GET: Home
        public ActionResult Index()
        {
            return View(regions);
        }
    }
}