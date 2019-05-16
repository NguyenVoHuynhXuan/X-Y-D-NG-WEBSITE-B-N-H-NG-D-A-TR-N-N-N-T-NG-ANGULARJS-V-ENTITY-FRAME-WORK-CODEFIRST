/// <reference path="../../../Assets/libs/angular/angular.js" />
(function (app) {
    app.controller('brandAddController', brandAddController);
    brandAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','commonService'];
    function brandAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.brand = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.AddBrand = AddBrand;
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        function GetSeoTitle() {
            $scope.brand.Alias = commonService.getSeoTitle($scope.brand.Name);
        }
        $scope.chooseImage = function () {
            var finder = new CKFinder();

            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.brand.Image = fileUrl;
                })
            }
            finder.popup();
        }
        function AddBrand() {
            apiService.post('/api/brand/create', $scope.brand,
                function (result) {
                    notificationService.displaySuccess('Đã thêm ' + result.data.Name + ' thành công');
                    $state.go('brands');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Thêm không thành công');
                });
        }
    }
})(angular.module('uStora.brands'));