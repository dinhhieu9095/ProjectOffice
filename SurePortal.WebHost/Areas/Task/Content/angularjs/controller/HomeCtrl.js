//Màn hình trang chủ
app.controller("HomeCtrl", function ($scope, $controller, $q, $timeout, HomeService) {
    $controller('BaseCtrl', { $scope: $scope });
    $scope.hasSubmit = false;
    $scope.filter = {
        getUrl: function (node) {
            return CommonUtils.RootURL("Contract/Home/GetAdvanceFilterTree?") + "advanceFilterSelectedId=" + $scope.filter.currentNodeId
        },
        currentNodeId: initFolderGroupId,
        id: '#tree-advance-filter',
        selectNode: function () {
            //Get project/ task
        },
        keyword: initKeyword,
    };
    $scope.GetTreeFolderGroup = function () {
        $scope.showLoading(null);
        surePortalCommon.initJstree($scope.filter);
        $scope.hideLoading();
    }
    $scope.init = function () {
    }
    $scope.ViewAdvanceFilter = function (action) {
        if (!$scope.hasSubmit) {
            $scope.optionValueId = '0';
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            $scope.AdvanceFilter = {
            };
            if (action === 'Update') {
                if ($scope.filter.FilterId === undefined || $scope.filter.FilterId === '') {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    toastr.error('Vui lòng chọn 1 tìm kiếm!', 'Thông báo');
                    return;
                }
                $scope.modalTitle = "Chỉnh sửa tìm kiếm nâng cao"
                var param = {
                    Id: $scope.filter.FilterId
                }
                var promises = [HomeService.GetAdvanceFilter(param), HomeService.GetAdvanceFilterPermission(), HomeService.GetCategory('TaskItemStatus'), HomeService.GetCategory('TaskItemPriority'), HomeService.GetCategory('NatureTask')];
                $q.all(promises).then(function (rs) {
                    $('#ViewAdvanceFilter').modal('show');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                }, function (er) {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                });
            } else if (action === 'New') {
                $scope.modalTitle = "Thêm tìm kiếm nâng cao";
                var param = {
                    Id: $scope.filter.FilterId
                }
                if ($scope.filter.FilterId !== undefined || $scope.filter.FilterId !== '') {
                    $scope.AdvanceFilter = {
                        ParentId: $scope.filter.FilterId
                    };
                }
                var promises = [HomeService.GetAdvanceFilterPermission(), HomeService.GetCategory('TaskItemStatus'), HomeService.GetCategory('TaskItemPriority'), HomeService.GetCategory('NatureTask')]
                $q.all(promises).then(function (rs) {
                    $('#ViewAdvanceFilter').modal('show');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                }, function (er) {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                });
            }
        }
    }
    $scope.SaveAdvanceFilter = function () {

    }
    $scope.DeleteAdvanceFilter = function () {

    }
    $scope.init();

});