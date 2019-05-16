(function (app) {

    app.controller('revenueStatisticController', revenueStatisticController);
    revenueStatisticController.$inject = ['apiService', '$scope', 'notificationService', '$filter', 'commonService'];
    function revenueStatisticController(apiService, $scope, notificationService, $filter, commonService) {
        $scope.tableData = [];
        $scope.getStatistic = getStatistic;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.labels = [];
        $scope.series = ['Doanh thu', 'Lợi nhuận'];
        $scope.chartData = [];
        $scope.loading = true;
        $scope.fromDate = '01/01/2015';
        $scope.toDate = '01/12/' + new Date().getFullYear();

        function getStatistic(page) {
            page = page || 0;
            var config = {
                params: {
                    fromDate: commonService.strToDate($scope.fromDate),
                    toDate: commonService.strToDate($scope.toDate),
                    page: page,
                    pageSize: 8
                }
            }
            apiService.get('/api/statistic/getrevenue', config, function (response) {
                var data = response.data.Items;
                $scope.tableData = data;
                $scope.page = response.data.Page;
                $scope.pagesCount = response.data.TotalPages;
                $scope.totalCount = response.data.TotalCount;
                var labels = [];
                var chartData = [];
                var revenues = [];
                var benefit = [];
                $.each(data, function (i, item) {
                    labels.push($filter('date')(item.Date, 'dd/MM/yyyy'));
                    revenues.push(item.Revenues);
                    benefit.push(item.Benefit);

                });
                chartData.push(revenues);
                chartData.push(benefit);
                $scope.labels = labels;
                $scope.chartData = chartData;
                $scope.loading = false;
            }, function (response) {
                setTimeout(function () {
                    $scope.loading = false;
                }, 100);
            });
        }
        $('#fromDate').click(function () {
            jQuery('#fromDate').datetimepicker({
                format: 'd/m/Y',
                lang: 'vi',
                timepicker: false
            });
        });
        $('#toDate').click(function () {
            jQuery('#toDate').datetimepicker({
                format: 'd/m/Y',
                lang: 'vi',
                timepicker: false
            });
        });

        $scope.getStatistic();
    }
})(angular.module('uStora.statistics'));