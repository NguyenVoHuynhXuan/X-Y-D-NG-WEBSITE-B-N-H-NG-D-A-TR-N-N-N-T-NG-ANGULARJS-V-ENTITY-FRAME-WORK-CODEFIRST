﻿(function () {
    angular.module('uStora.product_categories', ['uStora.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('product_categories', {
                url: "/product_categories",
                parent: 'base',
                templateUrl: "/app/components/product_categories/productCategoryListView.html",
                controller: "productCategoryListController"
            })
            .state('add_product_category', {
                url: "/add_product_category",
                parent: 'base',
                templateUrl: "/app/components/product_categories/productCategoryAddView.html",
                controller: "productCategoryAddController"
            })
            .state('edit_product_category', {
                url: "/edit_product_category/:id",
                parent: 'base',
                templateUrl: "/app/components/product_categories/productCategoryEditView.html",
                controller: "productCategoryEditController"
            });
    }
})();