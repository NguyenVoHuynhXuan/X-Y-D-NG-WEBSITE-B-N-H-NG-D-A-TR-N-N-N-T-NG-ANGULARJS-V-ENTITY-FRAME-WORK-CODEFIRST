﻿(function (app) {
    app.controller('rootController', rootController);
    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService'];

    function rootController($state, authData, loginService, $scope, authenticationService) {
        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        };
        $scope.authentication = authData.authenticationData;
        $scope.sidebar = "/app/shared/views/sidebar.html";
    }
})(angular.module('uStora'));