(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];
    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.loading = true;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deleteProduct = deleteProduct;
        $scope.selectAll = selectAll;
        $scope.deleteProductMulti = deleteProductMulti;
        $scope.exportProductToXsls = exportProductToXsls;
        $scope.importProductToXsls = importProductToXsls;

        function deleteProductMulti() {
            $ngBootbox.confirm('Tất cả dữ liệu đã chọn sẽ bị xóa. Bạn muốn tiếp tục?').then(function () {

                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                });
                var config = {
                    params: {
                        selectedProducts: JSON.stringify(listId)
                    }
                };
                apiService.del('/api/product/deletemulti', config, function (result) {
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
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch('products', function (n, o) {
            var checked = $filter('filter')(n, { checked: true });

            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn chắc chắn muốn xóa bản ghi này?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.del('/api/product/delete', config, function () {
                    notificationService.displaySuccess('Đã xóa thành công.');
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công!!!');
                });
            });
        }

        function search() {
            getProducts();
        }

        $scope.getProducts = getProducts;

        function getProducts(page) {
            page = page || 0;
            $scope.loading = true;
            var config = {
                params: {
                    filter: $scope.keyword,
                    page: page,
                    pageSize: 5
                }
            };
            apiService.get('/api/product/getall', config, function (result) {
                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
               
            }, function () {
                console.log('Không có sản phẩm nào!!!');
            });
             $scope.loading = false;
        }

        function exportProductToXsls() {
            $scope.loading = true;
            var config = {
                params: {
                    filter: $scope.keyword
                }
            }
            apiService.get('/api/product/exporttoexcel', config, function (response) {
                if (response.status == 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
            $scope.loading = false;
        }
        function importProductToXsls() {
            $scope.loading = true;

            var data = new FormData();

            var files = $("#importedProduct").get(0).files;


            if (files.length > 0) {
                data.append("importedProduct", files[0]);
            }
            else {
                $scope.loading = false;
                notificationService.displayInfo('Bạn chưa chọn file');
                return false;
            }
                

            $.ajax({
                url: '/api/product/importtoexcel',
                type: "POST",
                processData: false,
                data: data,
                dataType: 'text',
                contentType: false,
                success: function (response) {
                    notificationService.displaySuccess('Import file thành công');
                    $("#btnClose").trigger('click');
                },
                error: function (er) {
                    $("#btnClose").trigger('click');
                    notificationService.displayError('Import file không thành công');
                }
            });
            $scope.loading = false;

        }
        $scope.getProducts();
    }
})(angular.module('uStora.products'));