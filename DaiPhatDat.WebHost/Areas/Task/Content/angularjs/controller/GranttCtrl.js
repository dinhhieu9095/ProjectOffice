app.controller("MainCtrl", function ($scope, $controller, $q, $timeout, MainService) {
    $controller('BaseCtrl', { $scope: $scope });
    $scope.ProjectTaskItem = [];
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const filterId = urlParams.get('filterId');
    const folderId = urlParams.get('folderId');
    const view = urlParams.get('view');
    let parentId = '00000000-0000-0000-0000-000000000000';
    $scope.advanceFilter = {
        keyWord: ""
        , CurrentPage: 1
        , PageSize: 30
        , FromDate: ""
        , ToDate: ""
        , GranttView: "Week"
        , CurrentDate: moment(moment().startOf('isoweek').toDate()).format("DD/MM/YYYY")
    };
    $scope.getMonday = function (d) {
        d = new Date(d);
        var day = d.getDay(),
            diff = d.getDate() - day + (day == 0 ? -6 : 1); // adjust when day is sunday
        return new Date(d.setDate(diff));
    };
    //var startOfWeek = moment().startOf('isoweek').toDate();
    //var endOfWeek = moment().endOf('isoweek').toDate();
    //$scope.FromDate = moment(startOfWeek).format("DD/MM/YYYY");
    //$scope.advanceFilter.GranttView = 'Week';
    $scope.getRadioViewGrantt = function (option)
    {
        $scope.col_defs = [
            { field: "StatusName", displayName: "Status Name" },
            { field: "FromDateFormat", displayName: "From Date" },
            { field: "ToDateFormat", displayName: "To Date" },
            { field: "FullName", displayName: "Full Name" },
            { field: "DayBeforeW", displayName: "DayBeforeW" },
            { field: "T2", displayName: "T2" },
            { field: "T3", displayName: "T3" },
            { field: "DayAfterW", displayName: "DayAfterW" },
            { field: "PercentFinish", displayName: "PercentFinish" }
        ];
        //alert($scope.getMonday(new Date()));
        //$scope.selected = option.Code;
        $scope.advanceFilter.GranttView = option.Code;
        $scope.searchData();
    }
    //$scope.options = ['red', 'blue', 'yellow', 'green'];
    $scope.radioViewGrantt =
        [{
        Id: 1,
        Code: "Week",
        Text: "Tuần",
        IsChoice: "true"
         }
        , {
            Id: 2,
            Code: "Month",
            Text: "Tháng",
            IsChoice: "false"
    }, {
            Id: 3,
            Code: "Quater",
            Text: "Quý",
            IsChoice: "false"
        },
        {
            Id: 4,
            Code: "Year",
            Text: "Năm",
            IsChoice: "false"
        }
        ]

    $scope.init = function () {
        //debugger;
        ////$("#FromDate").value($scope.FromDate);

        //$scope.advanceFilter.FromDate=moment(startOfWeek).format("DD/MM/YYYY");
        //$scope.advanceFilter.ToDate = moment(endOfWeek).format("DD/MM/YYYY");
        $scope.tree_data = []
        $scope.my_tree = {};
        $scope.expanding_property = "Name";
        $scope.col_defs = [
            { field: "StatusName", displayName: "Status Name" },
            { field: "FromDateFormat", displayName: "From Date" },
            { field: "ToDateFormat", displayName: "To Date" },
            { field: "FullName", displayName: "Full Name" },
            { field: "DayBeforeW", displayName: "DayBeforeW" },
            { field: "T2", displayName: "T2" },
            { field: "T3", displayName: "T3" },
            { field: "T4", displayName: "T4" },
            { field: "T5", displayName: "T5" },
            { field: "T6", displayName: "T6" },
            { field: "T7", displayName: "T7" },
            { field: "CN", displayName: "CN" },
            { field: "DayAfterW", displayName: "DayAfterW" },
            { field: "PercentFinish", displayName: "PercentFinish" }
        ];
        $scope.getDataByProject(parentId);
    }
    $scope.searchData = function () {
        $scope.tree_data = []
        //$scope.my_tree = {};
        //$scope.expanding_property = "Name";
        //$scope.col_defs = [
        //    { field: "StatusName", displayName: "Status Name" },
        //    { field: "FromDateFormat", displayName: "From Date" },
        //    { field: "ToDateFormat", displayName: "To Date" },
        //    { field: "FullName", displayName: "Full Name" },
        //    { field: "DayBeforeW", displayName: "DayBeforeW" },
        //    { field: "T2", displayName: "T2" },
        //    { field: "T3", displayName: "T3" },
        //    { field: "T4", displayName: "T4" },
        //    { field: "T5", displayName: "T5" },
        //    { field: "T6", displayName: "T6" },
        //    { field: "T7", displayName: "T7" },
        //    { field: "CN", displayName: "CN" },
        //    { field: "DayAfterW", displayName: "DayAfterW" },
        //    { field: "PercentFinish", displayName: "PercentFinish" }
        //];
        $scope.getDataByProject(parentId);
    }
    $scope.callbackFunctionInController = function (branch) {
        if (branch.HasChildren) {
            if (!branch.HasPagination) {
                $scope.advanceFilter.CurrentPage = 1;
            }
            else {
                $scope.advanceFilter.CurrentPage = branch.CurrentPage;
            }

            if (branch.children.length > 0) {
                $scope.$apply(function () {
                    branch.children.splice(0, branch.children.length);
                });
            }
            $scope.getDataByProject(branch.Id, branch);
            //branch.children.push({ Id: 1, ParentId: 2, StatusName: "Test1", FromDate: "ddd", ToDate: "ddd", FullName: "test2", Type: "Te", HasChildren: true })
        }
        else if (branch.HasPagination) {
            $scope.advanceFilter.CurrentPage = branch.CurrentPage;
            $scope.getDataByProject(branch.ParentId, branch);
            //$scope.$apply(function () {
            //    branch.children.splice(0, branch.length);
            //});
        }
    };
    $scope.getDataByProject = function (parentId, branch) {
        //$scope.showLoading(null);
        var data = JSON.stringify({
            parentId: parentId,
            filterId: filterId,
            folderId: folderId,
            view: view,
            filter: $scope.advanceFilter
        });
        let promises = [MainService.getDataByProject(data)];
        $q.all(promises).then(function (rs) {

            if (rs[0].data.status) {
                var myTreeData = rs[0].data.data.Result;
                if (branch !== undefined) {
                    if (branch.HasChildren) {
                        branch.HasLoading = false;
                        for (var i = 0; i < myTreeData.length; i++) {
                            $scope.my_tree.add_branch(branch, myTreeData[i]);
                            //branch.children.push(myTreeData[i]);

                        }
                    }
                    else if (branch.HasPagination) {
                        var parentBranch = $scope.my_tree.get_parent_branch(branch);
                        if (branch.Type === "project") {
                            $scope.my_tree.delete_branch_pagination_roof();
                        }
                        else {
                            $scope.my_tree.delete_branch_pagination_children(parentBranch);
                        }
                        for (var i = 0; i < myTreeData.length; i++) {
                            //$scope.tree_data.push();
                            //$scope.my_tree.add_root_branch(myTreeData[i])
                            $scope.my_tree.add_branch(parentBranch, myTreeData[i]);
                        }
                    }
                    //c
                }
                else {
                    //$scope.my_tree.delete_branch_pagination(branch);
                    if ($scope.tree_data.length>0) {
                        for (var i = 0; i < myTreeData.length; i++) {
                            //$scope.tree_data.push();
                            $scope.tree_data.push(myTreeData[i]);
                        }
                    }
                    else {
                        $scope.tree_data = myTreeData;
                    }
               
                    //debugger;
                    //if ($scope.ProjectTaskItem.length > 0) {
                    //    //$scope.ProjectTaskItem = $scope.ProjectTaskItem.concat(myTreeData);
                    //}
                    //else {

                    //    //if (myTreeData.length > 0) {
                    //    //    var iPrCurrentPage = myTreeData[0].CurrentPage;
                    //    //    var iPrTotalPage = myTreeData[0].TotalRecord;
                    //    //    var iPrPageSize = myTreeData[0].PageSize;

                    //    //    //myTreeData.splice(myTreeData.length, 0, myTreeData[myTreeData.length - 1]);
                    //    //    //var more = myTreeData.slice(-1)[0];
                    //    //    //more.Id = "98D8DE8C-F079-49CF-9B0E-48D2E7BCA67F";
                    //    //    //more.HasPagination = true;
                    //    //    //more.Name = "Phân trang";
                    //    //    //more.HasChildren = false;
                    //    //    //if (iPrPageSize * iPrCurrentPage < iPrTotalPage) {
                    //    //    //    //$scope.my_tree.add_root_branch(more);
                    //    //    //    myTreeData.splice(myTreeData.length, 0, more);
                    //    //    //    //myTreeData.push(more);
                    //    //    //    //myTreeData.push({ Id: 1, ParentId: 2, StatusName: "Test1", FromDate: "ddd", ToDate: "ddd", FullName: "test2", Type: "Te", HasChildren: true })
                    //    //    //    //myTreeData.push({ Id: "47A34F8B-DB48-4689-8BAE-A8D782C73ECD", ParentId: more.ParentId, StatusName: "", FromDate: "", ToDate: "", FullName: "", Type: "", HasChildren: false, HasPagination: true })
                    //    //    //}

                    //    //}
                    //    //$scope.ProjectTaskItem = myTreeData;
                    //    //$scope.tree_data = myTreeData;
                    //    //for (var i = 0; i < myTreeData.length; i++) {
                    //    //    //$scope.tree_data.push();
                    //    //    $scope.tree_data.push(myTreeData[i]);
                    //    //}
                    //}
                }
            }
            else {
                $scope.hideLoading();
                toastr.error(rs[0].data.data.msg, 'Thông báo')
            }
            $scope.hideLoading();
        }, function (er) {
            toastr.error(er, 'Thông báo')
            $scope.hideLoading();
        });
    }
    $scope.my_tree_handler = function (branch) {
        //////alert("my_tree_handler");
        ////console.log('you clicked on', branch)

    }
    function getTreeGrid(data, primaryIdName, parentIdName) {
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

            //if (parentId !=="00000000-0000-0000-0000-000000000000") {
            //    parent = treeObjs[parentId];

            //    if (parent.children) {
            //        parent.children.push(item);
            //    }
            //    else {
            //        parent.children = [item];
            //    }
            //}
            //else {
            //    rootIds.push(primaryKey);
            //}
            rootIds.push(primaryKey);
        }

        for (var i = 0; i < rootIds.length; i++) {
            tree.push(treeObjs[rootIds[i]]);
        };

        return tree;
    }
    function getTree(data, primaryIdName, parentIdName) {
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
    }
    $scope.init();

    // -- Tạo dự án --
    $scope.hasSubmit = false;
    $scope.ViewProject = function (action) {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            $scope.Project = {
                ProjectMembers: []
            };
            if (action === 'Update') {
                if ($scope.filter.projectId === undefined || $scope.filter.projectId === '') {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    toastr.error('Vui lòng chọn 1 dự án!', 'Thông báo');
                    return;
                }
                $scope.modalTitle = "Chỉnh sửa dự án"
                var param = {
                    id: $scope.filter.projectId
                }
                var promises = [MainService.GetProject(param), MainService.GetProjectTypes(), MainService.GetProjectPriorities(), MainService.GetProjectCategories()];
                $q.all(promises).then(function (rs) {
                    if (rs.data.Message != undefined && rs.data.Message == 'AccessDenied') {
                        toastr.error('Bạn không có quyền chỉnh sửa dự án này', 'Thông báo');
                        $scope.hideLoading();
                        $scope.hasSubmit = false;
                        return;
                    }
                    $scope.Project = rs[0].data;
                    $scope.ProjectTypes = rs[1].data;
                    $scope.ProjectPriorities = rs[2].data;
                    $scope.ProjectCategories = rs[3].data;
                    $scope.Project.CreatedDateText = moment($scope.Project.CreatedDate).format("DD/MM/YYYY");
                    $('.select2').select2({
                        placeholder: "Tất cả"
                    });
                    $('#ProjectCategory').select2({
                        placeholder: 'Chọn nhãn dự án',
                        tags: true,
                        width: '100%'
                    });
                    $('#AddEditProject').modal('show');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                }, function (er) {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                });
            } else if (action === 'New') {
                $scope.modalTitle = "Thêm dự án"
                var promises = [MainService.GetProjectTypes(), MainService.GetProjectPriorities(), MainService.GetProjectCategories()];
                $q.all(promises).then(function (rs) {
                    $scope.ProjectTypes = rs[0].data;
                    $scope.ProjectPriorities = rs[1].data;
                    $scope.ProjectCategories = rs[2].data;
                    if ($scope.ProjectTypes != undefined && $scope.ProjectTypes.length > 0) {
                        $scope.Project.ProjectTypeId = $scope.ProjectTypes[0].Id;
                    }
                    if ($scope.ProjectPriorities != undefined && $scope.ProjectPriorities.length > 0) {
                        $scope.Project.ProjectPriorityId = $scope.ProjectPriorities[0].Id;
                    }
                    $('.select2').select2({
                        placeholder: "Tất cả"
                    });
                    $('#ProjectCategory').select2({
                        placeholder: 'Chọn loại dự án',
                        tags: true,
                        width: '100%',
                    });
                    $('#AddEditProject').modal('show');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                }, function (er) {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                });
            }
        }
    }
    $scope.SaveProject = function () {

        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;

            var validate = true;
            if ($scope.Project.Summary == '' || $scope.Project.Summary == undefined) {
                toastr.error('Vui lòng nhập tên dự án', 'Thông báo');
                $('#FolderName').focus();
                validate = false;
            }
            if (validate == false) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                return;
            }
            MainService.SaveProject($scope.Project).then(function (rs) {
                if (rs.data !== undefined && rs.data.IsSuccess == true) {
                    toastr.success('Thành công!', 'Thông báo')
                    $('#AddEditProject').modal('hide');
                    //$scope.ReloadTreeFolderGroup();
                } else {
                    if (rs.data.Message == "ProcessExpired") {
                        toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
                        $('#AddEditProject').modal('hide');
                        //$scope.ReloadTreeFolderGroup();
                    } else {
                        toastr.error('Có lỗi trong quá trình xử lý!', 'Thông báo')
                    }
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
    $scope.DeleteProject = function () {
        if (!$scope.hasSubmit) {
            $scope.hasSubmit = true;
            if ($scope.filter.projectId === undefined || $scope.filter.projectId === '') {
                $scope.hasSubmit = false;
                toastr.error('Vui lòng chọn 1 dự án!', 'Thông báo');
                return;
            }
            var param = {
                Id: $scope.filter.projectId
            }
            MainService.GetProject(param).then(function (rs) {
                if (rs.data.Message != undefined && rs.data.Message == 'AccessDenied') {
                    toastr.error('Bạn không có quyền chỉnh sửa danh mục này', 'Thông báo');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    return;
                }
                $scope.showConfirm('Bạn có chắc muốn xóa danh mục này?', function (rs) {
                    if (rs == true) {
                        $scope.showLoading(null);
                        MainService.DeleteProject(param).then(function (rs) {
                            if (rs.data !== undefined && rs.data.IsSuccess == true) {
                                toastr.success('Thành công!', 'Thông báo')
                                $scope.filter.projectId = undefined;
                                //$scope.ReloadTreeFolderGroup();
                                //$scope.GetProjectPaging(1);
                            } else {
                                if (rs.data.Message == "ProcessExpired") {
                                    toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
                                    //$scope.ReloadTreeFolderGroup();
                                }
                                else
                                    if (rs.data.Message != undefined && rs.data.Message == 'AccessDenied') {
                                        toastr.error('Bạn không có quyền chỉnh sửa danh mục này', 'Thông báo');
                                        $scope.hideLoading();
                                        $scope.hasSubmit = false;
                                        return;
                                    }
                                    else {
                                        toastr.error('Có lỗi trong quá trình xử lý!', 'Thông báo')
                                    }
                            }
                            $scope.hideLoading();
                            $scope.hasSubmit = false;
                        }, function (er) {
                            toastr.error('Có lỗi trong quá trình xử lý!', 'Thông báo')
                            $scope.hideLoading();
                            $scope.hasSubmit = false;
                        });
                    } else {
                        $scope.hasSubmit = false;
                    }
                });
            }, function (er) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
            });
        }
    }
    $scope.CloseFolderGroup = function () {
        $scope.Project = {
        };
        $('#AddEditProject').modal('hide');
    }
    $scope.DeleteProjectMember = function (userId, index) {
        $scope.showConfirm('Bạn có chắc muốn xóa phân quyền người dùng này!', function (rs) {
            if (rs) {
                var deleteIds = [];
                deleteIds.push(userId);
                $scope.Project.ProjectMembers = $.grep($scope.Project.ProjectMembers, function (item, idx) {
                    return idx !== index;
                });
                toastr.success('Thành công', 'Thông báo');

            }
        });

    }
    $scope.AddProjectMember = function (datas) {
        debugger
        if (datas.length > 0) {
            var users = datas.filter(e => e.type == 'user');
            if (users.length > 0) {
                for (var i = 0; i < users.length; i++) {
                    var check = $scope.Project.ProjectMembers.filter(e => e.UserName == users[i].code).length > 0;
                    if (check == false) {
                        var projectMember = {
                            UserName: users[i].code,
                            FullName: users[i].text,
                        }

                        $scope.Project.ProjectMembers.push(projectMember);
                    }
                }
                $scope.Project.ProjectMembers.sort((a, b) => (a.FullName > b.FullName) ? 1 : ((b.FullName > a.FullName) ? -1 : 0));
            }
        }
        $scope.$apply();
    }
    $scope.filterOrg = {
        getUrl: function (node) {
            return CommonUtils.RootURL("Org/GetOrgUserChart")
        },
        modalId: '#org-chart-modal',
        treeId: '#org-user-chart-modal',
        submitId: '#org-chart-submit-id',
        selectNode: function () {
        },
        submitOrgChart: function (datas) {
            $scope.AddProjectMember(datas);
        },
    }
    $scope.ChooseUser = function () {
        $scope.showLoading(null);
        surePortalCommon.initJstreeCheckBox($scope.filterOrg);
        $scope.hideLoading();
    }
    // --------------
});