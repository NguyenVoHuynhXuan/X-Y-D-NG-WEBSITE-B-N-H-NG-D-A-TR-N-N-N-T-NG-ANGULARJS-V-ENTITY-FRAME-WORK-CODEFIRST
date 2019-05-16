(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true)
                return "Đã kích hoạt";
            else
                return "Chưa kích hoạt";
        }
    })
})(angular.module('uStora.common'));