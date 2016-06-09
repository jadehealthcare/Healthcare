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

/// <reference path="../Scripts/event.min.js" />

myApp.controller("mapController", function ($scope, $http) {
       

    var portName = 'http://localhost:25390';
    var latitude=0;
    var longitude = 0;
    var content, infocontent;
    
    var radius = 5; // Radius of the earth in km

    if (!!navigator.geolocation)
    {

        var map;
        var myLatlng;
        var marker;
        
        var mapOptions = {
            enableHighAccuracy: true,
            zoom: 12,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

       // map = new google.maps.Map(document.getElementById('map'), mapOptions);

           navigator.geolocation.getCurrentPosition(function (position) {
             
            var geolocate = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);

            position: geolocate;
            

            latitude = position.coords.latitude;
            longitude = position.coords.longitude;

            
            var responsePromise = $http({
                method: 'GET',
                url: portName + "/api/Account/GetHospitalListData",
                responseType: 'json'

            });
            
            responsePromise.success(function (response, status, headers, config) {
                

                for (var i = 0; i < response.hospitalList.length; i++) {
                 
                    var distance = $scope.getDistanceFromLatLonInKm(latitude, longitude, response.hospitalList[i].latitude, response.hospitalList[i].longitude)
                    if (distance <= radius)
                    {
                        myLatlng = new google.maps.LatLng(response.hospitalList[i].latitude, response.hospitalList[i].longitude);
                     
                        marker = new google.maps.Marker({
                                position: myLatlng,
                                map: map,
                                title: response.hospitalList[i].HospitalName + 
                                       response.hospitalList[i].Address 
                                       
                        });
                        content = response.hospitalList[i].HospitalName+'</br>'+
                                      response.hospitalList[i].Address + ' </br>'

                        var infowindow = new google.maps.InfoWindow();
                        infocontent = 'Hospital Name: <b>' + response.hospitalList[i].HospitalName + '</b> </br>' +
                                  'Hospital Address: <b>' + response.hospitalList[i].Address + '</b> </br>'+
                                  'Contact Number: <b>' + response.hospitalList[i].Phone + '</b> </br>'+
                                  'Mobile Number: <b>' + response.hospitalList[i].Mobile + '</b> </br>';
                       map.setCenter(myLatlng);
                    }
                    else {
                        alert(distance);
                    }
                   
                    google.maps.event.addListener(marker, 'click', (function (marker, content, infowindow) {
                        return function () {
                            infowindow.setContent(content);
                            infowindow.open(map, marker);
                            //$scope.information = content;
                            document.getElementById('mapinfo').innerHTML = infocontent;
                        };
                    })(marker, content, infowindow));
                   
                }
                //alert(response.hospitalList.length);
                
              
            });
            responsePromise.error(function (data, status, headers, config) {
                alert("Submitting form failed!");
            });

        });
       

           map = new google.maps.Map(document.getElementById('map'), mapOptions);


                   $scope.getDistanceFromLatLonInKm=function(lat1, lon1, lat2, lon2) {

                           
                            var dLat = $scope.deg2rad(lat2 - lat1);  // deg2rad below
                            var dLon = $scope.deg2rad(lon2 - lon1);
                            var a = 
                            Math.sin(dLat/2) * Math.sin(dLat/2) +
                            Math.cos($scope.deg2rad(lat1)) * Math.cos($scope.deg2rad(lat2)) *
                            Math.sin(dLon / 2) * Math.sin(dLon / 2);
                            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a)); 
                            var d = radius * c; // Distance in km
                            return d;
                     }



                     $scope.deg2rad =function(deg) {
                         return deg * (Math.PI / 180);
                    }

                     //infowindow = new google.maps.InfoWindow({
                     //    map: map,
                     //    position: myLatlng,
                     //    content:
                     //        // //response.hospitalList[i].HospitalName +
                     //        //'<h2>Address: ' + response.hospitalList[i].Address + '</h2>' +
                     //        //'<h2>Contact Number: ' + Phone + '</h2>'
                     //        'hello World'
                     //});
                     //google.maps.event.addListener(marker, 'click', function () {
                     //    infowindow.open(map, marker);
                     //});

                     //google.maps.event.addDomListener(window, 'load', initialize);


    }//for if
    else
    {
         document.getElementById('map').innerHTML = 'No Geolocation Support.';
    }



        
    });
    

    

