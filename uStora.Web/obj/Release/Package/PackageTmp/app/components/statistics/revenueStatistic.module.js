(function () {
    angular.module('uStora.statistics', ['uStora.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('statistic_revenue', {
                url: "/statistic_revenue",
                parent: 'base',
                templateUrl: "/app/components/statistics/revenueStatisticView.html",
                controller: "revenueStatisticController"
            });
    }
})();