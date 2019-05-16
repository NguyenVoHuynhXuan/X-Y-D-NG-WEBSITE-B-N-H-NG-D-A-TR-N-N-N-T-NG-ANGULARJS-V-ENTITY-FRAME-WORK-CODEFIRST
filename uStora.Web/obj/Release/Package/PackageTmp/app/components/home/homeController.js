/// <reference path="../../../Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('homeController', homeController);
    homeController.$inject = ['$scope', 'apiService', 'notificationService', '$sce'];
    function homeController($scope, apiService, notificationService, $sce) {
        $scope.getItemCount = getItemCount;
        $scope.productsCount = 0;
        $scope.feedbacksCount = 0;
        $scope.ordersCount = 0;
        $scope.usersCount = 0;
        $scope.users = [];
        $scope.products = [];
        $scope.latestUsers = latestUsers;
        $scope.latestProducts = latestProducts;
       
        function latestUsers() {
            apiService.get('/api/home/getlatestusers', null, function (res) {
                $scope.users = res.data;
            });
        }
        function latestProducts() {
            apiService.get('/api/home/getlatestproducts', null, function (res) {
                $scope.products = res.data;
            });
        }
        
        $scope.htmlRaw = function (input) {
            return $sce.trustAsHtml(input);
        };
        function getItemCount() {
            apiService.get('/api/home/gettotalitem', null, function (res) {
                $scope.productsCount = res.data.TotalProducts;
                $scope.feedbacksCount = res.data.TotalFeedbacks;
                $scope.ordersCount = res.data.TotalOrders;
                $scope.usersCount = res.data.TotalUsers;
            }, function (res) {

            });
        }
        latestUsers();
        getItemCount();
        latestProducts();
    }
})(angular.module('uStora'));