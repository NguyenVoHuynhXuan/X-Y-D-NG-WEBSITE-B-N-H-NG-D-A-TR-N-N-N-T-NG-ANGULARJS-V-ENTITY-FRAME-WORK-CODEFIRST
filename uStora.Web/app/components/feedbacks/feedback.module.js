/// <reference path="E:\ILearning\LTPM\ASP.NET MVC\uStora\uStora\uStora.Web\Assets/libs/angular/angular.js" />
(function () {
    angular.module('uStora.feedbacks', ['uStora.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('feedbacks', {
                url: "/feedbacks",
                parent: 'base',
                templateUrl: "/app/components/feedbacks/feedbackListView.html",
                controller: "feedbackListController"
            }).state('detail', {
                url: "/detail/:id",
                parent: 'base',
                templateUrl: "/app/components/feedbacks/feedbackDetailView.html",
                controller: "feedbackDetailController"
            });
    }
})();