app.directive('kanbanBoardDragg', function () {
    return {
        link: function ($scope, element, attrs) {

            var dragData = "";
            $scope.$watch(attrs.kanbanBoardDragg, function (newValue) {
                dragData = newValue;
            });

            element.bind('dragstart', function (event) {
                event.originalEvent.dataTransfer.setData("Text", JSON.stringify(dragData));
            });
        }
    };
});
app.directive('kanbanBoardDrop', function () {
    return {
        link: function ($scope, element, attrs) {
            var dragOverClass = attrs.kanbanBoardDrop;
            cancel = function (event) {
                if (event.preventDefault) {
                    event.preventDefault();
                }

                if (event.stopPropigation) {
                    event.stopPropigation();
                }
                return false;
            };
            element.bind('dragover', function (event) {
                cancel(event);
                event.originalEvent.dataTransfer.dropEffect = 'move';
                element.addClass(dragOverClass);
            });
            element.bind('drop', function (event) {
                cancel(event);
                element.removeClass(dragOverClass);
                var droppedData = JSON.parse(event.originalEvent.dataTransfer.getData('Text'));
                $scope.onDropKanban(droppedData, element.attr('id'));

            });
            element.bind('dragleave', function (event) {
                element.removeClass(dragOverClass);
            });
        }
    };
});
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
            //el.draggable = true;

            //el.addEventListener(
            //    'dragstart',
            //    function (e) {
            //        e.dataTransfer.effectAllowed = 'move';
            //        e.dataTransfer.setData('Text', this.id);
            //        this.classList.add('drag');
            //        return false;
            //    },
            //    false
            //);

            //el.addEventListener(
            //    'dragend',
            //    function (e) {
            //        this.classList.remove('drag');
            //        return false;
            //    },
            //    false
            //);

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
app.directive('myEnterTask', function () {
    return function (scope, element, attrs) {
        element.bind("keydown", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.taskItem.createComment();
                });

                event.preventDefault();
            }
        });
    };
});
app.controller("MainCtrl", function ($scope, $controller, $q, $timeout, fileFactory, MainService) {
    $controller('BaseCtrl', { $scope: $scope });
    $controller('CalendarCtrl', { $scope: $scope });
    $controller('ProjectDetailCtrl', { $scope: $scope });
    $controller('TaskItemDetailCtrl', { $scope: $scope });
    $controller('LeftMenuCtrl', { $scope: $scope });
    
    $scope.ProjectTaskItem = [];
    $scope.projectFilters = [{
        Name: 'Tất cả',
        Id: ''
    }];
    $scope.CreateComment = function () {
        debugger
    }
    $scope.ViewBreadCrumb = [];
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const filterId = urlParams.get('filterId');
    const folderId = urlParams.get('folderId');
    var view = urlParams.get('view');
    //let parentId = '00000000-0000-0000-0000-000000000000';
    let parentId = '';
    $scope.ShowType = 0; //Kiem Tra View Dang Nao
    $scope.isEditProject = false;
    $scope.isAddTask = false;
    $scope.isEditTask = false;
    $scope.isProcessTask = false;
    $scope.isAppraiseTask = false;
    $scope.isAppraiseExtendTask = false;
    $scope.isProcessProject = false;
    $scope.isImport = false;
    $scope.IsEvict = false;
    $scope.advanceFilter = { keyWord: "", CurrentPage: 1, PageSize: 20 };
    $scope.selectedRow = null;
    $scope.projectFilterId = undefined;

    $scope.init = function () {
        

        //$scope.SetShowType(view);
        if (view === "calendar") {
            $scope.ShowType = 3;
        }
        else if (view === "kanban") {
            $scope.ShowType = 2;
        }
        else {
            $scope.tree_data = []
            $scope.my_tree = {};
            $scope.expanding_property = "Name";
            $scope.ShowType = 0;
        }
        $scope.col_defs = ColumnHeader;
        $scope.$example = $('#projectFilters').select2({
            placeholder: "Tất cả",
            width: '100%'
        });
        $('#projectFilters').on('select2:select', function (e) {
            ;
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
            if ($scope.ShowType === 2) {
                if ($('#projectFilters').val() !== "undefined") {
                    $scope.getDataByProject($('#projectFilters').val());
                } else {
                    $scope.getDataByProject(null);
                }
            }
            if ($scope.ShowType === 3) {
                if ($('#projectFilters').val() !== "undefined") {
                    let item = $scope.projectFilters.filter(e => e.ProjectId == $('#projectFilters').val())[0];
                    $scope.calendar.filter.fromDate = moment(item.FromDateText, 'DD/MM/YYYY').startOf('week').format('DD/MM/YYYY');
                }
                $scope.calendar.init();
            }
            //$scope.my_tree.add_branch(branch, myTreeData[i]);
        });
        $scope.getDataByProject(parentId);
    };
    $scope.SetShowType = function (view) {
        if (view === "calendar") {
            $scope.ShowType = 3;
        }
        else if (view === "kanban") {
            $scope.ShowType = 2;
        }
        else {
            $scope.ShowType = 0; //table, grantt
        }
    };
    $scope.ChangeView = function (viewType) {

        window.location.href = CommonUtils.RootURL("Task/Home/Index?filterId=" + filterId + "&folderId=" + folderId + "&view=" + viewType);
        //window.location.href =  CommonUtils.RootURL(result.url + "&folderGroupId=" + folderGroupId + " &keyword= " + keyword + "&pageIndex=" + pageIndex);
        //if ($('#projectFilters').val() !== "undefined") {
        //    window.location.href = CommonUtils.RootURL("Task/Home/Index?parentId=" + $('#projectFilters').val() + "&filterId=" + filterId + "&folderId=" + folderId + "&view=" + viewType);
        //} else {
           
        //}
    };
    $scope.callbackDblFunctionInController = function (branch) {
        if (!$("#kt_demo_panel").hasClass('offcanvas-on')) {
            $("#kt_demo_panel_toggle").click();
        }

        if (branch.Type == "project") {
            $scope.projectDetail.init(branch.Id);
            //if (!$('#modal-project-detail').is(':visible')) {
            //    $("#kt_demo_panel_toggle").click();
            //}
            $("#tabli_projectStatus").children().addClass("active");
            $("#tabli_attachmentZone").children().removeClass("active");
            $("#tabli_projectTaskRoot").children().removeClass("active");
            $("#tabli_projectReport").children().removeClass("active");
        }
        else {
            $scope.taskItem.init(branch.Id);
        }
    };

    $scope.CloseProjectDetail = function () {
        $('#modal-project-detail').hide();
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
                    let promises = [MainService.moveDataByTask(data)];
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
                                $scope.getDataByProject(branchChildrenD.Id, branchChildrenD);
                                //branch.children.push({ Id: 1, ParentId: 2, StatusName: "Test1", FromDate: "ddd", ToDate: "ddd", FullName: "test2", Type: "Te", HasChildren: true })
                                toastr.success(rs[0].data.msg);
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
            $scope.getDataByProject(branch.ParentId, branch);
            
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
            $scope.getDataByProject(branch.Id, branch);
        }
        else {
            $scope.getDataByProject(branch.Id, branch);
        }
    };
    $scope.getDataByProject = function (parentId, branch) {


        //$scope.showLoading(null);
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
        if ($scope.ShowType == 2) {
            promises = [MainService.GetDataByProject(data), MainService.GetDataByProject(JSON.stringify({
                parentId: parentId,
                filterId: filterId,
                folderId: folderId,
                view: '',
                filter: $scope.advanceFilter
            }))];
        } else {
            promises = [MainService.GetDataByProject(data)];
        }
        $q.all(promises).then(function (rs) {
            
            if (rs[0].data.status) {
                if (parentId == null || parentId == undefined || parentId == '') {
                    if ($scope.ShowType === 2) {
                        $scope.projectFilters = [{
                            Name: 'Tất cả',
                            ProjectId: undefined
                        }];
                        $scope.projectFilters = $scope.projectFilters.concat(rs[1].data.data.Result);
                    } else {
                        $scope.projectFilters = [{
                            Name: 'Tất cả',
                            ProjectId: undefined
                        }];
                        $scope.projectFilters = $scope.projectFilters.concat(rs[0].data.data.Result);
                    }
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
                            if ($scope.ShowType === 2) {

                            } else {
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
                        }
                        else if (!branch.HasChildren) {
                            branch.HasLoading = false;
                            branch.HasChildren = true;
                            for (var i = 0; i < myTreeData.length; i++) {
                                $scope.my_tree.add_branch(branch, myTreeData[i]);
                                //branch.children.push(myTreeData[i]);

                            }
                        }
                        else {

                        }
                        if ($scope.ShowType === 2) {

                        }
                        else {
                            $scope.my_tree.select_branch($scope.my_tree.get_branch($scope.selectedRow))
                        }
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
                else if ($scope.ShowType === 2) {
                    
                    if (branch != undefined && branch.HasPagination) {
                        var valueHasPagination = $scope.ProjectTaskItem.slice(-1)[0];
                        if (valueHasPagination !== undefined && valueHasPagination.HasPagination) {
                            $scope.ProjectTaskItem.pop();
                        }
                        $scope.bindingDataKanban(rs[0].data.data.Result, rs[0].data.data.Result);
                    }
                    else {
                        $scope.getViewBreadCrumbWithParent(parentId);
                        $scope.bindingDataKanban(rs[0].data.data.Result);
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
    $scope.bindingDataKanban = function (data, dataPagging) {
        if (dataPagging !== undefined) {
            $scope.ProjectTaskItem = $scope.ProjectTaskItem.concat(dataPagging);
        }
        else {
            $scope.ProjectTaskItem = data;
        }
    }
    $scope.dblGetDataKanbanByItem = function (data) {
        $scope.selectedRow = data;
        if (data.Type != "task" || data.Type == "tasks" || (data.Type == "task" && data.HasChildren)) {
            $scope.selectedRow = null;
            $scope.getDataByProject(data.Id);
        }
    }
    $scope.selectGetDataKanbanByItem = function (branch) {
        $scope.onSelectItem(branch);
    }
    $scope.selectViewDetailKanbanByItem = function (data) {
        $scope.callbackDblFunctionInController(data);
    }
    $scope.selectViewBreadCrumbWithParent = function (id) {
        $scope.selectedRow = null;
        $scope.getDataByProject(id);
    }
    $scope.getViewBreadCrumbWithParent = function (parentId) {
        var data = JSON.stringify({
            parentId: parentId
        });
        let promises = [MainService.GetViewBreadCrumbWithParent(data)];
        $q.all(promises).then(function (rs) {
            if (rs[0].data.status) {
                $scope.ViewBreadCrumb = rs[0].data.data;

            }
            else {
                toastr.error(rs[0].data.data.msg, 'Thông báo')
            }
        }, function (er) {
            toastr.error(er, 'Thông báo')
            $scope.hideLoading();
        });
    };
    $scope.selectViewMoreKanbanByItem = function (data) {
        $scope.callbackFunctionInController(data);
    };
    $scope.onSelectItem = function (branch) {
        if (branch !== null && branch !== undefined) {
            $scope.selectedRow = branch;
            if ($scope.selectedRow.Type == 'project') {
                MainService.CheckActionsProject({ projectId: $scope.selectedRow.Id }).then(function (rs) {
                    $scope.isEditProject = false;
                    $scope.isAddTask = false;
                    $scope.isEditTask = false;
                    $scope.isDeleteTask = false;
                    $scope.isDeleteProject = false;
                    $scope.isProcessTask = false;
                    $scope.isAppraiseTask = false;
                    $scope.isAppraiseExtendTask = false;
                    $scope.isProcessProject = false;
                    $scope.isImport = false;
                    if (rs.data != undefined && rs.data.length > 0) {
                        if (rs.data.filter(e => e.Code == 'Assign').length > 0) {
                            $scope.isAddTask = true;
                        }
                        if (rs.data.filter(e => e.Code == 'EditDoc').length > 0) {
                            $scope.isEditProject = true;
                        }
                        if (rs.data.filter(e => e.Code == 'DeleteDoc').length > 0) {
                            $scope.isDeleteProject = true;
                        }
                        if (rs.data.filter(e => e.Code == 'ProcessProject').length > 0) {
                            $scope.isProcessProject = true;
                        }
                        if (rs.data.filter(e => e.Code == 'ImportDoc').length > 0) {
                            $scope.isImport = true;
                        }
                    }
                }, function (er) {
                });
            } else {
                MainService.CheckActionsTask({ projectId: $scope.selectedRow.ProjectId, taskId: $scope.selectedRow.Id }).then(function (rs) {
                    $scope.isEditProject = false;
                    $scope.isAddTask = false;
                    $scope.isEditTask = false;
                    $scope.isDeleteTask = false;
                    $scope.isDeleteProject = false;
                    $scope.isProcessTask = false;
                    $scope.isAppraiseTask = false;
                    $scope.isAppraiseExtendTask = false;
                    $scope.isProcessProject = false;
                    $scope.isImport = false;
                    if (rs.data != undefined && rs.data.length > 0) {
                        if (rs.data.filter(e => e.Code == 'Assign').length > 0) {
                            $scope.isAddTask = true;
                        }
                        if (rs.data.filter(e => e.Code == 'EditDoc').length > 0) {
                            $scope.isEditTask = true;
                        }
                        if (rs.data.filter(e => e.Code == 'DeleteDoc').length > 0) {
                            $scope.isDeleteTask = true;
                        }
                        if (rs.data.filter(e => e.Code == 'Process').length > 0) {
                            $scope.isProcessTask = true;
                        }
                        if (rs.data.filter(e => e.Code == 'Appraise' || e.Code == 'ApproveReturn').length > 0) {
                            $scope.isAppraiseTask = true;
                        }
                        if (rs.data.filter(e => e.Code == 'ImportDoc').length > 0) {
                            $scope.isImport = true;
                        }
                        if (rs.data.filter(e => e.Code == 'AppraiseExtend').length > 0) {
                            $scope.isAppraiseExtendTask = true;
                        }
                    }
                }, function (er) {
                })
            }
        } else {
            $scope.isEditProject = false;
            $scope.isAddTask = false;
            $scope.isEditTask = false;
            $scope.isDeleteTask = false;
            $scope.isDeleteProject = false;
            $scope.isProcessTask = false;
            $scope.isAppraiseTask = false;
            $scope.isAppraiseExtendTask = false;
            $scope.isProcessProject = false;
        }

        //////alert("my_tree_handler");
        ////console.log('you clicked on', branch)

    }

    //$scope.get_parentDataByChildren = function (data,child) {
    //    var parent;
    //    parent = void 0;
    //    if (child.parent_uid) {
    //        data.forEach(function (b) {
    //            if (b.Id === child.parent_uid) {
    //                return parent = b;
    //            }
    //        });
    //    }
    //    return parent;
    //};
    $scope.onDropKanban = function (data, targetColId) {
        $scope.selectGetDataKanbanByItem(data);
        if (data.StatusId != targetColId) {
            if (data.Type == 'project') {
                $scope.ViewProcessProject();
            } else {
                $scope.ViewTaskItemAssign("Process");
            }
        }
    };
    $scope.ExportExcel = function () {

        if ($scope.selectedRow == null) {
            $scope.hasSubmit = false;
            toastr.error('Vui lòng chọn 1 dự án/ công việc!', 'Thông báo');
            return;
        }
        $scope.filter.projectId = $scope.selectedRow.ProjectId;
        var param = {
            projectId: $scope.filter.projectId
        }
        if ($scope.selectedRow.Type !== 'project') {
            param.taskId = $scope.selectedRow.Id
            window.open(CommonUtils.RootURL("Task/Project/ExportExcel") + "?projectId=" + param.projectId + "taskId=" + param.taskId);

        } else {
            window.open(CommonUtils.RootURL("Task/Project/ExportExcel") + "?projectId=" + param.projectId);
        }
        //MainService.ExportExcel(param).then(function (response) {
        //    if (response.data.Success) {
        //        toastr.success(er, 'Thông báo ok');
        //    }
        //    else {
        //        toastr.success(er, 'Thông báo Error');
        //    }
        //    $scope.hideLoading();
        //}, function errorCallback(error) {
        //    $scope.hideLoading();
        //    $scope.handleError(error)
        //});

    };
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
    $scope.filter = {};
    $scope.fileTemps = [];
    $scope.hasSubmit = false;
    $scope.ViewProject = function (action) {
        $scope.viewProjectAction = action;
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            $scope.Project = {
                ProjectMembers: [],
                ProjectCategories: [],
            };
            $scope.fileTemps = [];
            var promises = [];
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
                promises = [MainService.GetProject(param), MainService.GetProjectTypes(), MainService.GetProjectPriorities(), MainService.GetProjectCategories({ projectId: $scope.filter.projectId }), MainService.GetAdminCategories()];
                $q.all(promises).then(function (rs) {
                    if (rs[0].data.Message != undefined && rs[0].data.Message == 'AccessDenied') {
                        toastr.error('Bạn không có quyền chỉnh sửa dự án này', 'Thông báo');
                        $scope.hideLoading();
                        $scope.hasSubmit = false;
                        return;
                    }
                    $scope.Project = rs[0].data;
                    $scope.ProjectTypes = rs[1].data;
                    $scope.ProjectPriorities = rs[2].data;
                    $scope.ProjectCategories = rs[3].data;
                    $scope.AdminCategories = [{
                        Id: guidEmpty,
                        Summary: '-- Chọn phân loại --'
                    }];
                    $scope.AdminCategories = $scope.AdminCategories.concat(rs[4].data);
                    $scope.Project.CreatedDateText = moment($scope.Project.CreatedDate).format("DD/MM/YYYY");
                    $('.select2css').select2({
                        placeholder: "Tất cả",
                        width: '100%'
                    });
                    $('#ProjectCategory').select2({
                        placeholder: 'Chọn loại dự án',
                        tags: true,
                        width: '100%',
                        data: $scope.ProjectCategories
                    });
                    $("#ProjectCategory").val($scope.Project.ProjectCategories).trigger('change');
                    $("#ProjectMember").select2({
                        width: "100%",
                        templateSelection: function (data, container) {
                            var selection = $('#ProjectMember').select2('data');
                            var idx = selection.indexOf(data);

                            if (data.selected == true) {
                                $scope.InitProjectMemberType(idx, container);
                            } else {
                                $scope.InitProjectMemberTypeFromSearch(data.id, container);
                            }
                            $(container).on("click", function (e) {

                                if (e.target.tagName.toLowerCase() === 'span') {
                                    $scope.DeleteProjectMember(idx, container);
                                } else {
                                    $scope.ChangeProjectMemberType(idx, container);
                                }
                            });
                            return data.text;
                        },
                        dropdownCssClass: "hidden",
                        ajax: {
                            url: CommonUtils.RootURL("Task/Project/SearchProjectMember"),
                            dataType: 'json',
                            type: 'POST',
                            data: function (params) {
                                var query = {
                                    userName: params.term,
                                    projectMembers: $scope.Project.ProjectMembers
                                }
                                return query;
                            },
                            processResults: function (data) {
                                if (data.length > 0) {

                                }
                                var items = [];
                                for (var i = 0; i < data.length; i++) {
                                    var item = {
                                        id: data[i].UserId,
                                        text: data[i].FullName
                                    }
                                    items.push(item);
                                }
                                $scope.ProjectMemberTemps = data;
                                return {
                                    results: items
                                };
                            },
                        }
                    });
                    $("#FromDateText").val($scope.Project.FromDateText);
                    $('#FromDateText').datepicker(
                        { format: "dd/mm/yyyy" }).on('changeDate', function (e) {
                            var valid = true;
                            var date = moment($("#FromDateText").val(), 'DD/MM/YYYY');
                            var todate = moment($scope.Project.ToDateText, 'DD/MM/YYYY');
                            var fromdate = moment($scope.Project.FromDateText, 'DD/MM/YYYY');
                            if ($scope.Project.Id !== undefined) {
                                if ($scope.Project.MinFromDateText != '') {
                                    var min = moment($scope.Project.MinFromDateText, 'DD/MM/YYYY');
                                    if (min < date) {
                                        toastr.error('Có công việc có ngày bắt đầu nhỏ hơn', 'Thông báo');
                                        valid = false;
                                        $("#FromDateText").datepicker("destroy");
                                        $("#FromDateText").datepicker("setDate", fromdate.toDate());
                                    }
                                } else {
                                    $scope.Project.IsAuto = false;
                                }
                            }
                            if (valid && todate < date) {
                                $("#ToDateText").datepicker("destroy");
                                $("#ToDateText").datepicker("setDate", null);
                                $scope.Project.ToDateText = null;
                            }
                            if (valid) {
                                $scope.Project.FromDateText = $("#FromDateText").val();
                            }
                            $('.datepicker').hide();
                        });
                    $("#ToDateText").val($scope.Project.ToDateText);
                    $('#ToDateText').datepicker(
                        { format: "dd/mm/yyyy" }).on('changeDate', function (e) {
                            var valid = true;
                            var date = moment($("#ToDateText").val(), 'DD/MM/YYYY');
                            var todate = moment($scope.Project.ToDateText, 'DD/MM/YYYY');
                            if ($scope.Project.Id !== undefined) {
                                if ($scope.Project.MaxToDateText != '') {
                                    var max = moment($scope.Project.MaxToDateText, 'DD/MM/YYYY');
                                    if (max > date) {
                                        toastr.error('Có công việc có ngày kết thúc lớn hơn', 'Thông báo');
                                        valid = false;
                                        $("#ToDateText").datepicker("destroy");
                                        $("#ToDateText").datepicker("setDate", todate.toDate());
                                    }
                                } else {
                                    $scope.Project.IsAuto = false;
                                }
                            }
                            var fromdate = moment($scope.Project.FromDateText, 'DD/MM/YYYY');
                            if (valid && fromdate > date) {
                                $("#FromDateText").datepicker("destroy");
                                $("#FromDateText").datepicker("setDate", null);
                                $scope.Project.FromDateText = null;
                            }
                            if (valid) {
                                $scope.Project.ToDateText = $("#ToDateText").val();
                            }
                            $('.datepicker').hide();
                        });
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
                promises = [MainService.GetProjectTypes(), MainService.GetProjectPriorities(), MainService.GetProject({ id: null }), MainService.GetAdminCategories()];
                $q.all(promises).then(function (rs) {
                    $scope.ProjectTypes = rs[0].data;
                    $scope.ProjectPriorities = rs[1].data;
                    $scope.Project = rs[2].data;
                    $scope.AdminCategories = [{
                        Id: guidEmpty,
                        Summary: '-- Chọn phân loại --'
                    }];
                    $scope.AdminCategories = $scope.AdminCategories.concat(rs[3].data);
                    $scope.Project.AdminCategoryId = guidEmpty;
                    $scope.ProjectCategories = [];
                    if ($scope.ProjectTypes != undefined && $scope.ProjectTypes.length > 0) {
                        $scope.Project.ProjectTypeId = $scope.ProjectTypes[0].Id;
                    }
                    if ($scope.ProjectPriorities != undefined && $scope.ProjectPriorities.length > 0) {
                        $scope.Project.ProjectPriorityId = $scope.ProjectPriorities[0].Id;
                    }

                    $('.select2css').select2({
                        placeholder: "Tất cả",
                        width: '100%',
                    });
                    $('#ProjectCategory').select2({
                        placeholder: 'Chọn loại dự án',
                        tags: true,
                        width: '100%',
                        data: $scope.ProjectCategories
                    });
                    $("#ProjectMember").select2({
                        width: "100%",
                        templateSelection: function (data, container) {
                            var selection = $('#ProjectMember').select2('data');
                            var idx = selection.indexOf(data);

                            if (data.selected == true) {
                                $scope.InitProjectMemberType(idx, container);
                            } else {
                                $scope.InitProjectMemberTypeFromSearch(data.id, container);
                            }
                            $(container).on("click", function (e) {

                                if (e.target.tagName.toLowerCase() === 'span') {
                                    $scope.DeleteProjectMember(idx, container);
                                } else {
                                    $scope.ChangeProjectMemberType(idx, container);
                                }
                            });
                            return data.text;
                        },
                        dropdownCssClass: "hidden",
                        ajax: {
                            url: CommonUtils.RootURL("Task/Project/SearchProjectMember"),
                            dataType: 'json',
                            type: 'POST',
                            data: function (params) {
                                var query = {
                                    userName: params.term,
                                    projectMembers: $scope.Project.ProjectMembers
                                }
                                return query;
                            },
                            processResults: function (data) {
                                var items = [];
                                for (var i = 0; i < data.length; i++) {
                                    var item = {
                                        id: data[i].UserId,
                                        text: data[i].FullName
                                    }
                                    items.push(item);
                                }
                                $scope.ProjectMemberTemps = data;
                                return {
                                    results: items
                                };
                            },
                        }
                    });
                    $('#ToDateText').datepicker({
                        format: "dd/mm/yyyy"
                    }).on('changeDate', function (e) {
                        var valid = true;
                        var date = moment($("#ToDateText").val(), 'DD/MM/YYYY');
                        var fromdate = moment($scope.Project.FromDateText, 'DD/MM/YYYY');
                        if (fromdate > date) {
                            $("#FromDateText").datepicker("destroy");
                            $("#FromDateText").datepicker("setDate", null);
                            $scope.Project.FromDateText = null;
                        }
                        if (valid) {
                            $scope.Project.ToDateText = $("#ToDateText").val();
                        }
                        $('.datepicker').hide();
                    });
                    $('#FromDateText').datepicker({
                        format: "dd/mm/yyyy"
                    }).on('changeDate', function (e) {
                        var valid = true;
                        var date = moment($("#FromDateText").val(), 'DD/MM/YYYY');
                        var todate = moment($scope.Project.ToDateText, 'DD/MM/YYYY');
                        if (todate < date) {
                            $("#ToDateText").datepicker("destroy");
                            $("#ToDateText").datepicker("setDate", null);
                            $scope.Project.ToDateText = null;
                        }
                        if (valid) {
                            $scope.Project.FromDateText = $("#FromDateText").val();
                        }
                        $('.datepicker').hide();
                    });
                    $("#FromDateText").datepicker("setDate", new Date());
                    $("#ToDateText").datepicker("setDate", new Date());

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
    $scope.SaveProject = function () {

        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;

            var validate = true;
            if ($scope.Project.Summary == '' || $scope.Project.Summary == undefined) {
                toastr.error('Vui lòng nhập tên dự án', 'Thông báo');
                $('#ProjectSummary').focus();
                validate = false;
            } else
                if ($scope.Project.FromDateText == '' || $scope.Project.FromDateText == undefined) {
                    toastr.error('Vui lòng nhập từ ngày', 'Thông báo');
                    $('#FromDateText').focus();
                    $('#FromDateText').focus();
                    validate = false;
                } else
                    if ($scope.Project.ToDateText == '' || $scope.Project.ToDateText == undefined) {
                        toastr.error('Vui lòng nhập đến ngày', 'Thông báo');
                        $('#ToDateText').focus();
                        $('#ToDateText').focus();
                        validate = false;
                    } else
                        
                        if ($scope.Project.ProjectPriorityId === '' || $scope.Project.ProjectPriorityId === undefined) {
                            toastr.error('Vui lòng nhập mức đô quan trọng', 'Thông báo');
                            $('#ProjectPriorityId').focus();
                            validate = false;
                        } else
                            if ($scope.Project.ProjectMembers == undefined || $scope.Project.ProjectMembers.length <= 0) {
                                toastr.error('Vui lòng chọn thành viên', 'Thông báo');
                                validate = false;
                            } else
                                if ($scope.Project.ProjectMembers.filter(e => e.Role == "1").length <= 0) {
                                    toastr.error('Vui lòng chọn người quản lý', 'Thông báo');
                                    validate = false;
                                }
            if (validate == false) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                return;
            }

            $scope.Project.ProjectCategories = $('#ProjectCategory').val();

            MainService.SaveProject($scope.Project, $scope.fileTemps).then(function (rs) {

                if (rs !== undefined && rs.IsSuccess == true) {
                    toastr.success('Thành công!', 'Thông báo')
                    $scope.CloseProject();
                    $scope.getDataByProject(null);
                } else {
                    if (rs !== undefined && rs.Message == "ProcessExpired") {
                        toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
                        $scope.CloseProject();
                        $scope.getDataByProject(null);
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
                                $scope.getDataByProject(null);
                            } else {
                                if (rs.data.Message == "ProcessExpired") {
                                    toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
                                    $scope.getDataByProject(null);
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
    $scope.CloseProject = function () {
        
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
    $scope.DeleteProjectMember = function (idx, container) {
        $scope.Project.ProjectMembers.splice(idx, 1);
        //$scope.showConfirm('Bạn có chắc muốn xóa người dùng này!', function (rs) {
        //    if (rs) {
        //        $scope.Project.ProjectMembers.splice(idx, 1);
        //        toastr.success('Thành công', 'Thông báo');
        //        $scope.$apply();
        //    }
        //});
    }
    $scope.AddProjectMember = function (datas) {
        if (datas.length > 0) {
            var users = datas.filter(e => e.type == 'user');
            if (users.length > 0) {
                for (var i = 0; i < users.length; i++) {
                    var check = $scope.Project.ProjectMembers.filter(e => e.UserName == users[i].code).length > 0;
                    if (check == false) {
                        var projectMember = {
                            UserName: users[i].code,
                            FullName: users[i].text,
                            JobTitle: users[i].jobTitle,
                            Department: users[i].department,
                            UserId: users[i].id,
                            Role: "2"
                        }

                        $scope.Project.ProjectMembers.push(projectMember);
                    }
                }
                $scope.Project.ProjectMembers.sort((a, b) => (a.FullName > b.FullName) ? 1 : ((b.FullName > a.FullName) ? -1 : 0));
            }
        }
        $scope.$apply();
    }
    $scope.ScheduleProject = function () {
        if ($scope.Project.Id !== undefined) {
            if ($scope.Project.MinFromDateText !== '' && $scope.Project.MinFromDateText !== null) {
                $scope.Project.FromDateText = $scope.Project.MinFromDateText;
            }
            if ($scope.Project.MaxToDateText !== '' && $scope.Project.MaxToDateText !== null) {
                $scope.Project.ToDateText = $scope.Project.MaxToDateText;
            }
        }
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
                    if (!isFileInvalid(files[i].size, fileType, files[i].name)) {
                        return;
                    }
                }
                $scope.fileTemps = $scope.fileTemps.concat(files);

            } else {
                Notify({ title: "Thông báo", message: "Có lỗi", type: "error", delay: 2000 });
            }
        }
    }
    function isFileInvalid(fileSize, fileType, fileName) {
        
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
        if (fileName !== undefined) {
            if ($scope.fileTemps !== null && $scope.fileTemps.filter(e => e.name === fileName).length > 0) {
                toastr.error('Tập tin ' + fileName + 'đã tồn tại!', 'Thông báo');
                error = false;
            } else
                if ($scope.TaskItem !== undefined && $scope.TaskItem !== null && $scope.TaskItem.Attachments !== null && $scope.TaskItem.Attachments !== undefined && $scope.TaskItem.Attachments.filter(e => e.FileName === fileName).length > 0) {
                    toastr.error('Tập tin ' + fileName + 'đã tồn tại!', 'Thông báo');
                    error = false;
                } else
                    if ($scope.Project !== undefined && $scope.Project !== null && $scope.Project.Attachments !== null && $scope.Project.Attachments !== undefined && $scope.Project.Attachments.filter(e => e.FileName === fileName).length > 0) {
                        toastr.error('Tập tin ' + fileName + 'đã tồn tại!', 'Thông báo');
                        error = false;
                    }
        }
        
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
    $scope.DeleteFile = function (index, Id) {
        $scope.showConfirm('Bạn có chắc muốn xóa?', function (rs) {
            if (rs) {
                if ($scope.Project.AttachDelIds == undefined) $scope.Project.AttachDelIds = [];
                $scope.Project.AttachDelIds.push(Id);
                $scope.Project.Attachments.splice(index, 1);
            }
        });
    }
    $scope.ViewProcessProject = function () {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            $scope.Project = {
            };
            if ($scope.selectedRow == null || $scope.selectedRow.Type !== 'project') {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                toastr.error('Vui lòng chọn 1 dự án!', 'Thông báo');
                return;
            }
            $scope.filter.projectId = $scope.selectedRow.ProjectId;
            $scope.modalTitle = "Cập nhật tình trạng"
            var param = {
                id: $scope.filter.projectId
            }
            var promises = [MainService.GetProject(param), MainService.GetProjectStatuses(), MainService.GetTaskItemAssignConfigPoint()];
            $q.all(promises).then(function (rs) {
                if (rs[0].data.Message != undefined && rs[0].data.Message == 'AccessDenied') {
                    toastr.error('Bạn không có quyền chỉnh sửa dự án này', 'Thông báo');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    return;
                }
                $scope.Project = rs[0].data;
                $scope.ProjectStatuses = rs[1].data;
                $('.select2css').select2({
                    placeholder: "Chọn tình trạng",
                    width: '100%',
                });
                $("#FinishDateText").val($scope.Project.FinishDateText);
                $('#FinishDateText').datepicker(
                    { format: "dd/mm/yyyy" }).on('changeDate', function (e) {
                        var valid = true;
                        var date = moment($("#FinishDateText").val(), 'DD/MM/YYYY');
                        var fromdate = moment($scope.Project.FromDateText, 'DD/MM/YYYY');
                        var finishdate = moment($scope.Project.FinishDateText, 'DD/MM/YYYY');
                        if (fromdate > date) {
                            toastr.error('Ngày kết thúc không hợp lệ', 'Thông báo');
                            valid = false;
                            $("#FinishDateText").datepicker("destroy");
                            $("#FinishDateText").datepicker("setDate", finishdate.toDate());
                        }
                        if (valid) {
                            $scope.Project.FinishDateText = $("#FinishDateText").val();
                        }
                        $('.datepicker').hide();
                    });
                $scope.ConfigValue = [];
                $scope.ConfigType = rs[2].data.filter(e => e.Code == 'Task.TypeBarProcesTask')[0].Value;
                if ($scope.ConfigType == 'combobox') {
                    $scope.ConfigPoints = rs[2].data.filter(e => e.Code == 'Task.DataSourceProcesTaskCombobox')[0].Value.split(";");
                    $scope.ConfigPoints.forEach(function (config) {
                        config = {
                            text: config.split(":")[0],
                            value: config.split(":")[1],
                        }
                        if (config.value == $scope.Project.PercentFinish) {
                            $scope.Project.PercentFinishText = config.text;
                        }
                        $scope.ConfigValue.push(config);
                    });
                }
                $('#ProcessProject').modal('show');
                $scope.hideLoading();
                $scope.hasSubmit = false;
            }, function (er) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
            });
        }
    }
    $scope.UpdateStatusProject = function () {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            var validate = true;
            if ($scope.Project.AppraiseResult == '' || $scope.Project.AppraiseResult == undefined) {
                toastr.error('Vui lòng nhập nội dung', 'Thông báo');
                $('#ProjectAppraiseResult').focus();
                validate = false;
            }
            if (validate == false) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                return;
            }
            MainService.UpdateStatusProject($scope.Project, $scope.fileTemps).then(function (rs) {
                if (rs !== undefined && rs.IsSuccess == true) {
                    toastr.success('Thành công!', 'Thông báo')
                    $scope.CloseProcessProject();
                    $scope.getDataByProject(null);
                } else {
                    if (rs !== undefined && rs.Message == "ProcessExpired") {
                        toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
                        $scope.CloseProcessProject();
                        $scope.getDataByProject(null);
                    } else {
                        toastr.error('Có lỗi trong quá trình xử lý!', 'Thông báo')
                    }
                }
                $scope.hideLoading();
                $scope.hasSubmit = false;
                $scope.hideLoading();
                $scope.hasSubmit = false;
            }, function (er) {
                toastr.error('Có lỗi trong quá trình xử lý!', 'Thông báo')
                $scope.hideLoading();
                $scope.hasSubmit = false;
            });
        }
    }
    $scope.CloseProcessProject = function () {
        $scope.Project = {
        };
        $('#FinishDateText').datepicker(
            { format: "dd/mm/yyyy" }).off('changeDate');
        $('#FinishDateText').datepicker("destroy");
        $('#ProcessProject').modal('hide');
    }



    $scope.ChangeProjectMemberType = function (idx, container) {
        if ($scope.Project.ProjectMembers[idx] !== undefined) {
            if ($scope.Project.ProjectMembers[idx].Role == "1") {
                $scope.Project.ProjectMembers[idx].Role = "2";
                $(container).css("background-color", "orange");
            } else if ($scope.Project.ProjectMembers[idx].Role == "2") {
                $scope.Project.ProjectMembers[idx].Role = "1";
                $(container).css("background-color", "powderblue");
            }
        }
    }
    $scope.InitProjectMemberType = function (idx, container) {
        var assign = $scope.Project.ProjectMembers[idx];
        if (assign !== undefined) {
            if (assign.Role == "1") {
                $(container).css("background-color", "powderblue");
            }
            if (assign.Role == "2") {
                $(container).css("background-color", "orange");
            }
        }
    }
    $scope.InitProjectMemberTypeFromSearch = function (id, container) {
        var assign = $scope.ProjectMemberTemps.filter(e => e.UserId == id)[0];
        var count = $scope.Project.ProjectMembers.filter(e => e.UserId == assign.AssignTo).length;
        if (count == 0) {
            $scope.Project.ProjectMembers.push(assign);
            const index = $('#ProjectMember').val().indexOf(id);
            if (index > -1) {
                var value = $('#ProjectMember').val();
                value.splice(index, 1);
                $('#ProjectMember').val(value);
            }
            $scope.$apply();
        }
        if (assign !== undefined) {
            if (assign.Role == "1") {
                $(container).css("background-color", "powderblue");
            }
            if (assign.Role == "2") {
                $(container).css("background-color", "orange");
            }
        }
    }
    // --------------
    // --- update database ---
    $scope.PostTrackingUpdateDB = function () {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            MainService.PostTrackingUpdateDB().then(function (rs) {

                if (rs.data !== undefined && rs.data == true) {
                    toastr.success('Thành công!', 'Thông báo')
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
    // -----
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
                var promises = [MainService.GetTaskItem(param), MainService.GetTaskPriorities(), MainService.GetProjectCategories({ projectId: $scope.filterTask.projectId, taskId: $scope.filterTask.taskId }), MainService.GetAdminCategories()];
                $q.all(promises).then(function (rs) {
                    if (rs[0].data.Message != undefined && rs[0].data.Message == 'AccessDenied') {
                        toastr.error('Bạn không có quyền chỉnh sửa công việc này', 'Thông báo');
                        $scope.hideLoading();
                        $scope.hasSubmit = false;
                        return;
                    }
                    $scope.TaskItem = rs[0].data;
                    $scope.TaskItemPriorities = rs[1].data;
                    $scope.ProjectCategories = rs[2].data;
                    $scope.AdminCategories = [{
                        Id: guidEmpty,
                        Summary: '-- Chọn phân loại --'
                    }];
                    $scope.AdminCategories = $scope.AdminCategories.concat(rs[3].data);
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
                        data: $scope.ProjectCategories
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
                var promises = [MainService.GetTaskPriorities(), MainService.GetProjectCategories({ projectId: $scope.filterTask.projectId }), MainService.GetNewTaskItem($scope.TaskItem), MainService.GetAdminCategories()];
                $q.all(promises).then(function (rs) {
                    $scope.TaskItemPriorities = rs[0].data;
                    $scope.ProjectCategories = rs[1].data;
                    if ($scope.TaskItemPriorities != undefined && $scope.TaskItemPriorities.length > 0) {
                        $scope.TaskItem.TaskItemPriorityId = $scope.TaskItemPriorities[0].Id;
                    }
                    $scope.TaskItem = rs[2].data;
                    $scope.AdminCategories = [{
                        Id: guidEmpty,
                        Summary: '-- Chọn phân loại --'
                    }];
                    $scope.AdminCategories = $scope.AdminCategories.concat(rs[3].data);
                    $scope.TaskItem.AdminCategoryId = guidEmpty;
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
                        placeholder: 'Chọn thẻ',
                        tags: true,
                        width: '100%',
                        data: $scope.ProjectCategories
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
            MainService.SaveTaskItem($scope.TaskItem, $scope.fileTemps).then(function (rs) {
                if (rs !== undefined && rs.IsSuccess == true) {
                    toastr.success('Thành công!', 'Thông báo');
                    $scope.leftMenu.reload();
                    if ($scope.ShowType === 2) //kanban
                    {
                        if ($scope.TaskItem.Id !== '00000000-0000-0000-0000-000000000000') {
                            var id = $scope.selectedRow.ParentId === null ? $scope.selectedRow.ProjectId : $scope.selectedRow.ParentId;
                            $scope.selectViewBreadCrumbWithParent(id);
                        }
                        else if ($scope.TaskItem.Id === '00000000-0000-0000-0000-000000000000') {

                            $scope.selectViewBreadCrumbWithParent($scope.selectedRow.Id);
                        }
                    }
                    else {
                        if ($scope.TaskItem.IsParentAuto == true) {
                            ;
                            var fromdate = moment($scope.TaskItem.FromDateText, 'DD/MM/YYYY');
                            var todate = moment($scope.TaskItem.ToDateText, 'DD/MM/YYYY');
                            var min = moment($scope.TaskItem.ParentFromDateText, 'DD/MM/YYYY');
                            var max = moment($scope.TaskItem.ParentToDateText, 'DD/MM/YYYY');
                            if (fromdate < min || max < todate) {
                                $scope.getDataByProject(null);
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
    $scope.SaveDraftTaskItem = function () {
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
            }
            else {
                if (!$scope.TaskItem.IsParentAuto && fromDate < min) {
                    toastr.error('Ngày bắt đầu không hợp lệ', 'Thông báo');
                    validate = false;
                } else
                    if (!$scope.TaskItem.IsParentAuto && toDate > max) {
                        toastr.error('Ngày kết thúc không hợp lệ', 'Thông báo');
                        validate = false;
                    }
            }
            if (validate === false) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                return;
            }
            $scope.TaskItem.TaskItemCategories = $('#TaskCategory').val();

            if ($('#TaskGroupType').val() === '1') {
                $scope.TaskItem.IsReport = true;
                $scope.TaskItem.IsGroupLabel = false;
            } else if ($('#TaskGroupType').val() === '2') {
                $scope.TaskItem.IsGroupLabel = true;
                $scope.TaskItem.IsReport = false;
            } else {
                $scope.TaskItem.IsReport = false;
                $scope.TaskItem.IsGroupLabel = false;
            }
            MainService.SaveDraftTaskItem($scope.TaskItem, $scope.fileTemps).then(function (rs) {
                if (rs !== undefined && rs.IsSuccess === true) {
                    toastr.success('Thành công!', 'Thông báo');
                    if ($scope.ShowType === 2) //kanban
                    {
                        if ($scope.TaskItem.Id !== '00000000-0000-0000-0000-000000000000') {
                            var id = $scope.selectedRow.ParentId === null ? $scope.selectedRow.ProjectId : $scope.selectedRow.ParentId;
                            $scope.selectViewBreadCrumbWithParent(id);
                        }
                        else if ($scope.TaskItem.Id === '00000000-0000-0000-0000-000000000000') {

                            $scope.selectViewBreadCrumbWithParent($scope.selectedRow.Id);
                        }
                    }
                    else {
                        if ($scope.TaskItem.IsParentAuto === true) {
                            var fromdate = moment($scope.TaskItem.FromDateText, 'DD/MM/YYYY');
                            var todate = moment($scope.TaskItem.ToDateText, 'DD/MM/YYYY');
                            var min = moment($scope.TaskItem.ParentFromDateText, 'DD/MM/YYYY');
                            var max = moment($scope.TaskItem.ParentToDateText, 'DD/MM/YYYY');
                            if (fromdate < min || max < todate) {
                                $scope.getDataByProject(null);
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
            MainService.GetTaskItem(param).then(function (rs) {
                if (rs.data.Message != undefined && rs.data.Message == 'AccessDenied') {
                    toastr.error('Bạn không có quyền xóa công việc này', 'Thông báo');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    return;
                }
                $scope.showConfirm('Bạn có chắc muốn xóa công việc này?', function (rs) {
                    if (rs == true) {
                        $scope.showLoading(null);
                        MainService.DeleteTaskItem(param).then(function (rs) {
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
    $scope.ScheduleTask = function () {
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

    // -----------------
    // -------Xử lý Công việc-------
    $scope.ViewTaskItemAssign = function (action) {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            $scope.fileTemps = [];
            if ($scope.selectedRow == null || $scope.selectedRow == "project") {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                toastr.error('Vui lòng chọn 1 công việc!', 'Thông báo');
                return;
            }
            $scope.TaskItemAssign = {
            };
            $scope.filterTask.taskId = $scope.selectedRow.Id;
            if (action == "Process") {
                $scope.modalTitle = "Xử lý";
            }
            if (action == "Appraise") {
                $scope.modalTitle = "Đánh giá";
            }
            if (action == "Extend") {
                $scope.modalTitle = "Duyệt gia hạn";
            }
            var param = {
                taskId: $scope.selectedRow.Id,
                action: action
            }
            var promise = [MainService.GetTaskItemAssign(param), MainService.GetTaskItemAssignConfigPoint()]
            $q.all(promise).then(function (rs) {
                if (rs[0].data.Message != undefined && rs[0].data.Message == 'AccessDenied') {
                    toastr.error('Bạn không có quyền chỉnh sửa công việc này', 'Thông báo');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    return;
                }
                $scope.TaskItemAssign = rs[0].data;
                $scope.TaskItemAssign.AllowedExtendDateText = $scope.TaskItemAssign.ExtendDateText;
                $scope.ConfigValue = [];
                $scope.ConfigType = rs[1].data.filter(e => e.Code == 'Task.TypeBarProcesTask')[0].Value;
                if ($scope.ConfigType == 'combobox') {
                    $scope.ConfigPoints = rs[1].data.filter(e => e.Code == 'Task.DataSourceProcesTaskCombobox')[0].Value.split(";");
                    $scope.ConfigPoints.forEach(function (config) {
                        config = {
                            text: config.split(":")[0],
                            value: config.split(":")[1],
                        }
                        if (config.value == $scope.TaskItemAssign.PercentFinish) {
                            $scope.TaskItemAssign.PercentFinishText = config.text;
                        }
                        $scope.ConfigValue.push(config);
                    });
                }
                $("#ExtendDate").val($scope.TaskItemAssign.ExtendDateText);
                $('#ExtendDate').datepicker(
                    { format: "dd/mm/yyyy" }).on('changeDate', function (e) {
                        var valid = true;
                        var max = moment($scope.TaskItemAssign.TaskItemModel.ToDateText, 'DD/MM/YYYY');
                        var extenddate = moment($scope.TaskItemAssign.ExtendDateText, 'DD/MM/YYYY');
                        var date = moment($("#ExtendDate").val(), 'DD/MM/YYYY');
                        if (date < max) {
                            toastr.error('Ngày gia hạn không hợp lệ', 'Thông báo');
                            valid = false;
                            $("#ExtendDate").datepicker("destroy");
                            $("#ExtendDate").datepicker("setDate", extenddate.toDate());
                        }
                        if (valid === true) {
                            $scope.TaskItemAssign.ExtendDateText = $("#ExtendDate").val();
                        }
                        $('.datepicker').hide();
                    });
                $("#AllowedExtendDateText").val($scope.TaskItemAssign.ExtendDateText);
                $('#AllowedExtendDateText').datepicker(
                    { format: "dd/mm/yyyy" }).on('changeDate', function (e) {
                        var valid = true;
                        var max = moment($scope.TaskItemAssign.TaskItemModel.ToDateText, 'DD/MM/YYYY');
                        var date = moment($("#AllowedExtendDateText").val(), 'DD/MM/YYYY');
                        var extenddate = moment($scope.TaskItemAssign.ExtendDateText, 'DD/MM/YYYY');
                        if (date < max) {
                            toastr.error('Ngày gia hạn không hợp lệ', 'Thông báo');
                            valid = false;
                            $("#AllowedExtendDateText").datepicker("destroy");
                            $("#AllowedExtendDateText").datepicker("setDate", extenddate.toDate());
                        }
                        if (valid === true) {
                            $scope.TaskItemAssign.AllowedExtendDateText = $("#AllowedExtendDateText").val();
                        }
                        $('.datepicker').hide();
                    });

                if (action == 'Process') {
                    $scope.action = 'Process';
                    $('#ProcessTaskItem').modal('show');
                } else if (action == 'Appraise') {
                    if ($scope.TaskItemAssign.TaskItemStatus.Code == 'REPORT_RETURN') {
                        $scope.action = 'Return';
                        $('#AppraiseExtendTaskItem').modal('show');
                    } else {
                        $scope.action = 'Appraise';
                        $('#AppraiseTaskItem').modal('show');
                    }
                } else if (action == 'Extend' && $scope.TaskItemAssign.IsExtend == true) {
                    $scope.action = 'Extend';
                    $('#AppraiseExtendTaskItem').modal('show');
                }
                $scope.hideLoading();
                $scope.hasSubmit = false;
            }, function (er) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
            });
        }
    }
    $scope.DeleteFileTaskAssign = function (index, Id) {
        $scope.showConfirm('Bạn có chắc muốn xóa?', function (rs) {
            if (rs) {
                if ($scope.TaskItemAssign.AttachDelIds == undefined) $scope.TaskItemAssign.AttachDelIds = [];
                $scope.TaskItemAssign.AttachDelIds.push(Id);
                $scope.TaskItemAssign.Attachments.splice(index, 1);
            }
        });
    }
    $scope.SaveTaskItemAssign = function (action, isAssignBy) {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            var validate = true;
            if ($scope.TaskItemAssign.IsExtend == true && ($scope.TaskItemAssign.ExtendDateText == '' || $scope.TaskItemAssign.ExtendDateText == undefined)) {
                toastr.error('Vui lòng nhập ngày gia hạn', 'Thông báo');
                $('#ExtendDate').focus();
                $('#ExtendDate').focus();
                validate = false;
            } else
                if ($scope.TaskItemAssign.Description == '' || $scope.TaskItemAssign.Description == undefined) {
                    toastr.error('Vui lòng nhập Nội dung', 'Thông báo');
                    if ($scope.action == 'Process') {
                        $('#ProcessTaskItem #Description').focus();
                    }
                    if ($scope.action == 'Return') {
                        $('#AppraiseExtendTaskItem #Description').focus();
                    }
                    if ($scope.action == 'Appraise') {
                        $('#AppraiseTaskItem #Description').focus();
                    }
                    if ($scope.action == 'Extend') {
                        $('#AppraiseExtendTaskItem #Description').focus();
                    }
                    validate = false;
                } else if (isAssignBy == true && action == 'AppraiseExtend') {
                    if ($scope.TaskItemAssign.AllowedExtendDateText == '' || $scope.TaskItemAssign.AllowedExtendDateText == undefined) {
                        toastr.error('Vui lòng nhập ngày cho phép gia hạn', 'Thông báo');
                        $('#AllowedExtendDateText').focus();
                        $('#AllowedExtendDateText').focus();
                        validate = false;
                    }
                }
            if (validate == false) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                return;
            }
            if (isAssignBy == true && $scope.TaskItemAssign.IsExtend === true) {
                $scope.TaskItemAssign.ExtendDateText = $scope.TaskItemAssign.AllowedExtendDateText;
            }
            $scope.TaskItemAssign.ActionText = action;
            $scope.TaskItemAssign.IsAssignBy = isAssignBy;
            MainService.ProcessTaskItemAssign($scope.TaskItemAssign, $scope.fileTemps).then(function (rs) {
                if (rs !== undefined && rs.IsSuccess == true) {
                    toastr.success('Thành công!', 'Thông báo')
                    if ($scope.ShowType === 2) //kanban
                    {
                        if ($scope.TaskItemAssign !== undefined) {
                            var id = $scope.selectedRow.ParentId === null ? $scope.selectedRow.ProjectId : $scope.selectedRow.ParentId;
                            $scope.selectViewBreadCrumbWithParent(id);
                        }
                        else if ($scope.TaskItemAssign === undefined) {
                            $scope.selectViewBreadCrumbWithParent($scope.selectedRow.Id);
                        }
                    }
                    else {
                        var check = true;
                        if (action == 'AppraiseExtend') {
                            $scope.getDataByProject(null);
                        }
                        if (check === true) {
                            if ($scope.TaskItemAssign !== undefined) {
                                var parentBranch = $scope.my_tree.get_parent_branch($scope.selectedRow);
                                parentBranch.HasLoading = true;
                                $scope.callbackFunctionInController(parentBranch);
                            }
                            else if ($scope.TaskItemAssign === undefined) {
                                $scope.callbackFunctionInController($scope.selectedRow);
                            }
                        }
                    }
                }
                $scope.CloseTaskItemAssign();
                $scope.hideLoading();
                $scope.hasSubmit = false;
            }, function (er) {
                toastr.error('Có lỗi trong quá trình xử lý!', 'Thông báo')
                $scope.hideLoading();
                $scope.hasSubmit = false;
            });
        }
    }
    $scope.CloseTaskItemAssign = function () {
        $scope.TaskItemAssign = {
        };
        $("#AllowedExtendDateText").datepicker({ format: "dd/mm/yyyy" }).off('changeDate');
        $("#ExtendDate").datepicker({ format: "dd/mm/yyyy" }).off('changeDate');
        $("#AllowedExtendDateText").datepicker("destroy");
        $("#ExtendDate").datepicker("destroy");
        $('#ProcessTaskItem').modal('hide');
        $('#AppraiseTaskItem').modal('hide');
        $('#AppraiseExtendTaskItem').modal('hide');
    }
    // -----------------
    // -------Cập nhật tình trạng Công việc-------
    $scope.EvictFinishTaskItem = function (action) {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            if ($scope.selectedRow == null || $scope.selectedRow.Type == 'project') {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                toastr.error('Vui lòng chọn 1 công việc!', 'Thông báo');
                return;
            }

            $scope.filterTask.taskId = $scope.selectedRow.Id;
            $scope.filterTask.projectId = $scope.selectedRow.ProjectId;
            if (action === 'Evict') {
                $scope.modalTitle = "Thu hồi công việc";
                $scope.IsEvict = true;
            }
            if (action === 'Finish') {
                $scope.modalTitle = "Kết thúc công việc";
                $scope.IsEvict = false;
            }
            var param = {
                id: $scope.selectedRow.Id
            }
            var promises = [MainService.GetTaskItem(param), MainService.GetTaskPriorities(), MainService.GetProjectCategories({ projectId: $scope.filterTask.projectId, taskId: $scope.filterTask.taskId })];
            $q.all(promises).then(function (rs) {
                if (rs[0].data.Message != undefined && rs[0].data.Message == 'AccessDenied') {
                    toastr.error('Bạn không có quyền chỉnh sửa công việc này', 'Thông báo');
                    $scope.hideLoading();
                    $scope.hasSubmit = false;
                    return;
                }
                $scope.TaskItem = rs[0].data;
                $('#EvictFinishTaskItem').modal('show');
                $scope.hideLoading();
                $scope.hasSubmit = false;
            }, function (er) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
            });
        }
    }
    $scope.UpdateStatusTaskItem = function (action) {
        if (!$scope.hasSubmit) {
            $scope.showLoading(null);
            $scope.hasSubmit = true;
            var validate = true;
            if ($scope.TaskItem.Description == '' || $scope.TaskItem.Description == undefined) {
                toastr.error('Vui lòng lý do', 'Thông báo');
                $('#Description').focus();
                validate = false;
            }
            if (validate == false) {
                $scope.hideLoading();
                $scope.hasSubmit = false;
                return;
            }
            $scope.TaskItem.ActionText = action;
            MainService.UpdateStatusTaskItem($scope.TaskItem).then(function (rs) {
                if (rs.data !== undefined && rs.data.IsSuccess == true) {
                    toastr.success('Thành công!', 'Thông báo')
                    if ($scope.TaskItem.Id !== undefined) {
                        var parentBranch = $scope.my_tree.get_parent_branch($scope.selectedRow);
                        $scope.callbackFunctionInController(parentBranch);
                    }
                    else if ($scope.TaskItem.Id === undefined) {
                        $scope.callbackFunctionInController($scope.selectedRow);
                    }
                    $scope.CloseTaskItemEvict();
                } else {
                    if (rs.data !== undefined && rs.data.Message == "ProcessExpired") {
                        toastr.error('Thao tác của bạn đã hết hạn!', 'Thông báo')
                        $scope.CloseTaskItemEvict();
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
    }
    $scope.CloseTaskItemEvict = function () {
        $scope.TaskItem = {
        };
        $('#EvictFinishTaskItem').modal('hide');
    }
    // -----------------------------
    // -----------------------------------------
    $scope.ImportExcel = function (files) {
        if (!$scope.hasSubmit) {
            if (files.length !== 0) {
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        var n = files[i].name.lastIndexOf('.');
                        var fileType = files[i].name.substring(n + 1);
                        if (!isFileInvalid(files[i].size, fileType)) {
                            return;
                        }
                    }
                    $scope.fileTemps = [];
                    $scope.fileTemps = $scope.fileTemps.concat(files);
                    $scope.showLoading(null);
                    $scope.hasSubmit = true;
                    if ($scope.selectedRow == null) {
                        $scope.hasSubmit = false;
                        toastr.error('Vui lòng chọn 1 dự án/ công việc!', 'Thông báo');
                        return;
                    }
                    $scope.filter.projectId = $scope.selectedRow.ProjectId;
                    var param = {
                        projectId: $scope.filter.projectId,
                    }
                    if ($scope.selectedRow.Type !== 'project') {
                        param.taskId = $scope.selectedRow.Id
                    }
                    MainService.ImportExcel(param, $scope.fileTemps).then(function (rs) {
                        if (rs === 'AccessDenied') {
                            toastr.error('Bạn không có quyền Import Dự án này!', 'Thông báo');
                        } else
                        if (rs !== undefined && rs.Message == 'Success') {
                            window.location.reload();
                        } else if (rs !== undefined && rs.Message == 'Failure') {
                            window.open(CommonUtils.RootURL("Task/Project/DownloadFile") + "?path=" + rs.Url);
                        }
                        $scope.hideLoading();
                        $scope.hasSubmit = false;
                    }, function (er) {
                        toastr.error('Có lỗi trong quá trình xử lý!', 'Thông báo')
                        $scope.hideLoading();
                        $scope.hasSubmit = false;
                    });
                } else {
                    Notify({ title: "Thông báo", message: "Có lỗi", type: "error", delay: 2000 });
                }
            }
        }
    }
    $scope.ImportMSProject = function (files) {

        if (!$scope.hasSubmit) {
            if (files.length !== 0) {
                if (files.length > 0) {
                    for (var i = 0; i < files.length; i++) {
                        var n = files[i].name.lastIndexOf('.');
                        var fileType = files[i].name.substring(n + 1);
                        if (!isFileInvalid(files[i].size, fileType)) {
                            return;
                        }
                    }
                    $scope.fileTemps = [];
                    $scope.fileTemps = $scope.fileTemps.concat(files);
                    $scope.showLoading(null);
                    $scope.hasSubmit = true;
                    if ($scope.selectedRow == null) {
                        $scope.hasSubmit = false;
                        $scope.hideLoading();
                        toastr.error('Vui lòng chọn 1 dự án!', 'Thông báo');
                        return;
                    }
                    $scope.filter.projectId = $scope.selectedRow.ProjectId;
                    var param = {
                        projectId: $scope.filter.projectId,
                    }
                    MainService.ImportMSProject(param, $scope.fileTemps).then(function (rs) {
                        if (rs !== undefined && rs == 'Success') {
                            window.location.reload();
                        } else if (rs !== undefined && rs.Message == 'Failure') {
                            window.open(CommonUtils.RootURL("Task/Project/DownloadFile") + "?path=" + rs.Url);
                        }
                        $scope.hideLoading();
                        $scope.hasSubmit = false;
                    }, function (er) {
                        toastr.error('Có lỗi trong quá trình xử lý!', 'Thông báo')
                        $scope.hideLoading();
                        $scope.hasSubmit = false;
                    });
                } else {
                    Notify({ title: "Thông báo", message: "Có lỗi", type: "error", delay: 2000 });
                }
            }
        }
    }
    $scope.ExportMSProject = function () {
        if ($scope.selectedRow == null) {
            $scope.hasSubmit = false;
            toastr.error('Vui lòng chọn 1 dự án!', 'Thông báo');
            return;
        }
        $scope.filter.projectId = $scope.selectedRow.ProjectId;
        var param = {
            projectId: $scope.filter.projectId
        }
        window.open(CommonUtils.RootURL("Task/Project/ExportMSProject") + "?projectId=" + param.projectId);
    }
    // -----------------------------------------
});
