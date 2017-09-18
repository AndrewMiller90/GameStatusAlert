//Dependencies
// - SummonerInfo.js
// - Polling.js
// - TwilioRequests.js

$(document).ready(function () {
    $("#searchSummonerByName")
        .click(() => InitialRequest('na1', $("#summonerName").val()));
    $("#pollButton")
        .click(() => TogglePolling(5000));
    $("#setupAlert")
        .hide();
});

//SummonerInfo code
var _summonerInfo = null;
function InitialRequest(region, name) {
    _summonerInfo = new SummonerInfo(region, name);
    DisplaySummonerInfo();
    UpdateTrackerSectionVisibility();
}

function DisplaySummonerInfo() {
    console.log($("#result").innerHtml);
    $("#result").html(_summonerInfo.Name + ": " + _summonerInfo.Id + " (" + _summonerInfo.GameId + ") " + new Date().toLocaleString());
}
function UpdateTrackerSectionVisibility() {
    var trackerSection = $("#setupAlert");
    if (_summonerInfo.IsInGame()) {
        trackerSection.show();
    } else {
        trackerSection.hide();
    }
}

//Polling Code
var _poll = new Poll();
function TogglePolling(pollRate) {
    var GameTrackerPromise = function (resolve, reject) {
        _summonerInfo.GetGameInfo();
        if (_summonerInfo !== null && _summonerInfo.IsInGame()) {
            resolve();
        } else {
            reject();
        }
        UpdateTrackerSectionVisibility();
        DisplaySummonerInfo();
    }
    var GameTrackerCallback = function (phoneNumber) {
        SendSms(phoneNumber, _summonerInfo.Name + '\'s Game Ended');
    }

    var phoneNumber = $("#phoneNumber").value;
    var button = $("#pollButton");
    if (_poll.IsPolling()) {
        _poll.EndPoll();
        button.val("Start tracking");
    } else {
        _poll.StartPoll(GameTrackerPromise,
            () => GameTrackerCallback(phoneNumber),
            pollRate);
        button.val("Stop tracking");
    }  
}
