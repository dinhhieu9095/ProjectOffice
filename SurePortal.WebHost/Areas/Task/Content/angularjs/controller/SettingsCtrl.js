//Màn hình cấu hình hệ thống
app.controller("SettingsCtrl", function ($scope, $controller, $q, SettingsService) {
    $controller('BaseCtrl', { $scope: $scope });
    $scope.titleModal = "";
    $scope.filter = {
        keyword: '',
        pageIndex: 1,
        PageSize: 10
    };
    $scope.hasSubmit = false;
    $scope.settings = {
        list: [],

        item: {},

        getPaging: function () {
            $scope.showLoading(null);
            SettingsService.getPaging().then(function (rs) {
                $scope.settings.list = rs.data;
                $scope.hideLoading(null);
            });
        },

        saveSettings: function () {
            $scope.showLoading(null);
            SettingsService.saveSettings($scope.settings.list).then(function (rs) {
                if (rs.data == true) {
                    toastr["success"]("Thành công");
                }
                else {
                    toastr["error"]("Có lỗi xảy ra trong quá tình xử lý");
                }
                $scope.hideLoading(null);
            });
        },

        init: function () {
            this.getPaging();
        }
    }

    $scope.settings.init();
});