/// <reference path="../../../Assets/libs/angular/angular.js" />
(function (app) {
    app.controller('slideAddController', slideAddController);
    slideAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];
    function slideAddController(apiService, $scope, notificationService, $state) {
        $scope.slide = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.AddSlide = AddSlide;
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        
        $scope.chooseImage = function () {
            var finder = new CKFinder();

            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slide.Image = fileUrl;
                })
            }
            finder.popup();
        }
        function AddSlide() {
            apiService.post('/api/slide/create', $scope.slide,
                function (result) {
                    notificationService.displaySuccess('Đã thêm ' + result.data.Name + ' thành công');
                    $state.go('slides');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Thêm không thành công');
                });
        }
    }
})(angular.module('uStora.slides'));