(function (app) {
    app.filter('statusOrderFilter', function () {
        return function (input) {
            if (input == 0)
                return "Chờ xác nhận";
            else
                if (input == 1)
                    return "Đang chuyển hàng";
                else
                    return "Chuyển hàng xong";
        }
    })
})(angular.module('uStora.common'));