(function (app) {
    app.controller('trackOrderAddController', trackOrderAddController);
    trackOrderAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];
    function trackOrderAddController(apiService, $scope, notificationService, $state) {
        $scope.trackorder = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.orders = [];
        $scope.vehicles = [];
        $scope.users = [];
        $scope.AddTrackOrder = AddTrackOrder;
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        function loadOrders() {
            apiService.get('/api/trackorder/getorders', null,
               function (result) {
                   $scope.orders = result.data;
               }, function () {
                   notificationService.displayError("Không có đơn hàng nào được tìm thấy.");
               })
        }
        function loadVehicles() {
            apiService.get('/api/trackorder/getvehicles', null,
               function (result) {
                   $scope.vehicles = result.data;
               }, function () {
                   notificationService.displayError("Không có xe nào được tìm thấy.");
               })
        }
        function loadUsers() {
            apiService.get('/api/trackorder/getdriver', null,
               function (result) {
                   $scope.users = result.data;
               }, function () {
                   notificationService.displayError("Không có bản ghi nào được tìm thấy.");
               })
        }
        function AddTrackOrder() {
            apiService.post('/api/trackorder/create', $scope.trackorder,
                function (result) {
                    notificationService.displaySuccess('Phân công đơn hàng của ' + result.data.Order.CustomerName+' thành công');
                    $state.go('trackorders');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Phân công không thành công');
                });
        }
        loadOrders();
        loadVehicles();
        loadUsers();
    }
})(angular.module('uStora.trackorders'));