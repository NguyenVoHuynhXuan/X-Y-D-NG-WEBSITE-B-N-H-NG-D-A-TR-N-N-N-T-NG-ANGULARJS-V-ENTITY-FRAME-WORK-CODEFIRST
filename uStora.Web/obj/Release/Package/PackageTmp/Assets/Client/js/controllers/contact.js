var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        contact.initMap();
    },
    initMap: function () {
        var myLocation = { lat: parseFloat($('#hidLat').val()), lng: parseFloat($('#hidLng').val()) };
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 19,
            center: myLocation
        });

        var contentString = $('#hidAddress').val();

        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });

        var marker = new google.maps.Marker({
            position: myLocation,
            map: map,
            title: $('#hidName').val()
        });
        infowindow.open(map, marker);
    }
}
contact.init();