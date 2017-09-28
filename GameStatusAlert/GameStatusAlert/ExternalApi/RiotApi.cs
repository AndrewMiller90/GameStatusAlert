using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Collections;
using GameStatusAlert.Queue;
using System.Threading;
using GameStatusAlert.Caching;

namespace GameStatusAlert.ExternalApi {
    using JsonResult = Dictionary<string, object>;
    public class RiotApi : IRiotApi {
        private string _region;
        private string _apiKey;
        private static Cache _messageQueueCache = new Cache(new CachePolicy() {
            SlidingExpiration = TimeSpan.FromHours(1),
            AbsoluteExpiration = TimeSpan.FromDays(1)
        });

        public RiotApi(string region, string apiKey) {
            _region = region;
            _apiKey = apiKey;
        }
        private string MakeRequest(string command) {
            try {
                var request = BuildRequest(command);
                using (var response = GetResponse(request))
                using (var responseStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                    return responseStream.ReadToEnd();
                }
            } catch (WebException e) {
                //TODO: Do something with this
            } catch (NullReferenceException e) {
                //TODO: Do something with this
            }
            return null;
        }
        private WebResponse GetResponse(WebRequest request) {
            WebResponse response = null;
            //TODO: Handle WebExceptions in Async tasks. Will not bubble on this thread.
            var queue = (MessageQueue)_messageQueueCache.GetValueOrCreateEntry(_region, () => new MessageQueue());
            queue.Enqueue(() => response = request.GetResponse()).Wait();

            return response;
        }
        private WebRequest BuildRequest(string command) {
            var requestString = string.Format("https://{0}.api.riotgames.com/{1}?api_key={2}",
                _region,
                command,
                _apiKey);
            return WebRequest.Create(requestString);
        }

        public string GetSummonerByName(string name) {
            var command = string.Format("lol/summoner/v3/summoners/by-name/{0}", name);
            return MakeRequest(command);
        }
        public string GetCurrentGameInfo(string summonerId) {
            var command = string.Format("lol/spectator/v3/active-games/by-summoner/{0}", summonerId);
            return MakeRequest(command);
        }
    }
}
