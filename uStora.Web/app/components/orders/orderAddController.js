(function (app) {
    app.controller('orderAddController', orderAddController);
    orderAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];
    function orderAddController(apiService, $scope, notificationService, $state) {
        $scope.order = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.AddOrder = AddOrder;
        
        function AddOrder() {
            apiService.post('/api/order/create', $scope.order,
                function (result) {
                    notificationService.displaySuccess('Đã thêm ' + result.data.CustomerName + ' thành công');
                    $state.go('orders');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Thêm không thành công');
                });
        }
    }
})(angular.module('uStora.orders'));