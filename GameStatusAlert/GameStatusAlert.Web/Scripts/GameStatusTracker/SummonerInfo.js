//Dependencies
// - ApiRequests.js
function SummonerInfo(region, name, callback) {
    this.Name = name;
    this.Region = region;
    this.Id = null;
    this.GameId = null;
    this.Callback = callback;

    this.GetSummonerInfo = function () {
        if (this.Id === null) {
            GetGameStateByName(this.Region, this.Name, (json) => this.GetSummonerInfoComplete(json));
        } else {
            GetGameStateById(this.Region, this.Id, (json) => this.GetSummonerInfoComplete(json));
        }
    }
    this.GetSummonerInfoComplete = function (json) {
        this.Id = json.id;
        this.GameId = json.gameId;

        this.Callback();
    }
    this.IsInGame = function () {
        return this.GameId !== null;
    }
    this.GetDescription = function () {
        return this.Name + ": " + this.Id + " (" + this.GameId + ") " + new Date().toLocaleString()
    }
}