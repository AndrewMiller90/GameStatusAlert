using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatusAlert.Business {
    public sealed class GameStateDTO {
        //SummonerId
        public string id { get; set; }
        //GameId
        public string gameId { get; set; }
        public GameStateDTO(string Id, string GameId) {
            id = Id;
            gameId = GameId;
        }
    }
}
