/// <reference path="E:\ILearning\LTPM\ASP.NET MVC\Git\OnlineShop.Web\Assets/libs/angular/angular.js" />
(function () {
    angular.module('uStora.vehicles', ['uStora.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('vehicles', {
                url: "/vehicles",
                parent: 'base',
                templateUrl: "/app/components/vehicles/vehicleListView.html",
                controller: "vehicleListController"
            }).state('add_vehicle', {
                url: "/add_vehicle",
                parent: 'base',
                templateUrl: "/app/components/vehicles/vehicleAddView.html",
                controller: "vehicleAddController"
            }).state('edit_vehicle', {
                url: "/edit_vehicle/:id",
                parent: 'base',
                templateUrl: "/app/components/vehicles/vehicleEditView.html",
                controller: "vehicleEditController"
            });
    }
})();