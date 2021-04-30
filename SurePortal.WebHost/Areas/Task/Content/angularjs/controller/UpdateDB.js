app.service("UpdateDBService", function ($http, $q, objectToFormData) {
    this.PostTrackingUpdateDB = function () {
        var request = $http({
            method: "GET",
            url: CommonUtils.RootURL("Task/Project/PostTrackingUpdateDB"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
});
app.controller("UpdateDBCtrl", function ($scope, $controller, $q, $timeout, fileFactory, UpdateDBService) {
    $controller('BaseCtrl', { $scope: $scope });
    $scope.PostTrackingUpdateDB = function () {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            UpdateDBService.PostTrackingUpdateDB().then(function (rs) {
                debugger
                if (rs.data !== undefined && rs.data == true) {
                    toastr.success('Thành công!', 'Thông báo')
                }
                $scope.hideLoading();
                $scope.hasSubmit = false;
            }, function (er) {
                toastr.error('Có lỗi trong quá trình xử lý!', 'Thông báo')
                $scope.hideLoading();
                $scope.hasSubmit = false;
            });
        }
    }
});