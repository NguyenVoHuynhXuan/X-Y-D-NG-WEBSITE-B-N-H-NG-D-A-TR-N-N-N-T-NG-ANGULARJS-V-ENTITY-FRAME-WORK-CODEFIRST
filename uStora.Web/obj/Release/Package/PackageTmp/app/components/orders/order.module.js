(function () {
    angular.module('uStora.orders', ['uStora.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('orders', {
                url: "/orders",
                parent: 'base',
                templateUrl: "/app/components/orders/orderListView.html",
                controller: "orderListController"
            }).state('add_order', {
                url: "/add_order",
                parent: 'base',
                templateUrl: "/app/components/orders/orderAddView.html",
                controller: "orderAddController"
            }).state('edit_order', {
                url: "/edit_order/:id",
                parent: 'base',
                templateUrl: "/app/components/orders/orderEditView.html",
                controller: "orderEditController"
            });
    }
})();