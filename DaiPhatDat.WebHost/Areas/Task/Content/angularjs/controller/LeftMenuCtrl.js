//menu left
app.controller("LeftMenuCtrl", function ($scope, $controller, $q, $timeout, LeftMenuService, $window) {
    $controller('BaseCtrl', { $scope: $scope });

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    var filterParamId = urlParams.get('filterId');
    var viewParam = urlParams.get('view') == null ? '' : urlParams.get('view');

    $scope.private = '0';
    $scope.lable = '0';

    var ui = {
        folderCustomFilter: '#modal-advance-task .folder-custom-filter',
        isPrivateCustomFilter: '#modal-advance-task .is-private-custom-filter',
        isLableCustomFilter: '#modal-advance-task .is-lable-custom-filter',
        isCountCustomFilter: '#modal-advance-task .is-count-custom-filter',
        assignBySelect: '#modal-advance-task .assign-by-select',
        userCustomFilter: '#modal-advance-task .user-custom-filter',
        taskStatusSelect: '#modal-advance-task .task-status-select',
        assignToSelect: '#modal-advance-task .assign-to-select',
        taskTypeSelect: '#modal-advance-task .tracking-type-select',
        fromDateText: '#modal-advance-task .from-date-text',
        toDateText: '#modal-advance-task .to-date-text',
        fromDateOfFromDateText: '#modal-advance-task .from-date-of-from-date-text',
        fromDateOfToDateText: '#modal-advance-task .from-date-of-to-date-text',
        toDateOfFromDateText: '#modal-advance-task .to-date-of-from-date-text',
        toDateOfToDateText: '#modal-advance-task .to-date-of-to-date-text',
        statusTimeRadioButton: "#modal-advance-task input[name='status-time']",
        priorityProjectSelect: '#modal-advance-task .priority-project-select',
        natureTaskSelect: '#modal-advance-task .nature-task-select',
        projectSelect: '#modal-advance-task .project-select',
        hashtagProjectSelect: '#modal-advance-task .hashtag-project-select',
        hashtagTaskSelect: '#modal-advance-task .hashtag-task-select',
        isReportSelect: '#modal-advance-task .is-report-select',
        isWeirdoSelect: '#modal-advance-task .is-weirdo-select',
        manageMenuFilter: '#modal-advance-task .manage-menu-filter',
        isExtend: '#modal-advance-task .is-extend'
    };

    $scope.leftMenu = {

        item: {},

        control: {
            addTask: true,
            editTask: false,
            deleteTask: false
        },

        filter: {
            getUrl: function (node) {
                if (node.id == "#") {
                    return CommonUtils.RootURL("Task/Main/GetAdvanceFilterTree?") + "parentID=00000000-0000-0000-0000-000000000000"
                } else {
                    return CommonUtils.RootURL("Task/Main/GetAdvanceFilterTree?parentID=") + node.id
                }
            },
            currentNodeId: '',
            id: '#tree-nav-node',
            selectNode: function () {
                //this.getData();
            },
            keyword: '',
            pageIndex: 1,
            pageSize: 5
        },

        init: function () {
            this.getMenuRoot();
        },
        reload: function () {
            LeftMenuService.getNavigationLeftFilter().then(function (rs) {
                $scope.leftMenu.data = rs.data;
                $scope.leftMenu.data.forEach(function (e) {
                    var jsTree = $('#tree-nav-' + e.Code).jstree(true);
                    jsTree.refresh();
                })
            })
        },
        getMenuRoot: function () {
            $scope.showLoading(null);
            LeftMenuService.getNavigationLeftFilter().then(function (rs) {
                $scope.leftMenu.data = rs.data;
                setTimeout(function () {
                    $scope.leftMenu.data.forEach(function (e) {
                        $("#menu-item-" + e.Code).addClass('menu-item-open');
                        $("#menu-item-" + e.Code).click();
                    })
                });
                $scope.hideLoading();
            })
        },

        loadMenu: function (id, code, isReload = false) {
            if (isReload === true) {
                $("#tree-nav-" + code).html('');
            }
            if ($("#tree-nav-" + code).html() == '') {
                $("#tree-nav-" + code).jstree({
                    'core': {
                        'data': {
                            "url": function (node) {
                                return CommonUtils.RootURL("Task/Main/GetAdvanceFilterTree?parentID=") + id
                            },
                            'type': 'GET',
                            'dataType': 'JSON',
                            'contentType': 'application/json;',
                            'async': 'false',
                            'data': function (node) {

                            }
                        },
                        "themes": {
                            "responsive": false,
                            "icons": false
                        },
                        'check_callback': true,
                    },
                    "plugins": ["themes", "ui"]
                }).bind('ready.jstree', function (event, data) {
                    if (filterParamId != '' && filterParamId != null) {
                        $('#tree-nav-TASK').jstree('select_node', filterParamId);
                    }
                    //else if (folderParamId != '' && folderParamId != null) {
                    //    $('#tree-nav-HSCN').jstree('select_node', folderParamId);
                    //}

                }).bind("select_node.jstree", function (event, data) {
                    var code = $(event.target).attr('data-code');
                    var filterId = '';
                    var islable = data.node.li_attr.islable;
                    if (code == "TASK") {
                        filterId = data.node.id;
                        if (data.node.li_attr.isedit == true) {
                            $scope.$apply(function (scope) {
                                $scope.leftMenu.control.editTask = true;
                                $scope.leftMenu.control.deleteTask = true;
                            });
                        }
                    }
                    if (filterId != filterParamId && !islable)
                        $window.location.href = CommonUtils.RootURL("Task/Home/Index?filterId=" + filterId + "&folderId=&view=" + viewParam)
                });
            }

        },

        clearFormTask: function () {
            $scope.leftMenu.item = {};
        },

        loadIsPrivate: function () {
            var options = '<option value="1">Cá nhân</option>';

            if ($scope.leftMenu.item.IsAdmin == true)
                options = options + '<option value="0">Công khai</option>';
            $(ui.isPrivateCustomFilter).html(options);
        },

        loadFormTask: function () {
            this.loadProjectFilterParam();
            this.loadProjectCategory();
            this.loadTaskItemCategory();
            this.loadTaskItemStatus();
            this.loadTaskItemPriority();
            this.loadNatureTask();
            this.loadUserInfo();
            this.loadIsPrivate();
            this.loadProject();
            if ($scope.lable == '1') {
                $(ui.manageMenuFilter).css("display", "none");
            }
            else {
                $(ui.manageMenuFilter).css("display", "block");
            }
            $(ui.taskTypeSelect).select2({
                placeholder: "Vai trò",
                width: '100%'
            });

            if ($scope.leftMenu.item.ProjectFilterParam.IsPrivate == false) {
                $scope.private = '0';
            }
            else {
                $scope.private = '1';
            }

            if ($scope.leftMenu.item.ProjectFilterParam.IsCount == true)
                $(ui.isCountCustomFilter).prop("checked", true);
            else
                $(ui.isCountCustomFilter).prop("checked", false);

            if ($scope.leftMenu.item.ProjectFilterParam.IsLable == true)
                $(ui.isLableCustomFilter).val("1");
            else
                $(ui.isLableCustomFilter).val("0");

            var userStr = $scope.leftMenu.item.ProjectFilterParam.Users != null ? $scope.leftMenu.item.ProjectFilterParam.Users : '';
            var users = userStr.split(",");
            for (var i = 0; i < users.length; i++) {
                var user = users[i];
                if (user != '') {
                    $(ui.userCustomFilter + " option[value='" + user + "']").prop("selected", true);
                }
            }

            $(ui.userCustomFilter).trigger('change');

            $scope.leftMenu.loadParamValue();
        },

        loadProjectFilterParam: function () {
            if ($(ui.folderCustomFilter).hasClass('select2-hidden-accessible')) {
                $(ui.folderCustomFilter).select2('destroy');
            }

            let data = $scope.leftMenu.item.ProjectFilterParams;
            let options = '<option value="00000000-0000-0000-0000-000000000000">Thư mục gốc</option>';
            data.map(param => options += '<option value="' + param.Id + '">' + param.Name + '</option>');

            $(ui.folderCustomFilter).html(options);
            $(ui.folderCustomFilter).val($scope.leftMenu.item.ProjectFilterParam.ParentID)
            $(ui.folderCustomFilter).select2({
                placeholder: 'Cha',
                width: '100%'
            });
        },

        loadProjectCategory: function () {
            if ($(ui.hashtagProjectSelect).hasClass('select2-hidden-accessible')) {
                $(ui.hashtagProjectSelect).select2('destroy');
            }

            let data = $scope.leftMenu.item.ProjectCategories;
            let options = '<option value="0">Nhãn danh mục</option>';
            data.map(param => options += '<option value="' + param.Name + '">' + param.Name + '</option>');

            $(ui.hashtagProjectSelect).html(options);
            $(ui.hashtagProjectSelect).select2({
                placeholder: 'Nhãn danh mục',
                width: '100%'
            });
        },

        loadTaskItemCategory: function () {
            if ($(ui.hashtagTaskSelect).hasClass('select2-hidden-accessible')) {
                $(ui.hashtagTaskSelect).select2('destroy');
            }

            let data = $scope.leftMenu.item.TaskItemCategories;
            let options = '<option value="0">Nhãn công việc</option>';
            data.map(param => options += '<option value="' + param.Name + '">' + param.Name + '</option>');

            $(ui.hashtagTaskSelect).html(options);
            $(ui.hashtagTaskSelect).select2({
                placeholder: 'Nhãn công việc',
                width: '100%'
            });
        },

        loadTaskItemStatus: function () {
            if ($(ui.taskStatusSelect).hasClass('select2-hidden-accessible')) {
                $(ui.taskStatusSelect).select2('destroy');
            }

            let data = $scope.leftMenu.item.TaskItemStatuses;
            let options = '';
            data.map(param => options += '<option value="' + param.Id + '">' + param.Name + '</option>');

            $(ui.taskStatusSelect).html(options);
            $(ui.taskStatusSelect).select2({
                //placeholder: 'Tình trạng công việc',
                width: '100%'
            });
        },

        loadTaskItemPriority: function () {
            if ($(ui.priorityProjectSelect).hasClass('select2-hidden-accessible')) {
                $(ui.priorityProjectSelect).select2('destroy');
            }

            let data = $scope.leftMenu.item.TaskItemPriorites;
            let options = '';
            data.map(param => options += '<option value="' + param.Id + '">' + param.Name + '</option>');

            $(ui.priorityProjectSelect).html(options);
            $(ui.priorityProjectSelect).select2({
                placeholder: 'Mức độ quan trọng',
                width: '100%'
            });
        },

        loadUserInfo: function () {
            if ($(ui.assignBySelect).hasClass('select2-hidden-accessible')) {
                $(ui.assignBySelect).select2('destroy');
            }

            if ($(ui.assignToSelect).hasClass('select2-hidden-accessible')) {
                $(ui.assignToSelect).select2('destroy');
            }

            if ($(ui.userCustomFilter).hasClass('select2-hidden-accessible')) {
                $(ui.userCustomFilter).select2('destroy');
            }

            let data = $scope.leftMenu.item.UserInfos;
            let optionAssignTos = '<option value="0">Giao cho</option><option value="CurentUser">Giao cho tôi</option>';
            data.map(assignTo => optionAssignTos += '<option value="' + assignTo.ID + '">' + assignTo.FullName + '</option>');
            $(ui.assignToSelect).html(optionAssignTos);

            let optionAssignBys = '<option value="0">Giao bởi</option><option value="CurentUser">Giao bởi tôi</option>';
            data.map(assignBy => optionAssignBys += '<option value="' + assignBy.ID + '">' + assignBy.FullName + '</option>');
            $(ui.assignBySelect).html(optionAssignBys);

            let optionUserPermissions = '';
            data.map(userPermission => optionUserPermissions += '<option value="' + userPermission.ID + '">' + userPermission.FullName + '</option>');
            $(ui.userCustomFilter).html(optionUserPermissions);

            $(ui.assignBySelect).select2({
                placeholder: 'Giao bởi',
                width: '100%'
            });
            $(ui.assignToSelect).select2({
                placeholder: 'Giao cho',
                width: '100%'
            });
            $(ui.userCustomFilter).select2({
                placeholder: 'Chọn người dùng được sử dụng',
                width: '100%'
            });
        },

        loadNatureTask: function () {
            if ($(ui.natureTaskSelect).hasClass('select2-hidden-accessible')) {
                $(ui.natureTaskSelect).select2('destroy');
            }

            let data = $scope.leftMenu.item.NatureTasks;
            let options = '';
            data.map(param => options += '<option value="' + param.Id + '">' + param.Name + '</option>');

            $(ui.natureTaskSelect).html(options);
            $(ui.natureTaskSelect).select2({
                placeholder: 'Tính chất công việc',
                width: '100%'
            });
        },

        loadProject: function () {
            let filter = {
                parentId: null,
                filterId: null,
                folderId: null,
                view: '',
                filter: {
                    GranttView: '',
                    PageSize: 100,
                    CurrentPage: 1
                }
            };
            LeftMenuService.getDataByProject(filter).then(function (rs) {
                if ($(ui.projectSelect).hasClass('select2-hidden-accessible')) {
                    $(ui.projectSelect).select2('destroy');
                }
                let data = rs.data.data.Result;
                let options = '<option value="-1">Tất cả</option>';
                data.map(param => options += '<option value="' + param.Id + '">' + param.Name + '</option>');

                $(ui.projectSelect).html(options);

                let param = $scope.leftMenu.item.ProjectFilterParam.ParamValue != null ? $scope.leftMenu.item.ProjectFilterParam.ParamValue : '';
                let arrParam = param.split(";");
                arrParam.map(e => {
                    if (e !== '') {
                        if (e.indexOf("@ProjectId") >= 0) {
                            let projectId = e.split(":")[1];

                            $(ui.projectSelect).val(projectId.split(','));
                            $(ui.projectSelect).trigger('change');
                        }
                    }
                });

                $(ui.projectSelect).select2({
                    placeholder: 'Danh mục',
                    width: '100%'
                });
            })
        },

        getParamValue: function () {
            let assignBy = $(ui.assignBySelect).val();
            let taskStatus = $(ui.taskStatusSelect).val();
            let assignTo = $(ui.assignToSelect).val();
            let taskType = $(ui.taskTypeSelect).val();
            let fromDate = $(ui.fromDateText).val();
            let toDate = $(ui.toDateText).val();

            let fromDateOfFromDate = $(ui.fromDateOfFromDateText).val();
            let fromDateOfToDate = $(ui.fromDateOfToDateText).val();

            let toDateOfFromDate = $(ui.toDateOfFromDateText).val();
            let toDateOfToDate = $(ui.toDateOfToDateText).val();

            let statusTime = $(ui.statusTimeRadioButton + ':checked').val();
            let priority = $(ui.priorityProjectSelect).val();
            let natureTask = $(ui.natureTaskSelect).val();
            let project = $(ui.projectSelect).val();

            let hashTagVal = $(ui.hashtagProjectSelect).val();
            let hashtag = $(ui.hashtagProjectSelect + ' option:selected').text();

            let hashTagTaskVal = $(ui.hashtagTaskSelect).val();
            let hashtagTask = $(ui.hashtagTaskSelect + ' option:selected').text();

            let isExtend = $(ui.isExtend).prop("checked");
            let isReport = $(ui.isReportSelect).val();
            let isWeirdo = $(ui.isWeirdoSelect).val();

            let paramValue = '';

            if (assignBy != '0')
                paramValue += assignBy !== null ? '@AssignBy:' + assignBy + ';' : '';
            paramValue += taskStatus.length > 0 ? '@TaskItemStatusId:' + taskStatus + ';' : '';

            if (assignTo != '0')
                paramValue += assignTo !== null ? '@AssignTo:' + assignTo + ';' : '';
            paramValue += taskType.length > 0 ? '@TaskType:' + taskType + ';' : '';
            paramValue += fromDate !== '' ? '@FromDate:' + fromDate + ';' : '';
            paramValue += toDate !== '' ? '@ToDate:' + toDate + ';' : '';

            paramValue += fromDateOfFromDate !== '' ? '@TaskFromDateOfFromDate:' + fromDateOfFromDate + ';' : '';
            paramValue += fromDateOfToDate !== '' ? '@TaskFromDateOfToDate:' + fromDateOfToDate + ';' : '';

            paramValue += toDateOfFromDate !== '' ? '@TaskToDateOfFromDate:' + toDateOfFromDate + ';' : '';
            paramValue += toDateOfToDate !== '' ? '@TaskToDateOfToDate:' + toDateOfToDate + ';' : '';


            paramValue += priority.length > 0 ? '@TaskItemPriorityId:' + priority + ';' : '';
            paramValue += natureTask.length > 0 ? '@TaskItemNatureId:' + natureTask + ';' : '';
            paramValue += project.length > 2 ? '@ProjectId:' + project + ';' : '';
            if (hashTagVal != '0')
                paramValue += hashTagVal !== '0' ? '@ProjectHashtag:' + hashtag + ';' : '';

            if (hashTagTaskVal != '0')
                paramValue += hashTagTaskVal !== '0' ? '@TaskHashtag:' + hashtagTask + ';' : '';
            paramValue += isReport !== '-1' ? '@IsReport:' + isReport + ';' : '';
            paramValue += isWeirdo !== '-1' ? '@IsWeirdo:' + isWeirdo + ';' : '';
            paramValue += '@StatusTime:' + statusTime + ';';

            if (isExtend) {
                paramValue += '@IsExtend:1';
            }

            return paramValue;
        },

        loadParamValue: function () {
            let param = $scope.leftMenu.item.ProjectFilterParam.ParamValue != null ? $scope.leftMenu.item.ProjectFilterParam.ParamValue : '';
            let arrParam = param.split(";");
            arrParam.map(e => {
                if (e !== '') {
                    if (e.indexOf("@TaskItemStatusId") >= 0) {
                        let taskItemStatusIdVal = e.split(":")[1];

                        $(ui.taskStatusSelect).val(taskItemStatusIdVal.split(','));
                        $(ui.taskStatusSelect).trigger('change');
                    }

                    if (e.indexOf("@TaskType") >= 0) {
                        let taskTypeVal = e.split(":")[1];
                        $(ui.taskTypeSelect).val(taskTypeVal.split(','));
                        $(ui.taskTypeSelect).trigger('change');
                    }

                    if (e.indexOf("@FromDate") >= 0) {
                        let fromDateVal = e.substring(e.indexOf(":") + 1);
                        $(ui.fromDateText).val(fromDateVal);
                    }

                    if (e.indexOf("@ToDate") >= 0) {
                        let toDateVal = e.substring(e.indexOf(":") + 1);
                        $(ui.toDateText).val(toDateVal);
                    }

                    if (e.indexOf("@TaskFromDateOfFromDate") >= 0) {
                        let fromDateVal = e.substring(e.indexOf(":") + 1);
                        $(ui.fromDateOfFromDateText).val(fromDateVal);
                    }

                    if (e.indexOf("@TaskFromDateOfToDate") >= 0) {
                        let toDateVal = e.substring(e.indexOf(":") + 1);
                        $(ui.fromDateOfToDateText).val(toDateVal);
                    }

                    if (e.indexOf("@TaskToDateOfFromDate") >= 0) {
                        let fromDateVal = e.substring(e.indexOf(":") + 1);
                        $(ui.toDateOfFromDateText).val(fromDateVal);
                    }

                    if (e.indexOf("@TaskToDateOfToDate") >= 0) {
                        let toDateVal = e.substring(e.indexOf(":") + 1);
                        $(ui.toDateOfToDateText).val(toDateVal);
                    }

                    if (e.indexOf("@StatusTime") >= 0) {
                        let statusTime = e.substring(e.indexOf(":") + 1);
                        $(ui.statusTimeRadioButton).filter('[value=' + statusTime + ']').prop('checked', true);

                        if (statusTime === 1 || statusTime === 2) {
                            $(ui.fromDateText).prop('disabled', true);
                            $(ui.toDateText).prop('disabled', true);
                        }
                    }
                    if (e.indexOf("@IsExtend") >= 0) {
                        let isExtend = e.substring(e.indexOf(":") + 1);
                        if (isExtend == '1') {

                            $(ui.isExtend).prop("checked", true);
                        }
                        else {
                            $(ui.isExtend).prop("checked", false);
                        }
                    }

                    if (e.indexOf("@TaskItemPriorityId") >= 0) {
                        let taskItemPriorityIdVal = e.split(":")[1];
                        $(ui.priorityProjectSelect).val(taskItemPriorityIdVal.split(','));
                        $(ui.priorityProjectSelect).trigger('change');
                    }

                    if (e.indexOf("@TaskItemNatureId") >= 0) {
                        let taskItemNatureIdVal = e.split(":")[1];
                        $(ui.natureTaskSelect).val(taskItemNatureIdVal.split(','));
                        $(ui.natureTaskSelect).trigger('change');

                    }

                    if (e.indexOf("@IsReport") >= 0) {
                        let isReportVal = e.split(":")[1];
                        $(ui.isReportSelect).val(isReportVal);
                        $(ui.isReportSelect).trigger('change');
                    }

                    if (e.indexOf("@IsWeirdo") >= 0) {
                        let isWeirdoVal = e.split(":")[1];
                        $(ui.isWeirdoSelect).val(isWeirdoVal);
                        $(ui.isWeirdoSelect).trigger('change');
                    }

                    if (e.indexOf("@ProjectHashtag") >= 0) {
                        let projectHashtag = e.split(":")[1];
                        $(ui.hashtagProjectSelect).val(projectHashtag);
                        $(ui.hashtagProjectSelect).trigger('change');
                    }

                    if (e.indexOf("@TaskHashtag") >= 0) {
                        let taskHashtag = e.split(":")[1];
                        $(ui.hashtagTaskSelect).val(taskHashtag);
                        $(ui.hashtagTaskSelect).trigger('change');
                    }

                    if (e.indexOf("@AssignTo") >= 0) {
                        let assignToVal = e.split(":")[1];
                        $(ui.assignToSelect).val(assignToVal);
                        $(ui.assignToSelect).trigger('change');
                    }

                    if (e.indexOf("@AssignBy") >= 0) {
                        let assignByVal = e.split(":")[1];
                        $(ui.assignBySelect).val(assignByVal);
                        $(ui.assignBySelect).trigger('change');
                    }
                }
            });
        },

        addTask: function () {
            this.clearFormTask();
            $scope.showLoading(null);
            LeftMenuService.getById('').then(function (rs) {
                $scope.leftMenu.item = rs.data;
                $(ui.isLableCustomFilter).eq(0).prop("checked", true);
                $(ui.isExtend).prop("checked", false);
                $scope.leftMenu.loadFormTask();
                var selectedId = $("#tree-nav-TASK").jstree("get_selected");
                $(ui.folderCustomFilter).val(selectedId);
                $(ui.folderCustomFilter).trigger('change');
                $scope.hideLoading(null);
                $("#modal-advance-task").modal();
            })
            //$scope.hideLoading(null);
        },

        editTask: function () {
            this.clearFormTask();
            var selectedId = $("#tree-nav-TASK").jstree("get_selected");
            $scope.showLoading(null);
            LeftMenuService.getById(selectedId).then(function (rs) {
                $scope.leftMenu.item = rs.data;
                $scope.private = rs.data.ProjectFilterParam.IsPrivate == true ? '1' : '0';
                if ($scope.private == '1') {
                    $(ui.isPrivateCustomFilter).eq(0).prop('checked', true);
                }
                else {
                    $(ui.isPrivateCustomFilter).eq(1).prop('checked', true);
                }
                if (rs.data.ProjectFilterParam.IsLable == true) {
                    $(ui.isLableCustomFilter).eq(1).prop('checked', true);
                }
                else {
                    $(ui.isLableCustomFilter).eq(0).prop('checked', true);
                }

                $scope.leftMenu.loadFormTask();
                $scope.hideLoading(null);
                $("#modal-advance-task").modal();
            })
        },

        deleteTask: function () {
            var selectedId = $("#tree-nav-TASK").jstree("get_selected");
            $scope.showConfirm('Bạn có chắc muốn xóa dữ liệu này?', function (rs) {
                if (rs) {
                    $scope.showLoading(null);
                    LeftMenuService.deleteAdvancedSearch(selectedId).then(function (rs) {
                        if (rs.data == true) {
                            toastr.success('Thành công!', 'Thông báo');
                            $window.location.reload();
                        }
                        else
                            toastr.error('Có lỗi xảy ra trong quá trình xử lý!', 'Thông báo');

                        $scope.hideLoading(null);
                    })
                }
            });

        },

        saveTask: function () {
            $scope.showLoading(null);
            if ($scope.private == '1') {
                $scope.leftMenu.item.ProjectFilterParam.IsPrivate = true;
            }
            else {
                $scope.leftMenu.item.ProjectFilterParam.IsPrivate = false;
            }

            if ($(ui.isCountCustomFilter).prop("checked") == true && $scope.lable == '0') {
                $scope.leftMenu.item.ProjectFilterParam.IsCount = true;
            }
            else {
                $scope.leftMenu.item.ProjectFilterParam.IsCount = false;
            }

            let isLable = $(ui.isLableCustomFilter).eq(1).prop('checked');
            let isPrivate = $(ui.isPrivateCustomFilter).eq(0).prop('checked');

            $scope.leftMenu.item.ProjectFilterParam.IsLable = isLable;
            $scope.leftMenu.item.ProjectFilterParam.IsPrivate = isPrivate;

            this.item.ProjectFilterParam.ParentID = $(ui.folderCustomFilter).val();

            this.item.ProjectFilterParam.ParamValue = this.getParamValue();
            LeftMenuService.SaveAdvancedSearch(this.item.ProjectFilterParam).then(function (rs) {
                if (rs.data == true) {
                    toastr.success('Thành công!', 'Thông báo');
                    $window.location.reload();
                }
                else
                    toastr.error('Có lỗi xảy ra trong quá trình xử lý!', 'Thông báo');
            });
        },

        onChangeIsLabel: function () {
            if ($scope.lable == '1') {
                $(ui.manageMenuFilter).css("display", "none");
            }
            else {
                $(ui.manageMenuFilter).css("display", "block");
            }
        },

        onJstreeSelectNode: function (e, data) {

        },

        onCheckedChangeLabel: function (val) {
            ;
            $scope.lable = val;
            if (val == 1) {
                $(ui.folderCustomFilter).val('00000000-0000-0000-0000-000000000000');
                $(ui.folderCustomFilter).trigger('change');
                $(ui.isPrivateCustomFilter).eq(0).prop('checked', true);
            }
        }
    };

    //$scope.leftMenu.init();
});