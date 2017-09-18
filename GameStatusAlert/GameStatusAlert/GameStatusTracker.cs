//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GameStatusAlert {
//    using ExternalApi;
//    using Interfaces;
//    using JsonResult = Dictionary<string, object>;

//    public enum GameStatus {
//        GameStarted,
//        GameEnded,
//        InGame,
//        NotInGame
//    };
//    internal sealed class GameStatusTracker : IGameStatusTracker {
//        private JsonResult CurrentStatus;
//        private JsonResult PreviousStatus;
//        private RiotApi_Console RiotApi;
//        private string SummonerId;

//        public GameStatusTracker(string region, string apiKey, string user) {
//            RiotApi = new RiotApi_Console(region, apiKey);

//            var userInfo = RiotApi.GetSummonerByName(user);
//            if (!userInfo.ContainsKey("id")) {
//                throw new ArgumentException(nameof(user));
//            }

//            SummonerId = userInfo["id"].ToString();

//            UpdateGameStatus();
//        }
//        public void UpdateGameStatus() {
//            PreviousStatus = CurrentStatus;
//            CurrentStatus = RiotApi.GetCurrentGameInfo(SummonerId);
//        }
//        public GameStatus GetGameStatus() {
//            UpdateGameStatus();

//            if (CurrentStatus != null && PreviousStatus == null) {
//                return GameStatus.GameStarted;
//            }
//            if (CurrentStatus != null && PreviousStatus != null) {
//                if (!GetGameId(CurrentStatus).Equals(GetGameId(PreviousStatus))) { 
//                    return GameStatus.GameStarted;
//                } else {
//                    return GameStatus.InGame;
//                }
//            }
//            if (CurrentStatus == null && PreviousStatus != null) {
//                return GameStatus.GameEnded;
//            }
//            return GameStatus.NotInGame;
//        }
//        public string GetGameId() {
//            return GetGameId(CurrentStatus);
//        }
//        private string GetGameId(JsonResult status) {
//            if (status != null && status.ContainsKey("gameId")) {
//                return status["gameId"].ToString();
//            }
//            return null;
//        }
//    }
//}
