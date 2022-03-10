//menu left
app.controller("ProjectDetailCtrl", function ($scope, $controller, $q, $timeout, ProjectDetailService, TaskItemDetailService, $window) {
    $controller('BaseCtrl', { $scope: $scope });

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    $scope.projectDetail = {
        item: {

        },

        filter: {
            UserId: null,
            ProjectId: null,
            TaskItemId: null,
            TaskItemAssignId: null,
            PageIndex: 1,
            PageSize: 10,
            FromDate: null,
            ToDate: null
        },

        table: [],

        getData: function () {
            $scope.showLoading(null);
            ProjectDetailService.getData($scope.projectDetail.filter.ProjectId).then(function (rs) {
                debugger
                $scope.projectDetail.item = rs.data;
                $("#modal-project-detail").css("display", "block");
                $("#modal-task-detail").css("display", "none");
                $scope.projectDetail.getAttachment();
                $scope.hideLoading(null);
            })
        },
        downloadFile: function (id) {
            window.location.href = CommonUtils.RootURL("Task/Project/DownloadFileTrackingWorkflowDocument?fileTrackingWorkflowDocumentID=") + id;
        },
        getAttachment: function () {
            if ($scope.projectDetail.item.Attachments == null) {
                $scope.showLoading(null);
                ProjectDetailService.attachmentProject($scope.projectDetail.filter.ProjectId).then(function (rs) {
                    $scope.projectDetail.item.Attachments = rs.data;
                    $scope.hideLoading(null);
                })
            }
        },

        getReportProject: function () {
            //if ($scope.projectDetail.item.TaskItems == null) {
            $scope.showLoading(null);

            ProjectDetailService.reportProject($scope.projectDetail.filter.ProjectId).then(function (rs) {
                $scope.hideLoading(null);
                $scope.projectDetail.item.ReportProject = rs.data;
                $scope.projectDetail.renderPieChartStatus();
                $scope.projectDetail.renderPieChartProccess();



            });

            ProjectDetailService.userInProject($scope.projectDetail.filter.ProjectId).then(function (rs) {
                $scope.projectDetail.item.ProjectMembers = rs.data;
                $scope.projectDetail.renderbarChart();
            });
            //}
        },

        renderPieChartStatus: function () {
            let data = $scope.projectDetail.item.ReportProject;

            let centerTitle = document.createElement('div');
            centerTitle.innerHTML = data.TotalTask + "<br/>" + 'Công việc';
            centerTitle.style.position = 'absolute';
            centerTitle.style.textAlign = 'center';
            centerTitle.style.visibility = 'hidden';

            let dataSource = [];
            if (data.TaskNew > 0) dataSource.push({ name: 'Mới', id: '0', y: Math.round((data.TaskNew / data.TotalTask) * 100), color: "#999999" });
            if (data.TaskRead > 0) dataSource.push({ name: 'Đã xem', id: '8', y: Math.round((data.TaskRead / data.TotalTask) * 100), color: "#FF9800" });
            if (data.TaskProcess > 0) dataSource.push({ name: 'Đang thực hiện', id: '1', y: Math.round((data.TaskProcess / data.TotalTask) * 100), color: "#00CBFF" });
            if (data.TaskReport > 0) dataSource.push({ name: 'Báo cáo', id: '2', y: Math.round((data.TaskReport / data.TotalTask) * 100), color: "#0082FF" });
            if (data.TaskExtend > 0) dataSource.push({ name: 'Gia hạn', id: '5', y: Math.round((data.TaskExtend / data.TotalTask) * 100), color: "#FF000B" });
            if (data.TaskCancel > 0) dataSource.push({ name: 'Hủy', id: '3', y: Math.round((data.TaskCancel / data.TotalTask) * 100), color: "#CD5C5C" });
            if (data.TaskReportReturn > 0) dataSource.push({ name: 'Báo cáo trả lại', id: '6', y: Math.round((data.TaskReportReturn / data.TotalTask) * 100), color: "#CD5C5C" });
            if (data.TaskFinished > 0) dataSource.push({ name: 'Hoàn thành', id: '4', y: Math.round((data.TaskFinished / data.TotalTask) * 100), color: "#9900FF" });

            // Build the chart
            $("#pie-chart-status").html('');
            Highcharts.chart('pie-chart-status', {
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
                                $scope.projectDetail.table = $scope.projectDetail.item.ReportProject.TaskItems.filter(function (item) {
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
                                    $scope.projectDetail.table;
                                });

                                $("#detail-report-project").modal("show");
                            }
                        }
                    }
                }]
            });
        },

        renderPieChartProccess: function () {
            let data = $scope.projectDetail.item.ReportProject;
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
            $("#pie-chart-proccess").html('');
            Highcharts.chart('pie-chart-proccess', {
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
                                statusId = e.point.id;
                                let index = 0;
                                switch (statusId) {
                                    case 'indueDate':
                                        $scope.projectDetail.table = $scope.projectDetail.item.ReportProject.TaskItems.filter(function (item) {

                                            if (item.Process == "in-due-date") {
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
                                        $scope.projectDetail.table = $scope.projectDetail.item.ReportProject.TaskItems.filter(function (item) {

                                            if (item.Process == "out-of-date") {
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
                                    $scope.projectDetail.table;
                                });

                                $("#detail-report-project").modal("show");
                            }
                        }
                    }
                }]
            });
        },

        renderbarChart: function () {
            let series = [];
            let categories = [];
            let Read = { name: 'Đã xem', data: [], color: "#999999" };
            let New = { name: 'Mới', data: [], color: "#222" };
            let Process = { name: 'Đang xử lý', data: [], color: '#01CBFE' };
            let Report = { name: 'Báo cáo', data: [], color: '#9702FA' };
            let Finsh = { name: 'Kết thúc', data: [], color: '#0184FC' };
            let InDueDate = { name: 'Đúng hạn', data: [], color: '#0082FE' };
            let OutOfDate = { name: 'Trễ hạn', data: [], color: '#FD000B' };
            for (var i = 0; i < $scope.projectDetail.item.ProjectMembers.length; i++) {
                var user = $scope.projectDetail.item.ProjectMembers[i];

                let name = '<div>' +
                    '<div class="col-left-user-assign">' +
                    '<img title="" class="todo-userpic pull" src="{RootUrl}/{UserId}">' +
                    '</div>' +
                    '<div class="col-right-user-assign">' +
                    '<label><b>{UserName}</b> - {JobTitleName}</label>' +
                    '<br><label class="orderNumber"><b>{TotalTask}</b> {Type}</label>' +
                    '</div>' +
                    '</div>';

                if (user.Type == 0) {
                    name = name.replace("{Type}", 'Quản lý dự án');
                    name = name.replace("{TotalTask}", '');

                }
                else if (user.Type == 1) {
                    name = name.replace("{Type}", 'Giám sát dự án');
                    name = name.replace("{TotalTask}", '');
                }
                else {
                    name = name.replace("{Type}", 'Công việc được giao');
                    name = name.replace("{TotalTask}", user.TotalTask);
                }
                name = name.replace("{RootUrl}", CommonUtils.RootURL("Account/Avartar"));
                name = name.replace("{UserId}", user.UserId);
                name = name.replace("{UserName}", user.UserName);
                name = name.replace("{JobTitleName}", user.JobTitleName);

                categories.push(name);

                if (user.Type == 2) {
                    Read.data.push(user.Read);
                    New.data.push(user.New);
                    Process.data.push(user.Process);
                    Report.data.push(user.Report);
                    Finsh.data.push(user.Finsh);
                    InDueDate.data.push(user.InDueDate);
                    OutOfDate.data.push(user.OutOfDate);
                }
                else {
                    Read.data.push(0);
                    New.data.push(0);
                    Process.data.push(0);
                    Report.data.push(0);
                    Finsh.data.push(0);
                    InDueDate.data.push(0);
                    OutOfDate.data.push(0);
                }
            }

            series.push(Read);
            series.push(New);
            series.push(Process);
            series.push(Report);
            series.push(Finsh);
            series.push(InDueDate);
            series.push(OutOfDate);

            $("#bar-chart").html('');
            Highcharts.chart('bar-chart', {
                chart: {
                    type: 'bar'
                },
                title: {
                    text: 'Thành viên'
                },
                xAxis: {
                    categories: categories
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Số lượng'
                    }
                },
                legend: {
                    reversed: true
                },
                plotOptions: {
                    series: {
                        stacking: 'normal'
                    }
                },
                series: series
            });
        },

        clearForm: function () {
            $scope.projectDetail.item = {};
            $("#pie-chart-status").html('');
            $("#pie-chart-proccess").html('');
            $("#bar-chart").html('');
            $("#tabli_projectStatus").click();
            $("#tabli_projectStatus").trigger('click');
            $("#tab_projectStatus").attr("class", 'tab-pane active');
        },

        getHistory: function (userId) {
            this.filter.PageIndex = 1;
            this.filter.FromDate = '';
            this.filter.ToDate = '';
            this.filter.UserId = userId;
            if (userId != null) {
                $('.search-user-history').val(userId);
            }
            //if ($scope.taskItem.item.Attachments == null || $scope.taskItem.item.Attachments) {
            $scope.showLoading(null);
            ProjectDetailService.getHistory($scope.projectDetail.filter).then(function (rs) {
                $scope.projectDetail.ProcessHistories = rs.data;
                if (rs.data.length < $scope.projectDetail.filter.PageSize) {
                    $('.btn-more').css('display', 'none');
                }
                else {
                    $('.btn-more').css('display', 'block');
                }

                if ($('.search-user-history').html() == '') {
                    var html = '<option value= "">Chọn user</option>';
                    for (var i = 0; i < rs.data.length; i++) {
                        var user = rs.data[i];
                        html = html + '<option value= "' + user.CreatedBy + '">' + user.CreatedByFullName + '</option>'
                    }

                    $('.search-user-history').html(html);
                }
                $scope.hideLoading(null);
            })
            //}
        },

        moreHistory: function () {
            $scope.showLoading(null);
            this.filter.PageIndex = this.filter.PageIndex + 1;
            TaskItemDetailService.getHistory($scope.projectDetail.filter).then(function (rs) {
                $scope.projectDetail.item.TaskItemProcessHistories = $.merge($scope.projectDetail.item.TaskItemProcessHistories, rs.data);
                if (rs.data.length < $scope.projectDetail.filter.PageSize) {
                    $('.btn-more').css('display', 'none');
                }
                else {
                    $('.btn-more').css('display', 'block');
                }
                $scope.hideLoading(null);
            })
        },

        init: function (id) {
            this.clearForm();
            this.filter.ProjectId = id;
            this.getData();
        }
    };

    //$scope.projectDetail.init('e28f4c01-31e8-49e1-adc1-6cb61c7330d9');
});