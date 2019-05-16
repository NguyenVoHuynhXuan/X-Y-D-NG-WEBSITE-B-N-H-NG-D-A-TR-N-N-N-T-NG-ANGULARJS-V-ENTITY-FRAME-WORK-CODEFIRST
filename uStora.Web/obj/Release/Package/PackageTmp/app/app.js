(function () {
    angular.module('uStora',
        ['uStora.products',
            'uStora.product_categories',
            'uStora.slides',
            'uStora.brands',
            'uStora.application_groups',
            'uStora.application_roles',
            'uStora.application_users',
            'uStora.statistics',
            'uStora.vehicles',
            'uStora.trackorders',
            'uStora.feedbacks',
            'uStora.orders',
            'uStora.common'])
        .config(config)
        .config(configAuthentication);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: "",
                templateUrl: "/app/shared/views/baseView.html",
                abstract: true
            })
            .state('login', {
                url: "/login",
                templateUrl: "/app/components/login/loginView.html",
                controller: "loginController"
            })
            .state('home', {
                url: "/home",
                parent: 'base',
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/login');
    }
    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status === "401") {
                        $location.path('/login');

                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status === "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
    
})();