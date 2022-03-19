app.directive('droppable', ['$parse',
    function ($parse) {
        return {
            link: function (scope, element, attr) {
                var el = element[0];
                function onDragOver(e) {
                    if (e.preventDefault) {
                        e.preventDefault();
                    }
                    if (e.stopPropagation) {
                        e.stopPropagation();
                    }
                    //var targetParent = tbody.attr('drag-id') ? tbody.attr('drag-id') : void 0;
                    //e.dataTransfer.dropEffect = sourceParent !== targetParent || typeof attr.ngRepeat === "undefined" ? 'none' : 'move';
                    e.dataTransfer.dropEffect = 'move';
                    return false;
                }
                function onDrop(e) {
                    if (e.preventDefault) {
                        e.preventDefault();
                    }
                    if (e.stopPropagation) {
                        e.stopPropagation();
                    }
                    var data = e.dataTransfer.getData("Text");

                    data = angular.fromJson(data);

                    var dropfn = attr.drop;
                    var fn = $parse(attr.drop);
                    var rowDroppedAt = jQuery(e.target).parents("tr")[0].getAttribute("dragdata");
                    scope.$apply(function () {
                        scope[dropfn](data, rowDroppedAt);
                    });
                }
                el.addEventListener("dragover", onDragOver);
                el.addEventListener("drop", onDrop);
            }
        };
    }
]);

app.directive('draggable', function () {

    return {
        link: function (scope, elem, attr) {
            var el = elem[0];
            elem.attr("draggable", true);
            var dragDataVal = '';
            var draggedGhostImgElemId = '';
            attr.$observe('dragdata', function (newVal) {
                dragDataVal = newVal;
            });
            attr.$observe('dragimage', function (newVal) {
                draggedGhostImgElemId = newVal;
            });

            el.addEventListener("dragstart", function (e) {
                if (dragDataVal !== undefined && dragDataVal != "") {
                    var sendData = angular.toJson(dragDataVal);
                    var dataSS = angular.fromJson(dragDataVal);
                    if (dataSS.DragDrop === false) {
                        e.dataTransfer.effectAllowed = 'none';
                    }
                    e.dataTransfer.setData("Text", sendData);
                    var dragFn = attr.drag;
                    if (dragFn !== 'undefined') {
                        scope.$apply(function () {
                            scope[dragFn](sendData);
                        })
                    }
                }
            });
        }
    };
});
app.controller("AdminCategoryCtrl", function ($scope, $controller, $q, $timeout, fileFactory, AdminCategoryService) {
    $controller('BaseCtrl', { $scope: $scope });
    $controller('TaskItemDetailCtrl', { $scope: $scope });
    $scope.ProjectTaskItem = [];
    $scope.projectFilters = [{
        Name: 'Tất cả',
        Id: ''
    }];
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const filterId = urlParams.get('filterId');
    const folderId = urlParams.get('folderId');
    var view = urlParams.get('view');
    let parentId = '';
    $scope.ShowType = 0; //Kiem Tra View Dang Nao
    $scope.advanceFilter = { keyWord: "", CurrentPage: 1, PageSize: 20 };
    $scope.selectedRow = null;
    $scope.projectFilterId = undefined;

    $scope.init = function () {
        $scope.tree_data = []
        $scope.my_tree = {};
        $scope.expanding_property = "Name";
        $scope.ShowType = 0;
        $scope.col_defs = ColumnHeader;
        $scope.$example = $('#projectFilters').select2({
            placeholder: "Tất cả",
            width: '100%'
        });
        $('#projectFilters').on('select2:select', function (e) {
            var select = []
            if ($('#projectFilters').val() !== "undefined") {
                select = $scope.projectFilters.filter(e => e.ProjectId == $('#projectFilters').val());
            } else {
                select = $scope.projectFilters.filter(e => e.ProjectId !== undefined);
            }
            if ($scope.ShowType === 0) {
                $scope.tree_data = [];
                $scope.my_tree.reset_project_all();
                $scope.tree_data = select;
                $scope.$apply();
            }
        });
        $scope.GetAdminCategories(parentId);
    };
    $scope.callbackDblFunctionInController = function (branch) {
        if (!$("#kt_demo_panel").hasClass('offcanvas-on')) {
            $("#kt_demo_panel_toggle").click();
        }

        if (branch.Type == "project") {
            $("#tabli_projectStatus").children().addClass("active");
            $("#tabli_attachmentZone").children().removeClass("active");
            $("#tabli_projectTaskRoot").children().removeClass("active");
            $("#tabli_projectReport").children().removeClass("active");
        }
        else {
            $scope.taskItem.init(branch.Id);
        }
    };

    $scope.CloseTaskDetail = function () {
        $('#modal-task-detail').hide();
    };

    $scope.callbackFunctionDragDropInController = function (branchS, branchD, branchChildrenD, indexS, indexD) {
        var dataS = angular.fromJson(indexS);
        var dataD = angular.fromJson(indexD);
        if (dataS.Id !== dataD.Id) {
            if (branchS != undefined) {
                if (
                    dataD.Id !== dataS.ParentId &&
                    dataS.Id !== dataD.ParentId &&
                    dataS.ProjectId === dataD.ProjectId
                    // && dataS.level === dataD.level
                ) {
                    var data = JSON.stringify({
                        IdSource: dataS.Id,
                        IdDestination: dataD.Id,
                        TaskType: "TASK",
                        MoveType: "MOVE"
                    });
                    let promises = [AdminCategoryService.moveDataByTask(data)];
                    $q.all(promises).then(function (rs) {
                        if (rs[0].data.status) {
                            //var myTreeData = rs[0].data.data.Result;
                            branchChildrenD.HasLoading = true;
                            if (!branchChildrenD.HasChildren) {
                                branchChildrenD.HasChildren = true;
                            }
                            if (branchChildrenD.HasChildren) {
                                if (!branchChildrenD.HasPagination) {
                                    $scope.advanceFilter.CurrentPage = 1;
                                }
                                else {
                                    $scope.advanceFilter.CurrentPage = branchChildrenD.CurrentPage;
                                }
                                if (branchChildrenD.children.length > 0) {
                                    branchChildrenD.children.splice(0, branchChildrenD.children.length);
                                }
                                $scope.GetAdminCategories(branchChildrenD.Id, branchChildrenD);
                                
                                //xoa item duoc di chuyen
                                var positionS = branchS.children.map(function (e) { return e.Id; }).indexOf(dataS.Id);
                                //var element = branchS.children[positionS];
                                branchS.children.splice(positionS, 1);
                                if (branchS.children.length == 0) {
                                    branchS.HasChildren = false;
                                }
                            }
                        }
                        else {
                            toastr.error(rs[0].data.msg)
                        }
                    }, function (er) {
                        toastr.error(er, 'Thông báo')
                    });


                    //var indexA = branchS.children.map(function (e) { return e.Id; }).indexOf(dataS.Id);
                    //var indexB = branchS.children.map(function (e) { return e.Id; }).indexOf(dataD.Id);
                    ////var indexA = array_object.children.indexOf(dataS);
                    ////var indexB = array_object.children.indexOf(dataD);
                    //var element = branchS.children[indexA];
                    //branchS.children.splice(indexA, 1);
                    //branchD.children.splice(indexB, 0, element);
                    ////array_object.children.splice(indexA, 0, array_object.children.splice(indexB, 1)[0]);
                    ////$scope.treeControl.add_branch(array_object, dataS);
                    ////for (_i = 0, _len = array_object.children.length; _i < _len; _i++) {
                    ////    if (array_object.children === dataD) {
                    ////        array_object.children.push(dataS);
                    ////    }
                    ////    if (array_object.children === dataS) {
                    ////        array_object.children[i]
                    ////    }
                    ////    child = _ref[_i];
                    ////    _results.push(do_f(child, level + 1));
                    ////}
                    ////array_object.children.splice(dataD, 0, array_object.children.splice(dataS, 1)[0]);
                    ////toastr.error('1', 'Thông báo')
                    //return 11;
                }
                //task
            }
            else {
                //Project
            }
        }
    };
    //Expand
    $scope.callbackFunctionInController = function (branch) {
        if (branch.HasPagination) {
            $scope.advanceFilter.CurrentPage = branch.CurrentPage;
            $scope.GetAdminCategories(branch.ParentId, branch);
            
        }
        else if (branch.HasChildren) {
            if (!branch.HasPagination) {
                $scope.advanceFilter.CurrentPage = 1;
            }
            else {
                $scope.advanceFilter.CurrentPage = branch.CurrentPage;
            }

            if (branch.children.length > 0) {
                $timeout(function () {
                    branch.children.splice(0, branch.children.length);
                }, 0);
            }
            $scope.GetAdminCategories(branch.Id, branch);
        }
        else {
            $scope.GetAdminCategories(branch.Id, branch);
        }
    };
    $scope.GetAdminCategories = function (parentId, branch) {
        if (branch === undefined) {
            $scope.advanceFilter.CurrentPage = 1;
        }
        $scope.onSelectItem(branch);
        var data = JSON.stringify({
            parentId: parentId,
            filterId: filterId,
            folderId: folderId,
            view: view,
            filter: $scope.advanceFilter
        });
        var promises = [];
        promises = [AdminCategoryService.GetAdminCategories(data)];
        $q.all(promises).then(function (rs) {
            
            if (rs[0].data.status) {
                if (parentId == null || parentId == undefined || parentId == '') {
                    $scope.projectFilters = [{
                        Name: 'Tất cả',
                        ProjectId: undefined
                    }];
                    $scope.projectFilters = $scope.projectFilters.concat(rs[0].data.data.Result);
                }
                if ($scope.ShowType === 0) {
                    var myTreeData = rs[0].data.data.Result;
                    if (branch !== undefined) {
                        if (branch.HasChildren) {
                            branch.HasLoading = false;
                            for (var i = 0; i < myTreeData.length; i++) {
                                $scope.my_tree.add_branch(branch, myTreeData[i]);
                                //branch.children.push(myTreeData[i]);

                            }
                        }
                        if (branch.HasPagination) {
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
                        else if (!branch.HasChildren) {
                            branch.HasLoading = false;
                            branch.HasChildren = true;
                            for (var i = 0; i < myTreeData.length; i++) {
                                $scope.my_tree.add_branch(branch, myTreeData[i]);
                                //branch.children.push(myTreeData[i]);

                            }
                        }
                        $scope.my_tree.select_branch($scope.my_tree.get_branch($scope.selectedRow))
                    }
                    else {
                        if (parentId === null) {
                            if ($scope.my_tree && $scope.my_tree != undefined) {
                                $scope.my_tree.reset_project_all();
                            }
                        }
                        //$scope.my_tree.delete_branch_pagination(branch);
                        for (var i = 0; i < myTreeData.length; i++) {
                            //$scope.tree_data.push();
                            $scope.tree_data.push(myTreeData[i]);
                        }
                    }
                }
            }
            else {
                $scope.hideLoading();
                toastr.error(rs[0].data.data.msg, 'Thông báo');
            }
            $scope.hideLoading();
        }, function (er) {
            toastr.error(er, 'Thông báo')
            $scope.hideLoading();
        });
    }
    $scope.onSelectItem = function (branch) {
        if (branch !== null && branch !== undefined) {
            $scope.selectedRow = branch;
            if ($scope.selectedRow.Type === 'project') {
                $scope.isEditProject = true;
                $scope.isAddTask = true;
                $scope.isEditTask = false;
                $scope.isDeleteTask = false;
                $scope.isDeleteProject = true;

            } else {
                $scope.isEditProject = false;
                $scope.isAddTask = true;
                $scope.isEditTask = true;
                $scope.isDeleteTask = true;
                $scope.isDeleteProject = false;
            }
        } else {
            $scope.isEditProject = false;
            $scope.isAddTask = false;
            $scope.isEditTask = false;
            $scope.isDeleteTask = false;
            $scope.isDeleteProject = false
        }
    }

    $scope.init();

    // -- Tạo dự án --
    $scope.filter = {};
    $scope.fileTemps = [];
    $scope.hasSubmit = false;
    $scope.ViewAdminCategory = function (action) {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            $scope.Project = {
            };
            $scope.fileTemps = [];
            if (action === 'Update') {
                if ($scope.selectedRow == null || $scope.selectedRow.Type !== 'project') {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    toastr.error('Vui lòng chọn 1 dự án!', 'Thông báo');
                    return;
                }
                $scope.filter.projectId = $scope.selectedRow.ProjectId;
                $scope.modalTitle = "Chỉnh sửa dự án"
                var param = {
                    id: $scope.filter.projectId
                }
                var promises = [AdminCategoryService.GetAdminCategory(param)];
                $q.all(promises).then(function (rs) {
                    if (rs[0].data.Message != undefined && rs[0].data.Message == 'AccessDenied') {
                        toastr.error('Bạn không có quyền chỉnh sửa dự án này', 'Thông báo');
                        $scope.hideLoading();
                        $scope.hasSubmit = false;
                        return;
                    }
                    $scope.Project = rs[0].data;

                    $('#AddEditProject').modal('show');
                    $timeout(function () {
                        $('#ProjectSummary').focus();
                    }, 0);
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                }, function (er) {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                });
            } else if (action === 'New') {
                $scope.modalTitle = "Thêm dự án"
                var promises = [AdminCategoryService.GetAdminCategory({ id: null })];
                $q.all(promises).then(function (rs) {
                    $scope.Project = rs[0].data
                    $('#AddEditProject').modal('show');
                    $timeout(function () {
                        $('#ProjectSummary').focus();
                    }, 0);
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                }, function (er) {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                });
            }
        }
    }
    $scope.SaveAdminCategory = function () {

        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;

            var validate = true;
            if ($scope.Project.Summary == '' || $scope.Project.Summary == undefined) {
                toastr.error('Vui lòng nhập tên dự án', 'Thông báo');
                $('#ProjectSummary').focus();
                validate = false;
            }
            if (validate == false) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                return;
            }
            AdminCategoryService.SaveAdminCategory($scope.Project).then(function (result) {
                var rs = result && result.data;
                if (rs !== undefined && rs.IsSuccess == true) {
                    toastr.success('Thành công!', 'Thông báo')
                    $scope.CloseAdminCategory();
                    $scope.GetAdminCategories(null);
                } else {
                    if (rs !== undefined && rs.Message == "ProcessExpired") {
                        toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
                        $scope.CloseAdminCategory();
                        $scope.GetAdminCategories(null);
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
    $scope.DeleteAdminCategory = function () {
        if (!$scope.hasSubmit) {
            $scope.hasSubmit = true;
            if ($scope.selectedRow == null || $scope.selectedRow.Type !== 'project') {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                toastr.error('Vui lòng chọn 1 dự án!', 'Thông báo');
                return;
            }
            $scope.filter.projectId = $scope.selectedRow.ProjectId;
            var param = {
                Id: $scope.filter.projectId
            }
            AdminCategoryService.GetAdminCategory(param).then(function (rs) {
                if (rs.data.Message != undefined && rs.data.Message == 'AccessDenied') {
                    toastr.error('Bạn không có quyền chỉnh sửa danh mục này', 'Thông báo');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    return;
                }
                $scope.showConfirm('Bạn có chắc muốn xóa danh mục này?', function (rs) {
                    if (rs == true) {
                        $scope.showLoading(null);
                        AdminCategoryService.DeleteAdminCategory(param).then(function (rs) {
                            if (rs.data !== undefined && rs.data.IsSuccess == true) {
                                toastr.success('Thành công!', 'Thông báo')
                                $scope.filter.projectId = undefined;
                                $scope.GetAdminCategories(null);
                            } else {
                                if (rs.data.Message == "ProcessExpired") {
                                    toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
                                    $scope.GetAdminCategories(null);
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
    $scope.CloseAdminCategory = function () {
        
        $scope.Project = {
        };
        var $select = $('.select2').select2();
        //console.log($select);
        $select.each(function (i, item) {
            //console.log(item);
            $(item).select2("destroy");
        });
        $('#FromDateText').datepicker(
            { format: "dd/mm/yyyy" }).off('changeDate');
        $('#ToDateText').datepicker(
            { format: "dd/mm/yyyy" }).off('changeDate');
        $('#FromDateText').datepicker("destroy");
        $('#ToDateText').datepicker("destroy");
        $('#ProjectMember').select2('destroy');
        $('#collapseProject').collapse("hide");
        $('#ProjectCategory').select2('destroy');
        $('#ProjectCategory').val(null);
        $("#ProjectCategory option").remove();
        $('#AddEditProject').modal('hide');
    }
    $scope.filterOrgForTask = {
        getUrl: function (node) {
            return CommonUtils.RootURL("Org/GetOrgUserChart")
        },
        modalId: '#org-chart-modal',
        treeId: '#org-user-chart-modal',
        submitId: '#org-chart-submit-id',
        selectNode: function () {
        },
        submitOrgChart: function (datas) {
            $scope.AddTaskMember(datas);
        },
    }
    $scope.filterOrgForAssignBy = {
        getUrl: function (node) {
            return CommonUtils.RootURL("Org/GetOrgUserChart")
        },
        modalId: '#org-chart-modal',
        treeId: '#org-user-chart-modal',
        submitId: '#org-chart-submit-id',
        selectNode: function () {
        },
        submitOrgChart: function (datas) {
            $scope.AddAssignBy(datas);
        },
    }
    $scope.ChooseUser = function (type) {
        $scope.showLoading(null);
        if (type == 'TaskItem') {
            vanPhongDienTuCommon.initJstreeCheckBox($scope.filterOrgForTask);
        } else
            if (type == 'AssignBy') {
                vanPhongDienTuCommon.initJstreeCheckBox($scope.filterOrgForAssignBy);
            } else {
                vanPhongDienTuCommon.initJstreeCheckBox($scope.filterOrg);
            }
        $scope.hideLoading();
    }
    $scope.ParsingFile = function (files) {
        if (files.length !== 0) {
            if (files.length > 0) {
                for (var i = 0; i < files.length; i++) {
                    var n = files[i].name.lastIndexOf('.');
                    var fileType = files[i].name.substring(n + 1);
                    if (!isFileInvalid(files[i].size, fileType)) {
                        return;
                    }
                }
                $scope.fileTemps = $scope.fileTemps.concat(files);

            } else {
                Notify({ title: "Thông báo", message: "Có lỗi", type: "error", delay: 2000 });
            }
        }
    }
    function isFileInvalid(fileSize, fileType) {
        var acceptedTypes = [];
        angular.forEach(fileFactory.types.document, function (doc) {
            acceptedTypes.push(doc);
        });
        angular.forEach(fileFactory.types.image, function (img) {
            acceptedTypes.push(img);
        });
        angular.forEach(fileFactory.types.zip, function (zip) {
            acceptedTypes.push(zip);
        });
        angular.forEach(fileFactory.acceptedTypes, function (type) {
            acceptedTypes.push(type);
        });

        var error = true;
        if (fileSize > 31250000) {
            toastr.error('Vui lòng không chọn file quá 25MB!', 'Thông báo');
            error = false;
        }
        if (acceptedTypes.indexOf(fileType.toLowerCase()) === -1) {
            toastr.error('Tập tin có định đang không hợp lệ!', 'Thông báo');

            error = false;
        }
        if (error == false) {
            $('#datafile').each(function () {
                $(this).files = null;
                $(this).val(null);
            });
            //$('#datafile').val(null);
            $('.fileinput-remove-button').each(function () {
                $(this).click();
            });
        }
        return error;
    }
    $scope.DeletePendingFile = function (index) {
        $scope.showConfirm('Bạn có chắc muốn xóa?', function (rs) {
            if (rs) {
                $scope.fileTemps.splice(index, 1);
            }
        });
    }
    

    // --------------
    // ---- Tạo công việc -----
    $scope.filterTask = {};
    $scope.ViewTaskItem = function (action) {

        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            $scope.fileTemps = [];
            if ($scope.selectedRow == null) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                toastr.error('Vui lòng chọn 1 Dự án/công việc!', 'Thông báo');
                return;
            }
            $scope.TaskItem = {
                TaskItemAssigns: [],
                TaskItemCategories: [],
                ProjectId: $scope.selectedRow.ProjectId,
                IsParentAuto: $scope.selectedRow.IsAuto,
                ParentFromDateText: $scope.selectedRow.FromDateFormat,
                ParentToDateText: $scope.selectedRow.ToDateFormat,
            };
            if ($scope.selectedRow.Type !== 'project') {
                $scope.TaskItem.ParentId = $scope.selectedRow.Id;
            } else {
                $scope.TaskItem.ParentId = null;
            }
            $scope.filterTask.taskId = $scope.selectedRow.Id;
            $scope.filterTask.projectId = $scope.selectedRow.ProjectId;

            if (action === 'Update') {
                $scope.modalTitle = "Chỉnh sửa công việc";
                var param = {
                    id: $scope.selectedRow.Id
                }
                var promises = [AdminCategoryService.GetTaskItem(param), AdminCategoryService.GetTaskPriorities()];
                $q.all(promises).then(function (rs) {
                    if (rs[0].data.Message != undefined && rs[0].data.Message == 'AccessDenied') {
                        toastr.error('Bạn không có quyền chỉnh sửa công việc này', 'Thông báo');
                        $scope.hideLoading();
                        $scope.hasSubmit = false;
                        return;
                    }
                    $scope.TaskItem = rs[0].data;
                    $scope.TaskItemPriorities = rs[1].data;
                    if ($scope.TaskItem.IsReport == 1) {
                        $('#TaskGroupType').val('1');
                    } else if ($scope.TaskItem.IsGroupLabel == 1) {
                        $('#TaskGroupType').val('2');
                    } else {
                        $('#TaskGroupType').val('0');
                    }
                    
                    $('.select2css').select2({
                        placeholder: "Tất cả",
                        width: '100%',
                    });
                    $('#TaskGroupType').on('select2:select', function (e) {
                        if ($('#TaskGroupType').val() == '1') {
                            $scope.TaskItem.IsReport = true;
                            $scope.TaskItem.IsGroupLabel = false;
                        } else if ($('#TaskGroupType').val() == '2') {
                            $scope.TaskItem.IsGroupLabel = true;
                            $scope.TaskItem.IsReport = false;
                        } else {
                            $scope.TaskItem.IsReport = false;
                            $scope.TaskItem.IsGroupLabel = false;
                        }
                        $scope.$apply();
                    });
                    $("#TaskItemAssign").select2({
                        width: "100%",
                        templateSelection: function (data, container) {
                            var selection = $('#TaskItemAssign').select2('data');
                            var idx = selection.indexOf(data);

                            if (data.selected == true) {
                                $scope.InitAssignType(idx, container);
                            } else {
                                $scope.InitAssignTypeFromSearch(data.id, container);
                            }
                            $(container).on("click", function (e) {
                                if (e.target.tagName.toLowerCase() === 'span') {
                                    $scope.DeleteAssign(idx, container);
                                } else {
                                    $scope.ChangeAssignType(idx, container);
                                }
                            });
                            return data.text;
                        },
                        dropdownCssClass: "hidden",
                        ajax: {
                            url: CommonUtils.RootURL("Task/TaskItem/SearchTaskAssign"),
                            dataType: 'json',
                            type: 'POST',
                            data: function (params) {
                                var query = {
                                    userName: params.term,
                                    taskItemAssigns: $scope.TaskItem.TaskItemAssigns
                                }
                                return query;
                            },
                            processResults: function (data) {
                                if (data.length > 0) {

                                }
                                var items = [];
                                for (var i = 0; i < data.length; i++) {
                                    var item = {
                                        id: data[i].AssignTo,
                                        text: data[i].AssignToFullName
                                    }
                                    items.push(item);
                                }
                                $scope.TaskItemAssignTemps = data;
                                return {
                                    results: items
                                };
                            },
                        }
                    })
                        /*.on("change", function (e) { $scope.SearchTaskAssign(e) })*/;
                    $('#TaskCategory').select2({
                        placeholder: 'Chọn loại công việc',
                        tags: true,
                        width: '100%',
                        data: $scope.TaskItem.TaskItemCategories
                    });
                    $('#TaskCategory').val($scope.TaskItem.TaskItemCategories);
                    $('#TaskCategory').trigger('change.select2');
                    $("#TaskFromDateText").val($scope.TaskItem.FromDateText);
                    $('#TaskFromDateText').datepicker(
                        { format: "dd/mm/yyyy" }).on('changeDate', function (e) {
                            var valid = true;
                            var date = moment($("#TaskFromDateText").val(), 'DD/MM/YYYY');
                            var fromdate = moment($scope.TaskItem.FromDateText, 'DD/MM/YYYY');

                            if (!$scope.TaskItem.IsParentAuto) {
                                var min = moment($scope.TaskItem.ParentFromDateText, 'DD/MM/YYYY');
                                if (date < min) {
                                    toastr.error('Ngày bắt đầu không hợp lệ', 'Thông báo');
                                    valid = false;
                                    $("#TaskFromDateText").datepicker("destroy");
                                    $("#TaskFromDateText").datepicker("setDate", fromdate.toDate());
                                }
                            }
                            var todate = moment($scope.TaskItem.ToDateText, 'DD/MM/YYYY');
                            if (valid === true && todate < date) {
                                $("#TaskToDateText").datepicker("destroy");
                                $("#TaskToDateText").datepicker("setDate", null);
                                $scope.TaskItem.ToDateText = null;
                            }
                            if (valid === true) {
                                $scope.TaskItem.FromDateText = $("#TaskFromDateText").val();
                            }
                            $('.datepicker').hide();
                        });
                    $("#TaskToDateText").val($scope.TaskItem.ToDateText);
                    $('#TaskToDateText').datepicker(
                        { format: "dd/mm/yyyy" }).on('changeDate', function (e) {
                            var valid = true;
                            var date = moment($("#TaskToDateText").val(), 'DD/MM/YYYY');
                            var todate = moment($scope.TaskItem.ToDateText, 'DD/MM/YYYY');

                            if (!$scope.TaskItem.IsParentAuto) {
                                var max = moment($scope.TaskItem.ParentToDateText, 'DD/MM/YYYY');
                                if (date > max) {
                                    toastr.error('Ngày đến hạn không hợp lệ', 'Thông báo');
                                    valid = false;
                                    $("#TaskToDateText").datepicker("destroy");
                                    $("#TaskToDateText").datepicker("setDate", todate.toDate());
                                }
                            }
                            var fromdate = moment($scope.TaskItem.FromDateText, 'DD/MM/YYYY');
                            if (valid === true && fromdate > date) {
                                $("#TaskFromDateText").datepicker("destroy");
                                $("#TaskFromDateText").datepicker("setDate", null);
                                $scope.TaskItem.FromDateText = null;
                            }
                            if (valid === true) {
                                $scope.TaskItem.ToDateText = $("#TaskToDateText").val();
                            }
                            $('.datepicker').hide();
                        });
                    $('#AddEditTaskItem').modal('show');
                    $timeout(function () {
                        $('#TaskName').focus();
                    }, 0);
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                }, function (er) {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                });
            } else if (action === 'New') {
                $scope.modalTitle = "Giao việc";
                var promises = [AdminCategoryService.GetTaskPriorities(), AdminCategoryService.GetNewTaskItem($scope.TaskItem)];
                $q.all(promises).then(function (rs) {
                    $scope.TaskItemPriorities = rs[0].data;
                    if ($scope.TaskItemPriorities != undefined && $scope.TaskItemPriorities.length > 0) {
                        $scope.TaskItem.TaskItemPriorityId = $scope.TaskItemPriorities[0].Id;
                    }
                    $scope.TaskItem = rs[1].data;
                    $scope.TaskItemAssignTemps = [];
                    angular.copy($scope.TaskItem.TaskItemAssigns, $scope.TaskItemAssignTemps);
                    $scope.TaskItem.TaskItemPriorityId = $scope.TaskItemPriorities[0].Id;
                    //Mặc định là task
                    $('#TaskGroupType').val('0');
                    $('.select2css').select2({
                        placeholder: "Tất cả",
                        width: '100%',
                    });
                    $('#TaskGroupType').on('select2:select', function (e) {
                        
                        if ($('#TaskGroupType').val() == '1') {
                            $scope.TaskItem.IsReport = true;
                            $scope.TaskItem.IsGroupLabel = false;
                        } else if ($('#TaskGroupType').val() == '2') {
                            $scope.TaskItem.IsGroupLabel = true;
                            $scope.TaskItem.IsReport = false;
                        } else {
                            $scope.TaskItem.IsReport = false;
                            $scope.TaskItem.IsGroupLabel = false;
                        }
                        $scope.$apply();
                    });
                    $("#TaskItemAssign").select2({
                        width: "100%",
                        templateSelection: function (data, container) {
                            var selection = $('#TaskItemAssign').select2('data');
                            var idx = selection.indexOf(data);

                            if (data.selected == true) {
                                $scope.InitAssignType(idx, container);
                            } else {
                                $scope.InitAssignTypeFromSearch(data.id, container);
                            }
                            $(container).on("click", function (e) {
                                if (e.target.tagName.toLowerCase() === 'span') {
                                    $scope.DeleteAssign(idx, container);
                                } else {
                                    $scope.ChangeAssignType(idx, container);
                                }
                            });
                            return data.text;
                        },
                        dropdownCssClass: "hidden",
                        ajax: {
                            url: CommonUtils.RootURL("Task/TaskItem/SearchTaskAssign"),
                            dataType: 'json',
                            type: 'POST',
                            data: function (params) {
                                var query = {
                                    userName: params.term,
                                    taskItemAssigns: $scope.TaskItem.TaskItemAssigns
                                }
                                return query;
                            },
                            processResults: function (data) {
                                if (data.length > 0) {

                                }
                                var items = [];
                                for (var i = 0; i < data.length; i++) {
                                    var item = {
                                        id: data[i].AssignTo,
                                        text: data[i].AssignToFullName
                                    }
                                    items.push(item);
                                }
                                $scope.TaskItemAssignTemps = data;
                                return {
                                    results: items
                                };
                            },
                        }
                    });
                    
                    $('#TaskCategory').select2({
                        placeholder: 'Chọn loại công việc',
                        tags: true,
                        width: '100%'
                    });
                    $('#TaskFromDateText').datepicker(
                        { format: "dd/mm/yyyy" }).on('changeDate', function (e) {
                            var valid = true;
                            var date = moment($("#TaskFromDateText").val(), 'DD/MM/YYYY');
                            var todate = moment($scope.TaskItem.ToDateText, 'DD/MM/YYYY');
                            if (!$scope.TaskItem.IsParentAuto) {
                                var min = moment($scope.TaskItem.ParentFromDateText, 'DD/MM/YYYY');
                                if (date < min) {
                                    toastr.error('Ngày bắt đầu không hợp lệ', 'Thông báo');
                                    valid = false;
                                    $("#TaskFromDateText").datepicker("destroy");
                                    $("#TaskFromDateText").datepicker("setDate", null);
                                }
                            }
                            if (valid === true && todate < date) {
                                toastr.error('Ngày bắt đầu không hợp lệ', 'Thông báo');
                                valid = false;
                                $("#TaskFromDateText").datepicker("destroy");
                                $("#TaskFromDateText").datepicker("setDate", null);
                            }
                            if (valid === true) {
                                $scope.TaskItem.FromDateText = $("#TaskFromDateText").val();
                            }
                            $('.datepicker').hide();
                        });
                    $('#TaskToDateText').datepicker(
                        { format: "dd/mm/yyyy" }).on('changeDate', function (e) {

                            var valid = true;
                            var date = moment($("#TaskToDateText").val(), 'DD/MM/YYYY');
                            if (!$scope.TaskItem.IsParentAuto) {
                                var max = moment($scope.TaskItem.ParentToDateText, 'DD/MM/YYYY');
                                if (date > max) {
                                    toastr.error('Ngày đến hạn không hợp lệ', 'Thông báo');
                                    valid = false;
                                    $("#TaskToDateText").datepicker("destroy");
                                    $("#TaskToDateText").datepicker("setDate", null);
                                }
                            }
                            var fromdate = moment($scope.TaskItem.FromDateText, 'DD/MM/YYYY');
                            if (valid === true && fromdate > date) {
                                toastr.error('Ngày đến hạn không hợp lệ', 'Thông báo');
                                valid = false;
                                $("#TaskToDateText").datepicker("destroy");
                                $("#TaskToDateText").datepicker("setDate", null);
                            }
                            if (valid === true) {
                                $scope.TaskItem.ToDateText = $("#TaskToDateText").val();
                            }
                            $('.datepicker').hide();
                        });
                    $('#AddEditTaskItem').modal('show');
                    $timeout(function () {
                        $('#TaskName').focus();
                    }, 0);
                    $("#TaskFromDateText").datepicker("setDate", moment($scope.TaskItem.FromDateText, 'DD/MM/YYYY').toDate());
                    $("#TaskToDateText").datepicker("setDate", moment($scope.TaskItem.ToDateText, 'DD/MM/YYYY').toDate());
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                }, function (er) {
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                });
            }
        }
    }
    $scope.SaveTaskItem = function () {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            var validate = true;
            var min = moment($scope.TaskItem.ParentFromDateText, 'DD/MM/YYYY');
            var max = moment($scope.TaskItem.ParentToDateText, 'DD/MM/YYYY');
            var fromDate = moment($scope.TaskItem.FromDateText, 'DD/MM/YYYY');
            var toDate = moment($scope.TaskItem.ToDateText, 'DD/MM/YYYY');
            if ($scope.TaskItem.TaskName == '' || $scope.TaskItem.TaskName == undefined) {
                toastr.error('Vui lòng nhập tên công việc', 'Thông báo');
                $('#TaskName').focus();
                validate = false;
            } else if ($scope.TaskItem.TaskItemAssigns == undefined || $scope.TaskItem.TaskItemAssigns.length <= 0) {
                toastr.error('Vui lòng chọn người tham gia', 'Thông báo');
                $('#TaskItemAssign').focus();
                validate = false;
            } else if ($scope.TaskItem.TaskItemAssigns.filter(e => e.TaskType == '1').length !== 1) {
                toastr.error('Vui lòng chọn 1 người xử lý chính', 'Thông báo');
                $('#TaskItemAssign').focus();
                validate = false;
            }
            else if ($scope.TaskItem.FromDateText == '' || $scope.TaskItem.FromDateText == undefined) {
                toastr.error('Vui lòng nhập từ ngày', 'Thông báo');
                $('#TaskFromDateText').focus();
                $('#TaskFromDateText').focus();
                validate = false;
            } else if ($scope.TaskItem.ToDateText == '' || $scope.TaskItem.ToDateText == undefined) {
                toastr.error('Vui lòng nhập đến ngày', 'Thông báo');
                $('#TaskToDateText').focus();
                $('#TaskToDateText').focus();
                validate = false;
            }
            else
                if (!$scope.TaskItem.IsParentAuto && fromDate < min) {
                    toastr.error('Ngày bắt đầu không hợp lệ', 'Thông báo');
                    validate = false;
                } else
                    if (!$scope.TaskItem.IsParentAuto && toDate > max) {
                        toastr.error('Ngày kết thúc không hợp lệ', 'Thông báo');
                        validate = false;
                    }
            if (validate == false) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                return;
            }
            $scope.TaskItem.TaskItemCategories = $('#TaskCategory').val();
            if ($('#TaskGroupType').val() == '1') {
                $scope.TaskItem.IsReport = true;
                $scope.TaskItem.IsGroupLabel = false;
            } else if ($('#TaskGroupType').val() == '2') {
                $scope.TaskItem.IsGroupLabel = true;
                $scope.TaskItem.IsReport = false;
            } else {
                $scope.TaskItem.IsReport = false;
                $scope.TaskItem.IsGroupLabel = false;
            }
            AdminCategoryService.SaveTaskItem($scope.TaskItem, $scope.fileTemps).then(function (rs) {
                if (rs !== undefined && rs.IsSuccess == true) {
                    toastr.success('Thành công!', 'Thông báo');
                    if ($scope.TaskItem.IsParentAuto == true) {
                        var fromdate = moment($scope.TaskItem.FromDateText, 'DD/MM/YYYY');
                        var todate = moment($scope.TaskItem.ToDateText, 'DD/MM/YYYY');
                        var min = moment($scope.TaskItem.ParentFromDateText, 'DD/MM/YYYY');
                        var max = moment($scope.TaskItem.ParentToDateText, 'DD/MM/YYYY');
                        if (fromdate < min || max < todate) {
                            $scope.GetAdminCategories(null);
                        } else {
                            if ($scope.TaskItem.Id !== '00000000-0000-0000-0000-000000000000') {
                                var parentBranch = $scope.my_tree.get_parent_branch($scope.selectedRow);
                                parentBranch.HasLoading = true;
                                $scope.callbackFunctionInController(parentBranch);
                            }
                            else if ($scope.TaskItem.Id === '00000000-0000-0000-0000-000000000000') {
                                $scope.callbackFunctionInController($scope.selectedRow);
                            }
                        }

                    } else {
                        if ($scope.TaskItem.Id !== '00000000-0000-0000-0000-000000000000') {
                            var parentBranch = $scope.my_tree.get_parent_branch($scope.selectedRow);
                            parentBranch.HasLoading = true;
                            $scope.callbackFunctionInController(parentBranch);
                        }
                        else if ($scope.TaskItem.Id === '00000000-0000-0000-0000-000000000000') {
                            $scope.callbackFunctionInController($scope.selectedRow);
                        }
                    }
                    $scope.CloseTaskItem();
                } else {
                    if (rs !== undefined && rs.Message == "ProcessExpired") {
                        toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
                        $scope.CloseTaskItem();
                        window.location.reload();
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
    };
    $scope.DeleteTaskItem = function () {
        if (!$scope.hasSubmit) {
            $scope.hasSubmit = true;
            if ($scope.selectedRow == null || $scope.selectedRow.Type == 'project') {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                toastr.error('Vui lòng chọn 1 công việc!', 'Thông báo');
                return;
            }
            $scope.filterTask.taskId = $scope.selectedRow.Id;
            var param = {
                Id: $scope.filterTask.taskId
            }
            AdminCategoryService.GetTaskItem(param).then(function (rs) {
                if (rs.data.Message != undefined && rs.data.Message == 'AccessDenied') {
                    toastr.error('Bạn không có quyền xóa công việc này', 'Thông báo');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    return;
                }
                $scope.showConfirm('Bạn có chắc muốn xóa công việc này?', function (rs) {
                    if (rs == true) {
                        $scope.showLoading(null);
                        AdminCategoryService.DeleteTaskItem(param).then(function (rs) {
                            if (rs.data !== undefined && rs.data.IsSuccess == true) {
                                toastr.success('Thành công!', 'Thông báo')
                                $scope.filter.taskId = undefined;
                                var parentBranch = $scope.my_tree.get_parent_branch($scope.selectedRow);
                                $scope.callbackFunctionInController(parentBranch);
                            } else {
                                if (rs.data.Message == "ProcessExpired") {
                                    toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
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
    $scope.CloseTaskItem = function () {
        
        $scope.TaskItem = {
        };
        $('#TaskToDateText').datepicker({ format: "dd/mm/yyyy" }).off('changeDate');
        $('#TaskFromDateText').datepicker({ format: "dd/mm/yyyy" }).off('changeDate');
        $("#TaskFromDateText").datepicker("destroy");
        $("#TaskToDateText").datepicker("destroy");
        $('#collapseTask').collapse("hide");
        $('#TaskItemAssign').select2('destroy');
        $('#TaskCategory').select2('destroy');
        $('#TaskCategory').val(null);
        $("#TaskCategory option").remove();
        $('#AddEditTaskItem').modal('hide');
    }
    $scope.ChangeAssignType = function (idx, container) {

        if ($scope.TaskItem.TaskItemAssigns[idx] !== undefined) {
            if ($scope.TaskItem.TaskItemAssigns[idx].TaskType == "1") {
                $scope.TaskItem.TaskItemAssigns[idx].TaskType = "3";
                $(container).css("background-color", "orange");
            } else if ($scope.TaskItem.TaskItemAssigns[idx].TaskType == "3") {
                $scope.TaskItem.TaskItemAssigns.forEach(function (assign) {
                    assign.TaskType = "3";
                });
                $scope.TaskItem.TaskItemAssigns[idx].TaskType = "1";
                $('li.select2-selection__choice').each(function () {
                    $(this).css("background-color", "orange");
                });
                $(container).css("background-color", "powderblue");
            }
        }
    }
    $scope.DeleteAssign = function (idx, container) {
        $scope.TaskItem.TaskItemAssigns.splice(idx, 1);
    }
    $scope.InitAssignType = function (idx, container) {
        var assign = $scope.TaskItem.TaskItemAssigns[idx];
        if (assign !== undefined) {
            if (assign.TaskType == "1") {
                $(container).css("background-color", "powderblue");
            }
            if (assign.TaskType == "3") {
                $(container).css("background-color", "orange");
            }
        }
    }
    $scope.InitAssignTypeFromSearch = function (id, container) {
        var assign = $scope.TaskItemAssignTemps.filter(e => e.AssignTo == id)[0];
        var count = $scope.TaskItem.TaskItemAssigns.filter(e => e.AssignTo == assign.AssignTo).length;
        if (count == 0) {
            $scope.TaskItem.TaskItemAssigns.push(assign);
            const index = $('#TaskItemAssign').val().indexOf(id);
            if (index > -1) {
                var value = $('#TaskItemAssign').val();
                value.splice(index, 1);
                $('#TaskItemAssign').val(value);
            }
            $scope.$apply();
        }
        if (assign !== undefined) {
            if (assign.TaskType == "1") {
                $(container).css("background-color", "powderblue");
            }
            if (assign.TaskType == "3") {
                $(container).css("background-color", "orange");
            }
        }
    }
    $scope.AddAssignBy = function (datas) {
        if (datas.length > 0) {
            var users = datas.filter(e => e.type == 'user');
            if (users.length === 1) {
                for (var i = 0; i < users.length; i++) {
                    $scope.TaskItem.AssignBy = users[i].id;
                    $scope.TaskItem.AssignByFullName = users[i].text;
                }
            } else {
                toastr.error('Vui lòng chỉ chọn một', 'Thông báo');
            }
        }
        $scope.$apply();
    }
    $scope.AddTaskMember = function (datas) {
        if (datas.length > 0) {
            var users = datas.filter(e => e.type == 'user');
            if (users.length > 0) {
                for (var i = 0; i < users.length; i++) {
                    var check = $scope.TaskItem.TaskItemAssigns.filter(e => e.AssignTo == users[i].id).length > 0;
                    if (check == false) {
                        var assignee = {
                            AssignToFullName: users[i].text,
                            AssignToJobTitleName: users[i].jobTitle,
                            Department: users[i].department,
                            AssignTo: users[i].id,
                            TaskType: "3"
                        }
                        $scope.TaskItem.TaskItemAssigns.push(assignee);
                    }
                }
            }
        }
        $scope.$apply();
    }
    $scope.DeleteFileTask = function (index, Id) {
        $scope.showConfirm('Bạn có chắc muốn xóa?', function (rs) {
            if (rs) {
                if ($scope.TaskItem.AttachDelIds == undefined) $scope.TaskItem.AttachDelIds = [];
                $scope.TaskItem.AttachDelIds.push(Id);
                $scope.TaskItem.Attachments.splice(index, 1);
            }
        });
    }
});
