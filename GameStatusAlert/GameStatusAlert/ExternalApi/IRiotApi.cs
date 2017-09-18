using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatusAlert.ExternalApi {
    public interface IRiotApi {
        string GetSummonerByName(string name);
        string GetCurrentGameInfo(string summonerId);
    }
}
