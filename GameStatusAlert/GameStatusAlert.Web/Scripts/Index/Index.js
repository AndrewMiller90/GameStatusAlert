//Dependencies
// - SummonerInfo.js
// - Polling.js
// - TwilioRequests.js

//controller
var _summonerInfo = null;
var _summonerDescriptionApp = angular.module('IndexApp', []);
_summonerDescriptionApp.controller('summonerDescriptionCtrl', function ($scope) {
    $scope.LoaderVisible = false;
    $scope.GetSummonerInfo = function () {
        $scope.LoaderVisible = true;
        _summonerInfo = new SummonerInfo($scope.summonerRegion, $scope.summonerName, $scope.UpdateDisplays);
        _summonerInfo.GetSummonerInfo();
    };
    $scope.SearchButtonDisabled = function () {
        return (!$scope.summonerName || !$scope.summonerRegion || $scope.summonerName === "");
    }
    $scope.UpdateDisplays = function () {
        if ($scope.$$phase) {
            $scope.Description = _summonerInfo.GetDescription();
            $scope.ShowTracker = _summonerInfo.IsInGame();
            $scope.ButtonText = _poll.IsPolling() ? "Stop tracking" : "Start tracking";
            $scope.LoaderVisible = false;
        } else {
            $scope.$apply($scope.UpdateDisplays);
        }
    }
    $scope.TogglePolling = function () {
        TogglePolling(15000, $scope.PhoneNumber);
        $scope.UpdateDisplays();
    };
});

//Polling Code
//TODO: Come up with something better than passing updateController and calling it on every pass
var _poll = new Poll();
function TogglePolling(pollRate, phoneNumber) {
    var GameTrackerPromise = function (resolve, reject) {
        _summonerInfo.GetSummonerInfo();
        if (_summonerInfo.IsInGame()) {
            resolve();
        } else {
            reject();
        }
    }
    var GameTrackerCallback = function (phoneNumber) {
        SendSms(phoneNumber, _summonerInfo.Name + '\'s Game Ended');
    }

    if (_poll.IsPolling()) {
        _poll.EndPoll();
    } else {
        _poll.StartPoll(GameTrackerPromise,
            () => GameTrackerCallback(phoneNumber),
            pollRate);
    };
}
