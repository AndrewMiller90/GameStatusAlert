//Dependencies
// - SummonerInfo.js
// - Polling.js
// - TwilioRequests.js

//controller
var _summonerInfo = null;
var _summonerDescriptionApp = angular.module('IndexApp', []);
_summonerDescriptionApp.controller('summonerDescriptionCtrl', function ($scope) {
    $scope.GetSummonerInfo = function () {
        _summonerInfo = new SummonerInfo($scope.summonerRegion, $scope.summonerName);
        $scope.UpdateDisplays();
    };
    $scope.SearchButtonDisabled = function () {
        return (typeof ($scope.summonerName) == "undefined" ||
            typeof ($scope.summonerRegion) == "undefined" ||
            $scope.summonerName === "");
    }
    $scope.UpdateDisplays = function () {
        $scope.Description = _summonerInfo.GetDescription();
        $scope.ShowTracker = _summonerInfo.IsInGame();
        $scope.ButtonText = _poll.IsPolling() ? "Stop tracking" : "Start tracking";
    }
    $scope.TogglePolling = function () {
        TogglePolling(5000, $scope.PhoneNumber, function () {
            if (!$scope.$$phase) {
                $scope.$apply($scope.UpdateDisplays);
            } else {
                $scope.UpdateDisplays();
            }
        });
    };
});

//Polling Code
//TODO: Come up with something better than passing updateController and calling it on every pass
var _poll = new Poll();
function TogglePolling(pollRate, phoneNumber, updateController) {
    var GameTrackerPromise = function (resolve, reject) {
        _summonerInfo.GetGameInfo();
        if (_summonerInfo.IsInGame()) {
            resolve();
        } else {
            reject();
        }
        updateController();
    }
    var GameTrackerCallback = function (phoneNumber) {
        SendSms(phoneNumber, _summonerInfo.Name + '\'s Game Ended');
        updateController();
    }

    if (_poll.IsPolling()) {
        _poll.EndPoll();
    } else {
        _poll.StartPoll(GameTrackerPromise,
            () => GameTrackerCallback(phoneNumber),
            pollRate);
    };
}
