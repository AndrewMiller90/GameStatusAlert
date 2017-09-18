//Dependencies
// - ApiRequests.js
function SummonerInfo(region, name) {
    this.Name = name;
    this.Region = region;
    this.Id = null;
    this.GameId = null;

    this.GetSummonerInfo = function () {
        GetSummonerByName(this.Region, this.Name, (summonerJson) => this.Id = summonerJson.id);
    }
    this.GetGameInfo = function () {
        GetCurrentGameInfo(this.Region, this.Id, (gameJson) => this.GameId = gameJson.gameId);
    }
    this.IsInGame = function () {
        return this.GameId !== null;
    }

    this.GetSummonerInfo();
    this.GetGameInfo();
}