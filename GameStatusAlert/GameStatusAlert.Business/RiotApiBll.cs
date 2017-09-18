using GameStatusAlert.ExternalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GameStatusAlert.Business
{
    public sealed class RiotApiBll
    {
        //TODO: move to a config file
        private static string ApiKey = "RGAPI-47c292b0-41aa-4b6f-b686-dcd8df5dd557";
        public static object GetSummonerByName(string region, string name) {
            var riotApi = new RiotApi(region, ApiKey);
            var result = riotApi.GetSummonerByName(name);
            if (result != null) {
                var json = DeserializeJson(result);
                if (json.ContainsKey("id")) {
                    return new {
                        id = json["id"],
                        region = region,
                    };
                }
            }
            return null;
        }
        public static object GetCurrentGameInfo(string region, string summonerId) {
            var riotApi = new RiotApi(region, ApiKey);
            var result = riotApi.GetCurrentGameInfo(summonerId);
            object gameID = null;
            if (result != null) {
                var json = DeserializeJson(result);
                if (json.ContainsKey("gameId")) {
                    gameID = json["gameId"];
                }
            }
            return new {
                gameId = gameID
            };
        }
        private static Dictionary<string, object> DeserializeJson(string json) {
            var deserializer = new JavaScriptSerializer();
            return (Dictionary<string, object>)deserializer.DeserializeObject(json);
        }
        //private static string SerializeToJson(object obj) {
        //    var serializer = new JavaScriptSerializer();
        //    return serializer.Serialize(obj);
        //}
    }
}
