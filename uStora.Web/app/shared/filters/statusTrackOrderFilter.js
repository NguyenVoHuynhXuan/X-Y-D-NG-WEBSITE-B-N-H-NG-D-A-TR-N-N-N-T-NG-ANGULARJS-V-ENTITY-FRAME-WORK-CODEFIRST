(function (app) {
    app.filter('statusTrackOrderFilter', function () {
        return function (input) {
            if (input == true)
                return "Chưa hoàn thành";
            else
                return "Hoàn thành";
        }
    })
})(angular.module('uStora.common'));