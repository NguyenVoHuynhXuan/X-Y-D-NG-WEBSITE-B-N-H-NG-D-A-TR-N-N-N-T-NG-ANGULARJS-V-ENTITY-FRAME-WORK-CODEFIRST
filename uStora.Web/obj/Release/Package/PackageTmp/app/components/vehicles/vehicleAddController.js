/// <reference path="../../../Assets/libs/angular/angular.js" />
(function (app) {
    app.controller('vehicleAddController', vehicleAddController);
    vehicleAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];
    function vehicleAddController(apiService, $scope, notificationService, $state) {
        $scope.vehicle = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.AddVehicle = AddVehicle;
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        
        function AddVehicle() {
            apiService.post('/api/vehicle/create', $scope.vehicle,
                function (result) {
                    notificationService.displaySuccess('Đã thêm ' + result.data.Name + ' thành công');
                    $state.go('vehicles');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Thêm không thành công');
                });
        }
    }
})(angular.module('uStora.vehicles'));