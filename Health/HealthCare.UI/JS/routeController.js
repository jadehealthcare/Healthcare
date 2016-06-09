/// <reference path="../Html/Sample.html" />
/// <reference path="../Scripts/angular.min.js" />

/// <reference path="../Scripts/angular-route.min.js" />
/// <reference path="../Scripts/angular-material/angular-material.min.js" />
/// <reference path="../Scripts/angular-animate/angular-animate.min.js" />
/// <reference path="../Scripts/angular-aria/angular-aria.min.js" />
/// <reference path="../Scripts/jquery-1.9.1.min.js" />
/// <reference path="../Scripts/AngularUI/ui-router.min.js" />
/// <reference path="../Scripts/angular-google-maps.min.js" />
/// <reference path="../Scripts/event.min.js" />
/// <reference path="MapScript.js" />
/// <reference path="../Scripts/ng-cordova.min.js" />


var myApp = angular.module('myApp', ['ngRoute', 'ngMaterial', 'ui.router', 'ui.event', 'ngCordova']);

myApp.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/home');

    $stateProvider
        .state('home', {
            url: '/home',
            templateUrl: '/partials/Home.html'
        }
        )

        .state('Map', {
            url: '/Location',
            templateUrl: '/partials/MapView.html',
            controller: 'mapController'
        })


    //$routeProvider

    //    // route for the home page
    //    .when('/', {
    //        templateUrl : 'partials/Home.html',
    //        controller  : 'mainController'
    //    })
    //     .when('/Html', {
    //    templateUrl: '/Html/Sample.html',
    //    controller: 'mainController'
    //     })


});


myApp.controller('mainController', function ($scope) {

    $scope.info = 'Welcome to maincontroller';
});

//myApp.controller('footerController', function ($scope) {

//    //$scope.info = 'Welcome to maincontroller';
//});

myApp.controller("myCtrl", function ($scope, $mdDialog, $mdMedia, $http, $state) {
    var portName = 'http://localhost:25390';
    var vm = this;
    $scope.myForm = {};
    $scope.isResponse = false;
    $scope.showHospitalForm = function (ev) {
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'Html/DialogHospital.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: useFullScreen
        })
        .then(function (answer) {
            $scope.status = 'You said the information was "' + answer + '".';
        }, function () {
            $scope.status = 'You cancelled the dialog.';
        });
        $scope.$watch(function () {
            return $mdMedia('xs') || $mdMedia('sm');
        }, function (wantsFullScreen) {
            $scope.customFullscreen = (wantsFullScreen === true);
        });
    };

    $scope.showUserForm = function (ev) {
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'Html/DialogUser.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: useFullScreen
        })
        .then(function (answer) {
            $scope.status = 'You said the information was "' + answer + '".';
        }, function () {
            $scope.status = 'You cancelled the dialog.';
        });
        $scope.$watch(function () {
            return $mdMedia('xs') || $mdMedia('sm');
        }, function (wantsFullScreen) {
            $scope.customFullscreen = (wantsFullScreen === true);
        });
    };

    function DialogController($scope, $mdDialog) {
        $scope.hide = function () {
            $mdDialog.hide();
        };
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };
    }

    $scope.myForm.submitHospitalForm = function (item, event) {
        debugger;
        if (!!navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                alert('inside');
                enableHighAccuracy: true;
                var geolocate = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);


                position: geolocate;
                //$scope.latitude = position.coords.latitude;
                //$scope.longitude = position.coords.longitude;
                alert('Values inside' + $scope.latitude + $scope.longitude);

                alert('Values outside' + $scope.latitude + $scope.longitude);
                var name = $scope.form.name;
                var description = $scope.form.description;
                var Address = $scope.form.address;
                var City = $scope.form.city;
                var State = $scope.form.state;
                var Country = $scope.form.country;
                var PinCode = $scope.form.pincode;
                var Phone = $scope.form.phno;
                var Mobile = $scope.form.mono;
                var Email = $scope.form.email;
                var Id = $scope.form.id;
                var password = $scope.form.password;
                var latitude = position.coords.latitude;
                var longitude = position.coords.longitude;

                
                var requoestObj = {
                    'HospitalName': name,
                    'Description': description,
                    'Address': Address,
                    'City': City,
                    'State': State,
                    'Country': Country,
                    'PinCode': PinCode,
                    'Phone': Phone,
                    'Mobile': Mobile,
                    'Email': Email,
                    'Id': Id,
                    'Password': password,
                    'latitude': latitude,
                    'longitude': longitude

                };

                var responsePromise = $http.post(portName + "/api/Account/HospitalRegister", requoestObj);

                responsePromise.success(function (response, status, headers, config) {
                    alert("Submitted Successfully.");
                });
                responsePromise.error(function (data, status, headers, config) {
                    alert("Submitting form failed!");
                });
            });

        } else {
            alert('error');
        }


    }


    $scope.myForm.submitUserForm = function (item, event) {

        console.log("--> Submitting form");

        var fname = $scope.form.fname;
        var lname = $scope.form.lname;
        var mobno = $scope.form.mobno;
        var age = $scope.form.age;
        var gender = $scope.form.gender;
        var email = $scope.form.email;
        var password = $scope.form.password;

        var requoestObj = {
            'FirstName': fname,
            'LastName': lname,
            'Mobile': mobno,
            'Age': age,
            'Gender': gender,
            'Email': email,
            'Password': password
        };




        var responsePromise = $http.post(portName + "/api/Account/UserRegister", requoestObj);
        responsePromise.success(function (response, status, headers, config) {


            alert("Submitted Successfully.");
        });
        responsePromise.error(function (data, status, headers, config) {
            alert("Submitting form failed!");
        });
    }


    $scope.myForm.loginuser = function (item, event) {
        console.log("--> Submitting form");

        //alert('Hi');
        var username = $scope.form.username;
        var password = $scope.form.password;

        var requoestObj = {
            'Email': username,
            'Password': password
        };

        var responsePromise = $http.post(portName + "/api/Account/UserLogin", requoestObj);
        responsePromise.success(function (response, status, headers, config) {

            alert("Submitted Successfully.");
            //$location.path('#/Html/Sample.html');

            //window.location.href = "/Html";
            //$location.url("/Html");
            //$locationProvider.html5Mode(true);
            $state.go('Map')
        });

        responsePromise.error(function (data, status, headers, config) {

            alert("Submitting form failed!");
        });

    }

    $scope.myForm.loginhospital = function (item, event) {
        console.log("--> Submitting form");

        //alert('Hi');
        var id = $scope.form.id;
        var password = $scope.form.password;

        var requoestObj = {
            'Id': id,
            'Password': password
        };

        var responsePromise = $http.post(portName + "/api/Account/HospitalLogin", requoestObj);
        responsePromise.success(function (response, status, headers, config) {

            alert("Submitted Successfully.");
            //$location.path('#/Html/Sample.html');

            $state.go('Map')
            //$locationProvider.html5Mode(true);

        });

        responsePromise.error(function (data, status, headers, config) {

            alert("Submitting form failed!");
        });

    }
});


//for map
//myApp.controller('mapController', function ($scope) {


//    function initMap  () {

//        if (!!navigator.geolocation) {

//            var map;

//            var mapOptions = {
//                zoom: 15,
//                mapTypeId: google.maps.MapTypeId.ROADMAP
//            };

//            map = new google.maps.Map(document.getElementById('map'), mapOptions);

//            navigator.geolocation.getCurrentPosition(function (position) {

//                var geolocate = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

//                var infowindow = new google.maps.InfoWindow({
//                    map: map,
//                    position: geolocate,
//                    content:
//                        '<h1>Location pinned from HTML5 Geolocation!</h1>' +
//                        '<h2>Latitude: ' + position.coords.latitude + '</h2>' +
//                        '<h2>Longitude: ' + position.coords.longitude + '</h2>'
//                });

//                var marker = new google.maps.Marker({
//                    position: geolocate,
//                    map: map,
//                    title: "You are here!"
//                });

//                map.setCenter(geolocate);

//            });

//        } else {
//            document.getElementById('map').innerHTML = 'No Geolocation Support.';
//        }


//    }

//});
