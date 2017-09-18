function SummonerInfo(name, region) {
    this.Name = name;
    this.Region = region;
    this.Id = null;
    this.GameId = null;
    
    GetSummonerInfo();
    GetGameInfo();

    var GetSummonerInfo = new function () {
        GetSummonerByName(this.Region, this.Name, (summonerJson) => this.Id = summonerJson.id);
    }
    var GetGameInfo = new function () {
        GetCurrentGameInfo(this.Region, this.Id, (gameJson) => this.GameId = gameJson.gameId);
    }
    this.IsInGame = new function () {
        GetGameInfo();
        return this.GameId !== null;
    }
}