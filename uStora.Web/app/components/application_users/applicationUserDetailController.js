(function (app) {
    'use strict';

    app.controller('applicationUserDetailController', applicationUserDetailController);

    applicationUserDetailController.$inject = ['$scope', 'apiService', 'notificationService', '$location', '$stateParams'];

    function applicationUserDetailController($scope, apiService, notificationService, $location, $stateParams) {
        $scope.account = {}
        $scope.BirthDay = "";
        $scope.loading = true;

        function loadDetail() {
            $scope.loading = true;
            apiService.get('/api/applicationUser/detail/' + $stateParams.id, null,
            function (result) {
                $scope.account = result.data;
                $scope.account.BirthDay = new Date($scope.account.BirthDay);
            },
            function (result) {
                notificationService.displayError(result.data);
            });
            $scope.loading = false;
        }
        loadDetail();
    }
})(angular.module('uStora.application_users'));