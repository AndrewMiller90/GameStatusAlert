function GetSummonerByName(region, name, onSuccess) {
    $.ajax({
        type: "post",
        url: '/Api/GetSummonerByName',
        data: JSON.stringify({ 'region': region, 'name': name }),
        contentType: "application/json",
        dataType: "json",
        async: false,
        success: onSuccess,
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO: Write Erorr Handling
            alert(textStatus + ": " + errorThrown);
        }
    });
}
function GetCurrentGameInfo(region, summonerId, onSuccess) {
    $.ajax({
        type: "post",
        url: '/Api/GetCurrentGameInfo',
        data: JSON.stringify({ 'region': region, 'summonerId': summonerId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: onSuccess,
        error: function (jqXHR, textStatus, errorThrown) {
            //TODO: Write Error Handling
            alert(textStatus + ": " + errorThrown);
        }
    });
}
//function InitialRequest(region, name) {
//    var SummonerInfo = null;
//    function assignSummonerInfo(summonerJson) {
//        SummonerInfo.name = name;
//        SummonerInfo.id = summonerJson.id;
//        function assignGameInfo(gameJson) {
//            SummonerInfo.gameId = gameJson;
//        }
//        GetCurrentGameInfo(region, SummonerInfo.id, assignGameInfo);
//    }
//    GetCurrentGameInfo(region, summonerId, assignSummonerInfo);
//    return SummonerInfo;
//}