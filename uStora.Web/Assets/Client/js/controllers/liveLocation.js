var loc = {
    init: function () {
        loc.initMap();
    },
    updateTrackOrder: function (lng, lat) {
        $.ajax({
            url: '/Live/UpdateTrackOrder',
            type: 'POST',
            dataType: 'json',
            data: {
                lng: lng,
                lat: lat
            },
            success: function (res) {
                if (res.status) {
                    console.log(res.data);
                }
                else
                    window.location.href = "/no-order.htm";
            }
        })
    },
    initMap: function () {
        var map = new google.maps.Map(document.getElementById('gmap'), {
            center: { lat: 16.035470, lng: 108.222192 },
            zoom: 15
        });
        var infoWindow = new google.maps.InfoWindow({ map: map });

        // Try HTML5 geolocation.
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                loc.updateTrackOrder(pos.lng, pos.lat);
                //loc.getAddressFromLatLong(pos.lat, pos.lng);
                infoWindow.setPosition(pos);
                infoWindow.setContent('Bạn đang ở đây.');
                map.setCenter(pos);
            }, function () {
                loc.handleLocationError(true, infoWindow, map.getCenter());
            });
        } else {
            // Browser doesn't support Geolocation
            loc.handleLocationError(false, infoWindow, map.getCenter());
        }
    },
    getAddressFromLatLong: function (lat, lng) {
        var geocoder = new google.maps.Geocoder();
        var latLng = new google.maps.LatLng(lat, lng);
        geocoder.geocode({ 'latLng': latLng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[1]) {
                    console.log(results[0].formatted_address);
                }
            } else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });
    },
    handleLocationError: function (browserHasGeolocation, infoWindow, pos) {
        infoWindow.setPosition(pos);
        infoWindow.setContent(browserHasGeolocation ?
                              'Lỗi: Bạn hãy chuyển sang trình duyệt khác: Firefox, Opera, Edge' :
                              'Lỗi: Trình duyệt của bạn không hỗ trợ Geolocation');
    }
}

var liveLocationHub = $.connection.liveLocationHub;
$.connection.hub.start().done(function () {
    loc.init();
});
