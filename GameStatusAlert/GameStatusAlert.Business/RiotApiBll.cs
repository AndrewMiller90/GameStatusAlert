using GameStatusAlert.ExternalApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GameStatusAlert.Business
{
    public sealed class RiotApiBll
    {
        private static string ApiKey = ConfigurationManager.AppSettings["ApiKey"].ToString().Trim();
        public static GameStateDTO GetGameStateByName(string region, string name) {
            var riotApi = new RiotApi(region, ApiKey);
            var result = riotApi.GetSummonerByName(name);
            if (result != null) {
                var json = DeserializeJson(result);
                if (json.ContainsKey("id")) {
                    return GetGameStateById(region, json["id"].ToString());
                }
            }
            return new GameStateDTO(null, null);
        }
        public static GameStateDTO GetGameStateById(string region, string summonerId) {
            var riotApi = new RiotApi(region, ApiKey);
            var result = riotApi.GetCurrentGameInfo(summonerId);
            var gameState = new GameStateDTO(summonerId, null);
            if (result != null) {
                var json = DeserializeJson(result);
                if (json.ContainsKey("gameId")) {
                    gameState.gameId = json["gameId"].ToString();
                }
            }
            return gameState;
        }
        private static Dictionary<string, object> DeserializeJson(string json) {
            var deserializer = new JavaScriptSerializer();
            return (Dictionary<string, object>)deserializer.DeserializeObject(json);
        }
    }
}
