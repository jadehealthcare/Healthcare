/// <reference path="../Html/Sample.html" />
/// <reference path="../Scripts/angular.min.js" />

/// <reference path="../Scripts/angular-route.min.js" />
/// <reference path="../Scripts/angular-material/angular-material.min.js" />
/// <reference path="../Scripts/angular-animate/angular-animate.min.js" />
/// <reference path="../Scripts/angular-aria/angular-aria.min.js" />
/// <reference path="../Scripts/AngularUI/ui-router.min.js" />
/// <reference path="routeController.js" />
/// <reference path="../Scripts/jquery-1.9.1.min.js" />
/// <reference path="../Scripts/angular-google-maps.min.js" />

/// <reference path="../Scripts/ng-cordova.min.js" />
/// <reference path="../Scripts/event.min.js" />

myApp.controller('EmailController', function ($scope, $http) {
    var portName = 'http://localhost:25390';
    debugger;

    $scope.sendemail = function () {
        var responsePromise = $http.get(portName + "/api/Account/SendEmail");
        debugger;
        responsePromise.success(function (response, status, headers, config) {
            alert("Request added successfully.");
        });
        responsePromise.error(function (data, status, headers, config) {
            alert("Unsuccessful");

        });
    }
});