/// <reference path="E:\ILearning\LTPM\ASP.NET MVC\Git\OnlineShop.Web\Assets/libs/angular/angular.js" />
(function () {
    angular.module('uStora.slides', ['uStora.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('slides', {
                url: "/slides",
                parent: 'base',
                templateUrl: "/app/components/slides/slideListView.html",
                controller: "slideListController"
            }).state('add_slide', {
                url: "/add_slide",
                parent: 'base',
                templateUrl: "/app/components/slides/slideAddView.html",
                controller: "slideAddController"
            }).state('edit_slide', {
                url: "/edit_slide/:id",
                parent: 'base',
                templateUrl: "/app/components/slides/slideEditView.html",
                controller: "slideEditController"
            });
    }
})();