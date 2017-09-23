using GameStatusAlert.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GameStatusAlert.Web.Controllers {
    public class ApiController : Controller {
        
        [HttpPost]
        public ActionResult GetGameStateById(string region, string summonerId) {
            var gameState = RiotApiBll.GetGameStateById(region, summonerId);
            return Json(gameState, "application/json");
        }
        [HttpPost]
        public JsonResult GetGameStateByName(string region, string name) {
            var gameState = RiotApiBll.GetGameStateByName(region, name);
            return Json(gameState, "application/json");
        }
    }
}