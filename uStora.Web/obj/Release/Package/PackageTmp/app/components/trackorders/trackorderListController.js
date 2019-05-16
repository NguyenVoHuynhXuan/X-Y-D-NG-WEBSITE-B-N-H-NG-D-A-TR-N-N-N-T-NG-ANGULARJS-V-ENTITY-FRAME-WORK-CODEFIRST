(function (app) {
    app.controller('trackOrderListController', trackOrderListController);

    trackOrderListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter']
    function trackOrderListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.trackOrders = [];

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.loading = true;
        $scope.keyword = '';

        $scope.search = search;

        $scope.deleteTrackOrder = deleteTrackOrder;

        $scope.selectAll = selectAll;

        $scope.deleteTrackOrderMulti = deleteTrackOrderMulti;

        function deleteTrackOrderMulti() {
            $ngBootbox.confirm('Tất cả dữ liệu đã chọn sẽ bị xóa. Bạn muốn tiếp tục?').then(function () {

                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                });
                var config = {
                    params: {
                        selectedTrackOrders: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/trackorder/deletemulti', config, function (result) {
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
                angular.forEach($scope.trackOrders, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.trackOrders, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch('trackOrders', function (n, o) {
            var checked = $filter('filter')(n, { checked: true });

            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);



        function deleteTrackOrder(id) {
            $ngBootbox.confirm('Bạn chắc chắn muốn xóa bản ghi này?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/trackorder/delete', config, function () {
                    notificationService.displaySuccess('Đã xóa thành công.');
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công!!!');
                })
            });
        }

        function search() {
            getTrackOrders();
        }

        $scope.getTrackOrders = getTrackOrders;

        function getTrackOrders(page) {
            $scope.loading = true;
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/trackorder/getall', config, function (result) {
                $scope.trackOrders = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                console.log('Không có bản ghi nào!!!');
            });
        }
        $scope.getTrackOrders();
    }
})(angular.module('uStora.trackorders'));