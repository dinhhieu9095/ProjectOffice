//menu left
app.controller("ReportAdminCtrl", function ($scope, $controller, ReportAdminService) {
    $controller('BaseCtrl', { $scope: $scope });

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    $scope.item = {
    };
    $scope.expandClass = 'hidden';
    $scope.ui = {
        DepartmentId: ".form-search-report .department-id-hidden",
        UserOfDepartmentId: ".form-search-report .user-of-department-id-hidden",
        UserDepartmentText: ".form-search-report .department-name-text",
        FromDate: ".form-search-report .from-date-text",
        ToDate: ".form-search-report .to-date-text",
        IsReport: '.form-search-report .is-report-checkbox',
        IsAssignTo: '.form-search-report .is-assign-to-checkbox',
        IsAssignBy: '.form-search-report .is-assign-by-checkbox',
        TrackingProgress: ".form-search-report .tracking-progress-select",
        TrackingStatus: ".form-search-report .tracking-status-select",
        TrackingCrucial: ".form-search-report .tracking-crucial-select",
        ProjectId: ".form-search-report .report-project-id"
    };
    $scope.getValueFilter = function () {
        $scope.filter = {
            FromDate: $($scope.ui.FromDate).val(),
            ToDate: $($scope.ui.ToDate).val(),
            TrackingProgress: $($scope.ui.TrackingProgress).val(), // tiến độ
            TrackingStatus: $($scope.ui.TrackingStatus).val(), //tình trang
            TrackingCrucial: $($scope.ui.TrackingCrucial).val(), //mức độ quan trọng
            Title: '',
            ProjectId: $($scope.ui.ProjectId).val()
        };
    };
    $scope.filter = {
        UserId: null,
        ProjectId: null,
        TaskItemId: null,
        TaskItemAssignId: null,
        PageIndex: 1,
        PageSize: 10,
        FromDate: null,
        ToDate: null
    };
    $scope.expandFormsearch = function () {
        if ($scope.expandClass === 'hidden') {
            $scope.expandClass = '';
        }
        else
            $scope.expandClass = 'hidden';
    },
    $scope.init = function () {
        $scope.showLoading(null);
        $scope.clearForm();
        $scope.getValueFilter();
        ReportAdminService.getReportAdmin($scope.filter).then(function (rs) {
            $scope.hideLoading(null);
            $scope.item.ReportProject = rs.data;
            $scope.renderPieChartStatus();
            $scope.renderPieChartProccess();
        });
    };
    $scope.renderPieChartStatus = function () {
        let data = $scope.item.ReportProject;

        let centerTitle = document.createElement('div');
        centerTitle.innerHTML = data.TotalTask + "<br/>" + 'Công việc';
        centerTitle.style.position = 'absolute';
        centerTitle.style.textAlign = 'center';
        centerTitle.style.visibility = 'hidden';

        let dataSource = [];
        if (data.TaskNew > 0) dataSource.push({ name: 'Mới', id: '0', y: Math.round((data.TaskNew / data.TotalTask) * 100), color: "#999999" });
        if (data.TaskDraft > 0) dataSource.push({ name: 'Nháp', id: '9', y: Math.round((data.TaskDraft / data.TotalTask) * 100), color: "#a8a8a8" });
        if (data.TaskRead > 0) dataSource.push({ name: 'Đã xem', id: '8', y: Math.round((data.TaskRead / data.TotalTask) * 100), color: "#FF9800" });
        if (data.TaskProcess > 0) dataSource.push({ name: 'Đang thực hiện', id: '1', y: Math.round((data.TaskProcess / data.TotalTask) * 100), color: "#00CBFF" });
        if (data.TaskReport > 0) dataSource.push({ name: 'Báo cáo', id: '2', y: Math.round((data.TaskReport / data.TotalTask) * 100), color: "#0082FF" });
        if (data.TaskExtend > 0) dataSource.push({ name: 'Gia hạn', id: '5', y: Math.round((data.TaskExtend / data.TotalTask) * 100), color: "#FF000B" });
        if (data.TaskCancel > 0) dataSource.push({ name: 'Hủy', id: '3', y: Math.round((data.TaskCancel / data.TotalTask) * 100), color: "#CD5C5C" });
        if (data.TaskReportReturn > 0) dataSource.push({ name: 'Báo cáo trả lại', id: '6', y: Math.round((data.TaskReportReturn / data.TotalTask) * 100), color: "#CD5C5C" });
        if (data.TaskFinished > 0) dataSource.push({ name: 'Hoàn thành', id: '4', y: Math.round((data.TaskFinished / data.TotalTask) * 100), color: "#9900FF" });

        // Build the chart
        $("#report-pie-chart-status").html('');
        Highcharts.chart('report-pie-chart-status', {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: 'Theo tình trạng'
            },
            tooltip: {
                pointFormat: '{series.x}: <b>{point.percentage:.1f}%</b>'
            },
            accessibility: {
                point: {
                    valueSuffix: '%'
                }
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false
                    },
                    showInLegend: true
                }
            },
            series: [{
                name: 'Brands',
                colorByPoint: true,
                data: dataSource,
                innerSize: '50%',
                point: {
                    events: {
                        click: function (e) {
                            statusId = parseInt(e.point.id);
                            let index = 0;
                            $scope.table = $scope.item.ReportProject.TaskItems.filter(function (item) {
                                if (item.TaskItemStatusId === statusId) {
                                    index++;
                                    item.Index = index;
                                    if (item.ToDate === null
                                        || (item.TaskItemStatusId !== 4 && moment(item.ToDate) >= new Date())
                                        || (item.TaskItemStatusId === 4 && moment(item.ToDate) >= moment(item.FinishedDate))) {
                                        item.BgColor = 'bg-color-OnSchedule';
                                    }
                                    else {
                                        item.BgColor = 'bg-color-IsOutOfDate';
                                    }
                                    item.FromDateFormat = moment(item.FromDate).format('DD/MM/YY');
                                    item.ToDateFormat = moment(item.ToDate).format('DD/MM/YY');


                                    return item;
                                }
                            });

                            $scope.$apply(function (scope) {
                                $scope.table;
                            });

                            $("#detail-report-project").modal("show");
                        }
                    }
                }
            }]
        });
    };
    $scope.renderPieChartProccess = function () {
        let data = $scope.item.ReportProject;
        let centerTitle = document.createElement('div');
        centerTitle.innerHTML = data.TotalTask + "<br/>" + 'Công việc';
        centerTitle.style.position = 'absolute';
        centerTitle.style.textAlign = 'center';
        centerTitle.style.visibility = 'hidden';

        let dataSource = [
            { name: 'Đúng hạn', id: 'indueDate', y: Math.round((data.TaskInDueDate / data.TotalTask) * 100), color: "#075FE5" },
            { name: 'Quá hạn', id: 'outOfDate', y: Math.round((data.TaskOutOfDate / data.TotalTask) * 100), color: "#f44336" }
        ];

        // Build the chart
        $("#report-pie-chart-proccess").html('');
        Highcharts.chart('report-pie-chart-proccess', {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: 'Theo tiến độ'
            },
            tooltip: {
                pointFormat: '{series.x}: <b>{point.percentage:.1f}%</b>'
            },
            accessibility: {
                point: {
                    valueSuffix: '%'
                }
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false
                    },
                    showInLegend: true
                }
            },
            series: [{
                name: 'Brands',
                colorByPoint: true,
                data: dataSource,
                innerSize: '50%',
                point: {
                    events: {
                        click: function (e) {
                            debugger
                            statusId = e.point.id;
                            let index = 0;
                            switch (statusId) {
                                case 'indueDate':
                                    $scope.table = $scope.item.ReportProject.TaskItems.filter(function (item) {
                                        debugger
                                        if (item.Process === "in-due-date") {
                                            index++;
                                            item.Index = index;
                                            item.BgColor = 'bg-color-OnSchedule';
                                            item.FromDateFormat = moment(item.FromDate).format('DD/MM/YY');
                                            item.ToDateFormat = moment(item.ToDate).format('DD/MM/YY');

                                            return item;
                                        }
                                    });
                                    break;
                                case 'outOfDate':
                                    $scope.table = $scope.item.ReportProject.TaskItems.filter(function (item) {

                                        if (item.Process === "out-of-date") {
                                            index++;
                                            item.Index = index;
                                            item.BgColor = 'bg-color-IsOutOfDate';
                                            item.FromDateFormat = moment(item.FromDate).format('DD/MM/YY');
                                            item.ToDateFormat = moment(item.ToDate).format('DD/MM/YY');

                                            return item;
                                        }
                                    });
                                    break;
                            }

                            $scope.$apply(function (scope) {
                                $scope.table;
                            });

                            $("#detail-report-project").modal("show");
                        }
                    }
                }
            }]
        });
    };
    $scope.clearForm = function () {
        $scope.item = {};
        $("#pie-chart-status").html('');
        $("#pie-chart-proccess").html('');
    };
    $scope.CloseTaskItem = function () {
        $("#detail-report-project").modal("hide");
    };
    $scope.init();
});