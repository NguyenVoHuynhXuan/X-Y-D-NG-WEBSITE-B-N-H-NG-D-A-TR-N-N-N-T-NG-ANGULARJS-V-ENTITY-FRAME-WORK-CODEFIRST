(function (app) {
    'use strict';
    app.factory('authData', [function () {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            userName: "",
            image: "",
            createdDate: ""
        };

        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }]);
})(angular.module('uStora.common'));