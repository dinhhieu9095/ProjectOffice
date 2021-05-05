//Màn hình quản lý biểu mẫu
app.controller("ReportCommonCtrl", function ($scope, $controller, $q, ReportService) {
    $controller('BaseCtrl', { $scope: $scope });
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    $scope.reportCommon = {

        ui: {
            DepartmentId: ".form-search-report .department-id-hidden",
            UserOfDepartmentId: (".form-search-report .user-of-department-id-hidden"),
            UserDepartmentText: (".form-search-report .department-name-text"),
            FromDate: (".form-search-report .from-date-text"),
            ToDate: (".form-search-report .to-date-text"),
            IsReport: ('.form-search-report .is-report-checkbox'),
            IsAssignTo: ('.form-search-report .is-assign-to-checkbox'),
            IsAssignBy: ('.form-search-report .is-assign-by-checkbox'),
            TrackingProgress: (".form-search-report .tracking-progress-select"),
            TrackingStatus: (".form-search-report .tracking-status-select"),
            TrackingCrucial: (".form-search-report .tracking-crucial-select"),
            ProjectId: (".form-search-report .report-project-id")
        },

        filterOrgChart: {
            getUrl: function (node) {
                if ($('.input-is-user') == 'user')
                    return CommonUtils.RootURL("Org/GetOrgUserChart")
                else
                    return CommonUtils.RootURL("Org/GetOrgUserChart")
            },
            modalId: '#org-chart-modal',
            treeId: '#org-user-chart-modal',
            submitId: '#org-chart-submit-id',
            selectNode: function () {
            },
            submitOrgChart: function (data) {
                $scope.reportCommon.getOrgUser(data);
            },
        },

        expandClass: 'hidden',

        data: [],

        url: '',

        collumnExpand: "",

        collumns: [],

        filter: {
            DepartmentId: '',
            UserOfDepartmentId: '',
            UserDepartmentText: '',
            FromDate: '',
            ToDate: '',
            IsReport: false,
            IsAssignTo: false,
            IsAssignBy: false,
            IsWhoExcute: false,
            IsWhoAssignBy: false,
            IsFullControl: false,
            TrackingProgress: null,
            TrackingStatus: null,
            TrackingCrucial: null,
            Action: '',
            Title: '',
            TotalWeek: 0,
            CurrentWeek: 0,
            Level: null,
            ProjectId: '',
            ReportType: 'week',
            Projects: []
        },

        getData: function (type) {
            $scope.showLoading();
            this.getValueFilter();
            $scope.reportCommon.filter.Action = type;
            ReportService.getData($scope.reportCommon.filter, $scope.reportCommon.url).then(function (rs) {
                if (type == 'exportexcel') {
                    $scope.hideLoading();
                    window.location = rs.data;
                }
                else {
                    // báo cáo one page
                    if ($("#userReportTableOnePage").length == 1) {
                        $('#userReportTableOnePage').attr("src", "http://owa.bioportal.vn/op/view.aspx?src=" + CommonUtils.RootURL("Task/Report/DownloadFile?fileName=") + rs.data.fileName)
                    }
                    else {
                        $scope.reportCommon.data = rs.data.Result;
                        var htmlSumaryTotalTask = "<tr><td style='width: 120px;'>Số lượng</td>";
                        var htmlSumaryIndueTask = "<tr><td>Đúng hạn</td>";
                        var htmlSumaryOutdueTask = "<tr><td>Quá hạn</td>";
                        var htmlHeadSumaryTask = "<th></th>";
                        for (var i = 0; i < rs.data.SumaryTasks.length; i++) {
                            htmlHeadSumaryTask += '<th style="width: 120px; text-align: center">' + rs.data.SumaryTasks[i].Name + '</th>';
                            htmlSumaryTotalTask += '<td style="width: 120px; text-align: center">' + rs.data.SumaryTasks[i].Count + '</td>';
                            htmlSumaryIndueTask += '<td style="width: 120px; text-align: center">' + (rs.data.SumaryTasks[i].PercentInDue == null ? "" : rs.data.SumaryTasks[i].PercentInDue) + '</td>';
                            htmlSumaryOutdueTask += '<td style="width: 120px; text-align: center">' + (rs.data.SumaryTasks[i].PercentOutDue == null ? "" : rs.data.SumaryTasks[i].PercentOutDue) + '</td>';
                        }
                        htmlSumaryTotalTask += "</tr>";
                        htmlSumaryIndueTask += "</tr>";
                        htmlSumaryOutdueTask += "</tr>";
                        $("#tbHeadSumaryTask").html(htmlHeadSumaryTask);
                        $("#tbSumaryTask").html(htmlSumaryTotalTask + htmlSumaryIndueTask + htmlSumaryOutdueTask);
                    }
                    $scope.hideLoading(null);
                }
                //$scope.reportCommon.data = rs.data;
                //$scope.hideLoading(null);
            }, function (error) {
                $scope.reportCommon.data = [];
                $("#tbHeadSumaryTask").html('');
                $("#tbSumaryTask").html('');
                $scope.hideLoading(null);
            })
        },

        getTree: function (data, primaryIdName, parentIdName) {
            if (!data || data.length == 0 || !primaryIdName || !parentIdName)
                return [];

            var tree = [],
                rootIds = [],
                item = data[0],
                primaryKey = item[primaryIdName],
                treeObjs = {},
                parentId,
                parent,
                len = data.length,
                i = 0;

            while (i < len) {
                item = data[i++];
                primaryKey = item[primaryIdName];
                treeObjs[primaryKey] = item;
                parentId = item[parentIdName];

                if (parentId) {
                    parent = treeObjs[parentId];

                    if (parent.children) {
                        parent.children.push(item);
                    }
                    else {
                        parent.children = [item];
                    }
                }
                else {
                    rootIds.push(primaryKey);
                }
            }

            for (var i = 0; i < rootIds.length; i++) {
                tree.push(treeObjs[rootIds[i]]);
            };

            return tree;
        },

        getValueFilter: function () {
            var reportType = this.filter.ReportType;
            this.filter = {
                DepartmentId: $($scope.reportCommon.ui.DepartmentId).val(),
                UserOfDepartmentId: $($scope.reportCommon.ui.UserOfDepartmentId).val(),
                UserDepartmentText: $($scope.reportCommon.ui.UserDepartmentText).val(),
                FromDate: $($scope.reportCommon.ui.FromDate).val(),
                ToDate: $($scope.reportCommon.ui.ToDate).val(),
                IsReport: $($scope.reportCommon.ui.IsReport).is(':checked'),
                IsAssignTo: $($scope.reportCommon.ui.IsAssignTo).is(':checked'),
                IsAssignBy: $($scope.reportCommon.ui.IsAssignBy).is(':checked'),
                IsWhoExcute: false,
                IsWhoAssignBy: false,
                IsFullControl: false,
                TrackingProgress: $($scope.reportCommon.ui.TrackingProgress).val(),
                TrackingStatus: $($scope.reportCommon.ui.TrackingStatus).val(),
                TrackingCrucial: $($scope.reportCommon.ui.TrackingCrucial).val(),
                Action: 'Report',
                Title: '',
                TotalWeek: 0,
                CurrentWeek: 0,
                Level: null,
                ProjectId: $($scope.reportCommon.ui.ProjectId).val(),
                ReportType: reportType
            }
        },

        expandFormsearch: function () {
            if ($scope.reportCommon.expandClass == 'hidden') {
                $scope.reportCommon.expandClass = '';
            }
            else
                $scope.reportCommon.expandClass = 'hidden';
        },

        initOrgUser: function () {
            $scope.showLoading(null);
            surePortalCommon.initJstreeCheckBox($scope.reportCommon.filterOrgChart);
            $scope.hideLoading();
        },

        getOrgUser: function (data) {
            if (data.filter(x => x.type == 'department').length > 0) {
                var departments = data.filter(x => x.type == 'department');
                $(".form-search-report .department-name-text").val(departments[0].text);
                $(".form-search-report .department-id-hidden").val(departments[0].id);
                $(".form-search-report .user-of-department-id-hidden").val('');
            }
            else {
                var users = data.filter(x => x.type == 'user');
                if (users.length > 0) {
                    $(".form-search-report .department-name-text").val(users[0].text + ' - ' + users[0].jobTitle);
                    $(".form-search-report .user-of-department-id-hidden").val(users[0].id);
                    $(".form-search-report .department-id-hidden").val('');
                }
            }
        },

        changeTime: function (type) {
            this.filter.ReportType = type;
            if ($scope.reportCommon.filter.ReportType == 'week') {
                $($scope.reportCommon.ui.FromDate).val(moment().startOf('week').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(moment().endOf('week').format('DD/MM/YYYY'));
            }
            else if ($scope.reportCommon.filter.ReportType == 'month') {
                $($scope.reportCommon.ui.FromDate).val(moment().startOf('month').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(moment().endOf('month').format('DD/MM/YYYY'));
            }
            else if ($scope.reportCommon.filter.ReportType == 'quarter') {
                $($scope.reportCommon.ui.FromDate).val(moment().startOf('quarter').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(moment().endOf('quarter').format('DD/MM/YYYY'));
            }
            else if ($scope.reportCommon.filter.ReportType == 'year') {
                $($scope.reportCommon.ui.FromDate).val(moment().startOf('year').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(moment().endOf('year').format('DD/MM/YYYY'));
            }
            $($scope.reportCommon.ui.FromDate).trigger('change');
            $($scope.reportCommon.ui.ToDate).trigger('change');
        },

        previousTime: function () {
            if ($scope.reportCommon.filter.ReportType == 'week') {
                var previous = moment($($scope.reportCommon.ui.FromDate).val(), 'DD/MM/YYYY').add(-1, 'week');
                $($scope.reportCommon.ui.FromDate).val(previous.startOf('week').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(previous.endOf('week').format('DD/MM/YYYY'));
            }
            else if ($scope.reportCommon.filter.ReportType == 'month') {
                var previous = moment($($scope.reportCommon.ui.FromDate).val(), 'DD/MM/YYYY').add(-1, 'month');
                $($scope.reportCommon.ui.FromDate).val(previous.startOf('month').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(previous.endOf('month').format('DD/MM/YYYY'));
            }
            else if ($scope.reportCommon.filter.ReportType == 'quarter') {
                var previous = moment($($scope.reportCommon.ui.FromDate).val(), 'DD/MM/YYYY').add(-1, 'quarter');
                $($scope.reportCommon.ui.FromDate).val(previous.startOf('quarter').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(previous.endOf('quarter').format('DD/MM/YYYY'));
            }
            else if ($scope.reportCommon.filter.ReportType == 'year') {
                var previous = moment($($scope.reportCommon.ui.FromDate).val(), 'DD/MM/YYYY').add(-1, 'year');
                $($scope.reportCommon.ui.FromDate).val(previous.startOf('year').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(previous.endOf('year').format('DD/MM/YYYY'));
            }
            $($scope.reportCommon.ui.FromDate).trigger('change');
            $($scope.reportCommon.ui.ToDate).trigger('change');
        },

        nextTime: function () {
            if ($scope.reportCommon.filter.ReportType == 'week') {
                var next = moment($($scope.reportCommon.ui.FromDate).val(), 'DD/MM/YYYY').add(1, 'week');
                $($scope.reportCommon.ui.FromDate).val(next.startOf('week').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(next.endOf('week').format('DD/MM/YYYY'));
            }
            else if ($scope.reportCommon.filter.ReportType == 'month') {
                var next = moment($($scope.reportCommon.ui.FromDate).val(), 'DD/MM/YYYY').add(1, 'month');
                $($scope.reportCommon.ui.FromDate).val(next.startOf('month').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(next.endOf('month').format('DD/MM/YYYY'));
            }
            else if ($scope.reportCommon.filter.ReportType == 'quarter') {
                var next = moment($($scope.reportCommon.ui.FromDate).val(), 'DD/MM/YYYY').add(1, 'quarter');
                $($scope.reportCommon.ui.FromDate).val(next.startOf('quarter').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(next.endOf('quarter').format('DD/MM/YYYY'));
            }
            else if ($scope.reportCommon.ReportType == 'year') {
                var next = moment($($scope.reportCommon.ui.FromDate).val(), 'DD/MM/YYYY').add(1, 'year');
                $($scope.reportCommon.ui.FromDate).val(next.startOf('year').format('DD/MM/YYYY'));
                $($scope.reportCommon.ui.ToDate).val(next.endOf('year').format('DD/MM/YYYY'));
            }
            $($scope.reportCommon.ui.FromDate).trigger('change');
            $($scope.reportCommon.ui.ToDate).trigger('change');
        },

        changeWeek: function () {
            var year = $(".select-year").val();
            var week = $(".select-week").val();
            var firstDate = new Date(parseInt(year), 0, 1);
            var date = moment(firstDate).add(parseInt(week) - 1, 'week');
            $($scope.reportCommon.ui.FromDate).val(date.startOf('week').format('DD/MM/YYYY'));
            $($scope.reportCommon.ui.ToDate).val(date.endOf('week').format('DD/MM/YYYY'));
        },

        changeYear: function () {
            var year = $(".select-year").val();
            var week = $(".select-week").val();
            var firstDate = new Date(parseInt(year), 0, 1);
            var date = moment(firstDate).add(parseInt(week) - 1, 'week');
            $($scope.reportCommon.ui.FromDate).val(date.startOf('week').format('DD/MM/YYYY'));
            $($scope.reportCommon.ui.ToDate).val(date.endOf('week').format('DD/MM/YYYY'));
        }
    };
});