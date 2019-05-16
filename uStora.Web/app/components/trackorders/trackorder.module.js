(function () {
    angular.module('uStora.trackorders', ['uStora.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('trackorders', {
                url: "/trackorders",
                parent: 'base',
                templateUrl: "/app/components/trackorders/trackorderListView.html",
                controller: "trackOrderListController"
            }).state('add_trackorder', {
                url: "/add_trackorder",
                parent: 'base',
                templateUrl: "/app/components/trackorders/trackorderAddView.html",
                controller: "trackOrderAddController"
            }).state('edit_trackorder', {
                url: "/edit_trackorder/:id",
                parent: 'base',
                templateUrl: "/app/components/trackorders/trackorderEView.html",
                controller: "trackOrderEditController"
            });
    }
})();