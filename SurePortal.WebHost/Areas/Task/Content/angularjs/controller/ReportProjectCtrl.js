//Màn hình quản lý biểu mẫu
app.controller("ReportProjectCtrl", function ($scope, $controller, $q) {
    $controller('BaseCtrl', { $scope: $scope });
    $controller('ReportCommonCtrl', { $scope: $scope });
    $scope.reportProject = {
        url: CommonUtils.RootURL("Task/Report/PostProjectReport"),
          
        init: function () {
            $scope.reportCommon.url = this.url;

            $scope.reportCommon.collumns = [
                { field: "NumberOf", displayName: "STT", visible: false, typeHtml: false, className: "" },
                { field: "Name", displayName: "Công việc", visible: true, typeHtml: false, className: "m-width100" },
                { field: "TaskItemPriorityName", displayName: "Mức độ công việc", visible: true, typeHtml: false, className: "" },
                { field: "TaskItemCategory", displayName: "Nhãn công việc", visible: true, typeHtml: false, className: "" },
                { field: "NatureTask", displayName: "Tính chất CV", visible: true, typeHtml: false, className: "" },
                { field: "StatusName", displayName: "Tình trạng", visible: true, typeHtml: false, className: "" },
                { field: "FromDateText", displayName: "Tgian bắt đầu", visible: true, typeHtml: false, className: "" },
                { field: "ToDateText", displayName: "Tgian kết thúc", visible: true, typeHtml: false, className: "" },
                { field: "FinishedDateText", displayName: "Tgian hoàn thành", visible: true, typeHtml: false, className: "" },
                { field: "AppraiseProcess", displayName: "Điểm tiến độ CV", visible: true, typeHtml: false, className: "" },
                { field: "Progress", displayName: "Tiến độ", visible: true, typeHtml: false, className: "" },
                { field: "ProjectPercentFinish", displayName: "Tỷ lệ hoàn thành dự án", visible: true, typeHtml: false, className: "" },
                { field: "AssignBy", displayName: "Người giao", visible: true, typeHtml: false, className: "" },
                { field: "Assign", displayName: "Người nhận", visible: true, typeHtml: false, className: "" },
                { field: "AppraisePercentFinish", displayName: "Tỉ lệ hoàn thành người giao", visible: true, typeHtml: false, className: "" },
                { field: "PercentFinish", displayName: "Tỉ lệ hoàn thành người nhận", visible: true, typeHtml: false, className: "" },
                { field: "LastResult", displayName: "Nội dung xử lý", visible: true, typeHtml: false, className: "" },
                { field: "ExtendDescription", displayName: "Nguyên nhân quá hạn", visible: true, typeHtml: false, className: "" },
                { field: "Problem", displayName: "Khó khăn vướng mắc", visible: true, typeHtml: false, className: "" },
                { field: "Solution", displayName: "Đề xuất", visible: true, cellTemplate: true, className: "" }
            ];

            $scope.reportCommon.collumnExpand = "NumberOf";
        }
    }

    $scope.reportProject.init();
});