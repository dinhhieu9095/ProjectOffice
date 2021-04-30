app.controller("ReportOnePageCtrl", function ($scope, $controller, $q) {
    $controller('BaseCtrl', { $scope: $scope });
    $controller('ReportCommonCtrl', { $scope: $scope });
    $scope.ReportOnePageCtrl = {
        url: CommonUtils.RootURL("Task/Report/PostReportOnePage"),

        init: function () {
            $scope.reportCommon.url = this.url;

            $scope.reportCommon.collumns = [
                { field: "NumberOf", displayName: "STT", visible: false, typeHtml: false, className: "" },
                { field: "Name", displayName: "Công việc", visible: true, typeHtml: false, className: "m-width100" },
                { field: "LastResult", displayName: "Diễn giải mức độ hoàn thành", visible: true, typeHtml: false, className: "" },
                { field: "PercentFinish", displayName: "Tỉ lệ hoàn thành người nhận", visible: true, typeHtml: false, className: "" },
                { field: "AppraisePercentFinish", displayName: "Tỉ lệ hoàn thành người giao", visible: true, typeHtml: false, className: "" },
                { field: "FromDateText", displayName: "Tgian bắt đầu", visible: true, typeHtml: false, className: "" },
                { field: "ToDateText", displayName: "Tgian kết thúc", visible: true, typeHtml: false, className: "" },
                { field: "Assign", displayName: "Người nhận", visible: true, typeHtml: false, className: "" },
                { field: "Note", displayName: "Nội dung đánh giá", visible: true, typeHtml: true, className: "" },
            ];

            $scope.reportCommon.collumnExpand = "NumberOf";
        }
    }

    $scope.ReportOnePageCtrl.init();
});