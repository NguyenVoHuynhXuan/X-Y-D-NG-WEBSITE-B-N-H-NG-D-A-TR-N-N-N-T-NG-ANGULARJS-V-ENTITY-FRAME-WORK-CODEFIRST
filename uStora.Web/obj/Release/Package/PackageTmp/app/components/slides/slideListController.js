/// <reference path="../../../Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope','$sce', 'apiService', 'notificationService', '$ngBootbox', '$filter']
    function slideListController($scope,$sce, apiService, notificationService, $ngBootbox, $filter) {
        $scope.slides = [];
        $scope.loading = true;
        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteSlide = deleteSlide;

        $scope.selectAll = selectAll;

        $scope.deleteSlideMulti = deleteSlideMulti;


        $scope.htmlRaw = function (input) {
            return $sce.trustAsHtml(input);
        };

        function deleteSlideMulti() {
            $ngBootbox.confirm('Tất cả dữ liệu đã chọn sẽ bị xóa. Bạn muốn tiếp tục?').then(function () {

                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                });
                var config = {
                    params: {
                        selectedSlides: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/slide/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');
                });
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.slides, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.slides, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch('slides', function (n, o) {
            var checked = $filter('filter')(n, { checked: true });

            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);



        function deleteSlide(id) {
            $ngBootbox.confirm('Bạn chắc chắn muốn xóa bản ghi này?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/slide/delete', config, function () {
                    notificationService.displaySuccess('Đã xóa thành công.');
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công!!!');
                })
            });
        }

        function search() {
            getSlides();
        }

        $scope.getSlides = getSlides;

        function getSlides(page) {
            $scope.loading = true;
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/slide/getall', config, function (result) {
                $scope.slides = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                console.log('Không có slide nào!!!');
            });
        }
        $scope.getSlides();
    }
})(angular.module('uStora.slides'));