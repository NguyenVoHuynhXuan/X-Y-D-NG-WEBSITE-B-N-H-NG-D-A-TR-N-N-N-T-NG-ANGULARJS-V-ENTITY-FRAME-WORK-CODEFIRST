(function (app) {
    app.controller('slideEditController', slideEditController);
    slideEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];
    function slideEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.slide = {
            UpdatedDate: new Date()
        }
        $scope.loadSlideDetail = loadSlideDetail;
        $scope.UpdateSlide = UpdateSlide;

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

        function loadSlideDetail() {
            apiService.get('/api/slide/getbyid/' + $stateParams.id, null, function (result) {
                $scope.slide = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateSlide() {
            apiService.put('/api/slide/update', $scope.slide,
                function (result) {
                    notificationService.displaySuccess('Đã cập nhật ' + result.data.Name + ' thành công');
                    $state.go('slides');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Cập nhật không thành công');
                });
        }
        loadSlideDetail();
    }
})(angular.module('uStora.slides'));