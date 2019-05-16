(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];
    function productEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.product = {
            UpdatedDate: new Date()
        }
        $scope.flatFolders = [];
        $scope.brands = [];
        $scope.loadBrands = loadBrands;
        $scope.loadProductCategories = loadProductCategories;
        $scope.loadProductDetail = loadProductDetail;
        $scope.UpdateProduct = UpdateProduct;
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.moreImages = [];
        $scope.thisClose = thisClose;

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        function loadBrands() {
            apiService.get('/api/product/listbrands', null,
               function (result) {
                   $scope.brands = result.data;
               }, function () {
                   notificationService.displayError("Không có nhãn hiệu nào được tìm thấy.");
               })
        };

        function thisClose(img) {
            var listImage = $scope.moreImages;
            var index = listImage.indexOf(img);
            if (index > -1) {
                listImage.splice(index, 1);
            }
            console.log(index);
        };

        $scope.chooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }

        $scope.chooseImage = function () {
            var finder = new CKFinder();

            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        };

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        };

        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                if ($scope.product.MoreImages !== "null" && $scope.product.MoreImages !== "NULL")
                {
                    $scope.moreImages = JSON.parse($scope.product.MoreImages);
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        };
        function UpdateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess('Đã cập nhật ' + result.data.Name + ' thành công');
                    $state.go('products');
                }, function (error) {
                    console.log(error);
                    notificationService.displayError('Cập nhật không thành công');
                });
        };

        function loadProductCategories() {
            apiService.get('/api/productcategory/getallparents', null,
                function (result) {
                    $scope.parentCategories = commonService.getTree(result.data, "ID", "ParentID");
                    $scope.parentCategories.forEach(function (item) {
                        recur(item, 0, $scope.flatFolders);
                    });
                }, function () {
                    console.log('Không có dữ liệu!!!');
                });
        };
        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };
        loadProductCategories();
        loadProductDetail();
        loadBrands();
    }
})(angular.module('uStora.products'));