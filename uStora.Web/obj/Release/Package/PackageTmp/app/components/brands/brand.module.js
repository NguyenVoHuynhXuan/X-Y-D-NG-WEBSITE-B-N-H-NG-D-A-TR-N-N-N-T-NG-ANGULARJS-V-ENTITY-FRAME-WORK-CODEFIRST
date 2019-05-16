/// <reference path="E:\ILearning\LTPM\ASP.NET MVC\Git\OnlineShop.Web\Assets/libs/angular/angular.js" />
(function () {
    angular.module('uStora.brands', ['uStora.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('brands', {
                url: "/brands",
                parent: 'base',
                templateUrl: "/app/components/brands/brandListView.html",
                controller: "brandListController"
            }).state('add_brand', {
                url: "/add_brand",
                parent: 'base',
                templateUrl: "/app/components/brands/brandAddView.html",
                controller: "brandAddController"
            }).state('edit_brand', {
                url: "/edit_brand/:id",
                parent: 'base',
                templateUrl: "/app/components/brands/brandEditView.html",
                controller: "brandEditController"
            });
    }
})();