(function (app) {
    app.controller('orderEditController', orderEditController);
    orderEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];
    function orderEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.order = {
            UpdatedDate: new Date()
        }
        $scope.loadOrderDetail = loadOrderDetail;
        $scope.UpdateOrder = UpdateOrder;

        function loadOrderDetail() {
            apiService.get('/api/order/getbyid/' + $stateParams.id, null, function (result) {
                $scope.order = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateOrder() {
            apiService.put('/api/order/update', $scope.order,
                function (result) {
                    notificationService.displaySuccess('Đã cập nhật ' + result.data.CustomerName + ' thành công');
                    $state.go('orders');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Cập nhật không thành công');
                });
        }
        loadOrderDetail();
    }
})(angular.module('uStora.orders'));