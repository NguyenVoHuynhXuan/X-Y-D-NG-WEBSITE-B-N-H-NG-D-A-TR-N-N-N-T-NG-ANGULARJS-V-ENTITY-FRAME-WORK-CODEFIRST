(function (app) {
    app.controller('feedbackDetailController', feedbackDetailController);
    feedbackDetailController.$inject = ['$scope', '$stateParams', 'apiService'];
    function feedbackDetailController($scope, $stateParams, apiService) {
        $scope.feedback = {};
        $scope.loading = true;
        $scope.loadFeedbackDetail = loadFeedbackDetail;

        function loadFeedbackDetail() {
            $scope.loading = true;
            apiService.get('/api/feedback/getbyid/' + $stateParams.id, null, function (result) {
                $scope.feedback = result.data;
            }, function (error) {
                console.log(error.data);
            });
            $scope.loading = false;
        }
        loadFeedbackDetail();
    }
})(angular.module('uStora.feedbacks'));