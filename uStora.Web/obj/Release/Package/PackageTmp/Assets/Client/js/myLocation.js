var curentLocation = {
    lng: 0,
    lat: 0
}
var loc = {
    initMap: function () {
        console.log(curentLocation.lat + " - " + curentLocation.lat)

        var myLatLng = { lat: curentLocation.lat, lng: curentLocation.lng };

        var map = new google.maps.Map(document.getElementById('gmap'), {
            zoom: 10,
            center: myLatLng
        });

        var marker = new google.maps.Marker({
            position: myLatLng,
            map: map,
            title: 'Hàng đang được chuyển...'
        });

        $('#btnLoadMap').off('click').on('click', function (e) {
            e.preventDefault();
            loc.getLoction();
        })
    },
    getLoction: function () {
        $.ajax({
            url: '/TrackOrder/GetLocation',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var data = res.data;
                    $.each(data, function (i, item) {
                        loc.setLocation(item.Latitude, item.Longitude);
                    });
                    loc.initMap();
                }
            }
        });
    },
    setLocation: function (lat, lng) {
        curentLocation.lat = parseFloat(lat);
        curentLocation.lng = parseFloat(lng);
    }
}
loc.getLoction();