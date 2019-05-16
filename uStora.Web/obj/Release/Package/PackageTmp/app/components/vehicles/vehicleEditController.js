(function (app) {
    app.controller('vehicleEditController', vehicleEditController);
    vehicleEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];
    function vehicleEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.vehicle = {
            UpdatedDate: new Date()
        }
        $scope.loadVehicleDetail = loadVehicleDetail;
        $scope.UpdateVehicle = UpdateVehicle;
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        function loadVehicleDetail() {
            apiService.get('/api/vehicle/getbyid/' + $stateParams.id, null, function (result) {
                $scope.vehicle = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateVehicle() {
            apiService.put('/api/vehicle/update', $scope.vehicle,
                function (result) {
                    notificationService.displaySuccess('Đã cập nhật ' + result.data.Name + ' thành công');
                    $state.go('vehicles');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Cập nhật không thành công');
                });
        }
        loadVehicleDetail();
    }
})(angular.module('uStora.vehicles'));