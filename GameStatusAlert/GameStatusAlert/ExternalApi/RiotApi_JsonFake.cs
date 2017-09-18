using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GameStatusAlert.ExternalApi {
    public class RiotApi_JsonFake : IRiotApi {
        public RiotApi_JsonFake(string region, string apiKey) { }
        public string GetCurrentGameInfo(string summonerId) {
            return SerializeToJson(new { id = "12345678" });
        }
        public string GetSummonerByName(string name) {
            return SerializeToJson(new { id = "1234" });
        }
        private string SerializeToJson(object obj) {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
    }
}
