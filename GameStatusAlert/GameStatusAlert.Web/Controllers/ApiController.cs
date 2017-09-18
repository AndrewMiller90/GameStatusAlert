using GameStatusAlert.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GameStatusAlert.Web.Controllers {
    public class ApiController : Controller {
        
        [HttpPost]
        public ActionResult GetCurrentGameInfo(string region, string summonerId) {
            var game = RiotApiBll.GetCurrentGameInfo(region, summonerId);
            if (game != null) {
                return Json(game, "application/json");
            }
            return null; //TODO: Make this into an http error
        }
        [HttpPost]
        public JsonResult GetSummonerByName(string region, string name) {
            var summoner = RiotApiBll.GetSummonerByName(region, name);
            if (summoner != null) {
                return Json(summoner, "application/json");
            }
            return null; //TODO: Make this into an http error
        }
    }
}