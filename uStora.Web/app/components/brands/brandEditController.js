(function (app) {
    app.controller('brandEditController', brandEditController);
    brandEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];
    function brandEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.brand = {
            UpdatedDate: new Date()
        }
        $scope.loadBrandDetail = loadBrandDetail;
        $scope.UpdateBrand = UpdateBrand;

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
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

        function loadBrandDetail() {
            apiService.get('/api/brand/getbyid/' + $stateParams.id, null, function (result) {
                $scope.brand = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateBrand() {
            apiService.put('/api/brand/update', $scope.brand,
                function (result) {
                    notificationService.displaySuccess('Đã cập nhật ' + result.data.Name + ' thành công');
                    $state.go('brands');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Cập nhật không thành công');
                });
        }
        loadBrandDetail();
    }
})(angular.module('uStora.brands'));