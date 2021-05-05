(function () {
    var module;
    module = angular.module('treeGrid', []);
    module.directive('treeGrid', [
        '$timeout', function ($timeout) {
            return {
                restrict: 'E', 
                templateUrl:'../../Areas/Task/Content/angularjs/template/tree-grid-template.html',
                //template: "<div><table id=\"dragtable\" class=\"table table-bordered table-striped tree-grid\"><thead class=\"text-primary\"><tr><th>Nội dung</th><th ng-repeat=\"col in colDefinitions\">{{col.displayName || col.field}}</th></tr></thead><tbody><tr ng-repeat=\"row in tree_rows | filter:{visible:true} track by row.branch.uid\" draggable drag=\"handleDrag\" dragImage=\"{{dragImageId}}\" dragData=\"{{ row.branch }}\" droppable drop=\"handleDrop\" style=\"cursor: point\" ng-class=\"(row.branch.HasPagination? ' haspagination ':'')+'level-' + {{ row.level }} + (row.branch.selected ? ' active':'')\" class=\"tree-grid-row\" ng-dblclick=\"user_double_clicks_branch(row.branch)\" ng-click=\"user_clicks_branch2(row.branch)\"><td class=\"text-primary\"><a ng-click=\"user_clicks_branch(row.branch)\"><i id=\"iconexpanded\" ng-class=\" (row.branch.HasLoading) ? 'glyphicon fa icon-minus glyphicon-minus spinner': row.tree_icon\" ng-click=\"row.branch.expanded = !row.branch.expanded\" class=\"indented tree-icon {{row.branch.Process}}\"></i></a><span class=\"indented tree-label\">{{row.branch[expandingProperty]}}</span></td><td ng-repeat=\"col in colDefinitions\">{{row.branch[col.field]}}</td></tr></tbody><table></div>",
                replace: true,
                scope: {
                    treeData: '=',
                    colDefs: '=',
                    expandOn: '=',
                    onSelect: '&',
                    initialSelection: '@',
                    treeControl: '=',
                    callback: '&myClickCallback',
                    callbackDblClick: '&myDblClickCallback',
                    callbackDragDrop: '&myDragDropCallback'
                },
                link: function (scope, element, attrs) {
                    var error, expandingProperty, expand_all_parents, expand_level, for_all_ancestors, for_each_branch, get_parent, n, on_treeData_change, select_branch, selected_branch, tree;
                    error = function (s) {
                        console.log('ERROR:' + s);
                        return void 0;
                    };
                    if (attrs.iconExpand == null || attrs.iconExpand===undefined) {
                        attrs.iconExpand = 'icon-plus  glyphicon glyphicon-plus  fa fa-plus';
                    }
                    if (attrs.iconCollapse == null) {
                        attrs.iconCollapse = 'icon-minus glyphicon glyphicon-minus fa fa-minus';
                    }
                    else {
                        attrs.iconExpand = 'icon-plus  glyphicon glyphicon-plus  fa fa-plus';
                    }
                    if (attrs.iconLeaf == null) {
                        attrs.iconLeaf = 'icon-file  glyphicon glyphicon-file  fa fa-file';
                    }
                    if (attrs.iconRefresh == null) {
                        attrs.iconRefresh = 'icon-file  glyphicon glyphicon-file  fa fa-refresh';
                    }
                    if (attrs.expandLevel == null) {
                        attrs.expandLevel = '2';
                    }
                    expand_level = parseInt(attrs.expandLevel, 10);
                    if (!scope.treeData) {
                        return;
                    }
                    if (scope.treeData.length == null) {
                        if (treeData.label != null) {
                            scope.treeData = [treeData];
                        } else {
                            return;
                        }
                    }
                    if (attrs.expandOn) { 
                        expandingProperty = scope.expandOn;
                        scope.expandingProperty = scope.expandOn;
                    }
                    else {
                        var _firstRow = scope.treeData[0],
                            _keys = Object.keys(_firstRow);
                        for (var i = 0, len = _keys.length; i < len; i++) {
                            if (typeof (_firstRow[_keys[i]]) == 'string') {
                                expandingProperty = _keys[i];
                                break;
                            }
                        }
                        if (!expandingProperty) expandingProperty = _keys[0];
                        scope.expandingProperty = expandingProperty;
                    }
                    if (!attrs.colDefs) {
                        var _col_defs = [], _firstRow = scope.treeData[0], _unwantedColumn = ['children', 'level', 'expanded', expandingProperty];
                        for (var idx in _firstRow) {
                            if (_unwantedColumn.indexOf(idx) == -1)
                                _col_defs.push({ field: idx });
                        }
                        scope.colDefinitions = _col_defs;
                    }
                    else {
                        //console.log(scope.colDefs);
                        //scope.colDefinitions = scope.colDefs.filter(filter => filter.field !== scope.expandingProperty);
                        scope.colDefinitions = scope.colDefs;
                    }
                    for_each_branch = function (f) {
                        var do_f, root_branch, _i, _len, _ref, _results;
                        do_f = function (branch, level) {
                            var child, _i, _len, _ref, _results;
                            f(branch, level);
                            if (branch.children != null) {
                                _ref = branch.children;
                                _results = [];
                                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                                    child = _ref[_i];
                                    _results.push(do_f(child, level + 1));
                                }
                                return _results;
                            }
                        };
                        _ref = scope.treeData;
                        _results = [];
                        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                            root_branch = _ref[_i];
                            _results.push(do_f(root_branch, 1));
                        }
                        return _results;
                    };
                    selected_branch = null;
                    select_branch = function (branch) {
                        if (!branch) {
                            if (selected_branch != null) {
                                selected_branch.selected = false;
                            }
                            selected_branch = null;
                            return;
                        }
                        if (branch !== selected_branch)
                        {
                            if (selected_branch != null) {
                                selected_branch.selected = false;
                            }
                            branch.selected = true;
                            selected_branch = branch;
                            scope.selectedOnValue = selected_branch;
                            expand_all_parents(branch);
                            if (branch.onSelect != null) {
                                return $timeout(function () {
                                    return branch.onSelect(branch);
                                });
                            } else {
                                if (scope.onSelect != null) {
                                    return $timeout(function () {
                                        return scope.onSelect({
                                            branch: branch
                                        });
                                    });
                                }
                            }
                        }
                    };
                    scope.user_clicks_branch2 = function (branch) {
                          if (branch !== selected_branch) {
                            //return select_branch(branch);
                              if (!branch.HasPagination) {
                                  return select_branch(branch);
                              }
                              else {
                                  if (branch.HasLoading) {
                                      return;
                                  }
                                  else {
                                      if (branch !== undefined) {
                                          branch.HasLoading = true;
                                      }
                                      return $timeout(function () {
                                          return scope.callback({
                                              branch: branch
                                          });
                                      });
                                  }
                              }
                        }
                    }
                    scope.user_double_clicks_branch = function (branch) {
                            //if (branch !== selected_branch) {
                            //    if (!branch.HasPagination) {
                            //        select_branch(branch);
                            //    }
                            //}
                        return $timeout(function () {
                            return scope.callbackDblClick({
                                branch: branch
                            });
                        });
                    }
                    scope.user_clicks_branch = function (branch) {
                        if (branch.HasLoading) {
                            return;
                        }
                        else {
                            if (branch.HasChildren && branch.expanded === true && branch.children != null && branch.children.length == 0
                            ) {
                                //var parentBrach = tree.get_parent_branch(branch);
                                if (branch !== undefined) {
                                    branch.HasLoading = true;
                                }
                                //if (scope.callback())
                                //    scope.callback({
                                //        branch: branch
                                //    });

                                return $timeout(function () {
                                    return scope.callback({
                                        branch: branch
                                    });
                                });
                                //var refreshHandler = attrs.refresh;
                                //scope.$apply(function () {
                                //    refreshHandler({ data: conditions }, scope, { $event: event });
                                //});
                                //debugger;
                                //      return $timeout(function () {
                                //          return scope.callback(branch);
                                //    });
                                //scope.callback().apply(branch); ; // fires alert
                            }
                            else {
                                if (!branch.HasPagination) {
                                    return select_branch(branch);
                                }
                            }
                        } 
                        //if (branch.onClick != null) {
                        //        return $timeout(function () {
                        //            return branch.onClick(branch);
                        //        });
                        //    } else {
                        //    if (scope.onClick != null) {
                        //            return $timeout(function () {
                        //                return scope.onClick({
                        //                    branch: branch
                        //                });
                        //            });
                        //        }
                        //    }
                        //if (branch.expanded===false)
                        ////if (branch !== selected_branch)
                        //{
                        //    debugger;
                        //    //scope.my_clickCE_handler(branch);
                        //    //debugger;
                        //    //return select_branch(branch);
                        //    //if (branch.onSelect != null) {
                        //    //    return $timeout(function () {
                        //    //        return branch.onSelect(branch);
                        //    //    });
                        //    //} else {
                        //    //    if (scope.onSelect != null) {
                        //    //        return $timeout(function () {
                        //    //            return scope.onSelect({
                        //    //                branch: branch
                        //    //            });
                        //    //        });
                        //    //    }
                        //    //}
                        //}
                        //else {
                        //    return select_branch(branch);
                        //} 
                        //if (branch !== selected_branch) {
                        //    return select_branch(branch);
                        //}
                        //if (branch.expanded === true)
                    };
                    get_parent = function (child) {
                        var parent;
                        parent = void 0;
                        if (child.parent_uid) {
                            for_each_branch(function (b) {
                                if (b.uid === child.parent_uid) {
                                    return parent = b;
                                }
                            });
                        }
                        return parent;
                    };
                    get_branch = function (child) {
                        var branch;
                        branch = void 0;
                            for_each_branch(function (b) {
                                //if (b.uid === child.uid) {
                                //    return branch = b;
                                //}
                                if (b.Id === child.Id) {
                                    return branch = b;
                                }
                            });
                        return branch;
                    };
                    get_branch_by_id = function (b) {
                        var branch;
                        branch = void 0;
                        for_each_branch(function (b) {
                            //if (b.uid === child.uid) {
                            //    return branch = b;
                            //}
                            if (branch ===undefined) {
                                if (b.Id === b.ProjectId) {
                                    return branch = b;
                                }
                            }
                        });
                        return branch;
                    };
                    for_all_ancestors = function (child, fn) {
                        var parent;
                        parent = get_parent(child);
                        if (parent != null) {
                            fn(parent);
                            return for_all_ancestors(parent, fn);
                        }
                    };
                    expand_all_parents = function (child) {
                        return for_all_ancestors(child, function (b) {
                            return b.expanded = true;
                        });
                    };
                    scope.tree_rows = [];
                    on_treeData_change = function () {
                        var add_branch_to_list, root_branch, _i, _len, _ref, _results;
                        for_each_branch(function (b, level) {
                            if (!b.uid) {
                                return b.uid = "" + Math.random();
                            }
                        });
                        for_each_branch(function (b) {
                            var child, _i, _len, _ref, _results;
                            if (angular.isArray(b.children)) {
                                _ref = b.children;
                                _results = [];
                                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                                    child = _ref[_i];
                                    _results.push(child.parent_uid = b.uid);
                                }
                                return _results;
                            }
                        });
                        scope.tree_rows = [];
                        for_each_branch(function (branch) {
                            var child, f;
                            if (branch.children) {
                                if (branch.children.length > 0) {
                                    f = function (e) {
                                        if (typeof e === 'string') {
                                            return {
                                                label: e,
                                                children: []
                                            };
                                        } else {
                                            return e;
                                        }
                                    };
                                    return branch.children = (function () {
                                        var _i, _len, _ref, _results;
                                        _ref = branch.children;
                                        _results = [];
                                        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                                            child = _ref[_i];
                                            _results.push(f(child));
                                        }
                                        return _results;
                                    })();
                                }
                            } else {
                                return branch.children = [];
                            }
                        });
                        add_branch_to_list = function (level, branch, visible) {
                            var child, child_visible, tree_icon, _i, _len, _ref, _results;
                            if (branch.expanded == null) {
                                branch.expanded = false;
                            }
                            if ((!branch.children || branch.children.length === 0) && !(branch.HasChildren)) {
                                tree_icon = attrs.iconLeaf;
                            } else {
                    
                                if (branch.expanded) {
                                    tree_icon = attrs.iconCollapse;
                                } else {
                                    tree_icon = attrs.iconExpand;
                                }
                            }
                            branch.level = level;
                            scope.tree_rows.push({
                                level: level,
                                branch: branch,
                                label: branch[expandingProperty],
                                tree_icon: tree_icon,
                                visible: visible
                            });
                            if (branch.children != null) {
                                _ref = branch.children;
                                _results = [];
                                for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                                    child = _ref[_i];
                                    child_visible = visible && branch.expanded;
                                    _results.push(add_branch_to_list(level + 1, child, child_visible));
                                }
                                return _results;
                            }
                        };
                        _ref = scope.treeData;
                        _results = [];
                        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
                            root_branch = _ref[_i];
                            _results.push(add_branch_to_list(1, root_branch, true));
                        }
                        return _results;
                    };
                    scope.$watch('treeData', on_treeData_change, true);
                    if (attrs.initialSelection != null) {
                        for_each_branch(function (b) {
                            if (b.label === attrs.initialSelection) {
                                return $timeout(function () {
                                    return select_branch(b);
                                });
                            }
                        });
                    }
                    n = scope.treeData.length;
                    for_each_branch(function (b, level) {
                        b.level = level;
                        return b.expanded = b.level < expand_level;
                    });
                    if (scope.treeControl != null) {
                        if (angular.isObject(scope.treeControl)) {
                            tree = scope.treeControl;
                            tree.expand_all = function () {
                                return for_each_branch(function (b, level) {
                                    return b.expanded = true;
                                });
                            };
                            tree.collapse_all = function () {
                                return for_each_branch(function (b, level) {
                                    return b.expanded = false;
                                });
                            };
                            tree.get_first_branch = function () {
                                n = scope.treeData.length;
                                if (n > 0) {
                                    return scope.treeData[0];
                                }
                            };
                            tree.select_first_branch = function () {
                                var b;
                                b = tree.get_first_branch();
                                return tree.select_branch(b);
                            };
                            tree.get_selected_branch = function () {
                                return selected_branch;
                            };
                            tree.get_parent_branch = function (b) {
                                return get_parent(b);
                            };
                            tree.get_branch = function (b) {
                                return get_branch(b);
                            };
                            tree.get_branch_by_id = function (b) {
                                return get_branch_by_id(b);
                            };
                            tree.select_branch = function (b) {
                                select_branch(b);
                                return b;
                            };
                            tree.get_children = function (b) {
                                return b.children;
                            };
                            tree.select_parent_branch = function (b) {
                                var p;
                                if (b == null) {
                                    b = tree.get_selected_branch();
                                }
                                if (b != null) {
                                    p = tree.get_parent_branch(b);
                                    if (p != null) {
                                        tree.select_branch(p);
                                        return p;
                                    }
                                }
                            };
                            tree.add_branch = function (parent, new_branch) {
                                if (parent != null) {
                                    parent.children.push(new_branch);
                                    parent.expanded = true;
                                } else {
                                    scope.treeData.push(new_branch);
                                }
                                return new_branch;
                            };
                            tree.delete_branch_pagination_children = function (parent) {
                                if (parent != null) {
                                    var valueHasPagination = parent.children.slice(-1)[0];
                                    if (valueHasPagination !== undefined && valueHasPagination.HasPagination) {
                                        parent.children.pop();
                                    }
                                }
                                return parent;
                            };
                    tree.delete_branch_pagination_roof = function () {
                        var valueHasPagination = scope.treeData.slice(-1)[0];
                        if (valueHasPagination !== undefined && valueHasPagination.HasPagination) {
                            scope.treeData.pop();
                        }
                        return valueHasPagination;
                            };
                            tree.reset_project_all = function () {
                                if (!scope.treeData) {
                                    return;
                                }
                                if (scope.treeData !== undefined && scope.treeData.length>0) {
                                    scope.treeData.splice(0, scope.treeData.length);
                                }
                                return ;
                            };
                            tree.add_root_branch = function (new_branch) {
                                tree.add_branch(null, new_branch);
                                return new_branch;
                            };
                            tree.expand_branch = function (b) {
                                if (b == null) {
                                    b = tree.get_selected_branch();
                                }
                                if (b != null) {
                                    b.expanded = true;
                                    return b;
                                }
                            };
                            tree.collapse_branch = function (b) {
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    b.expanded = false;
                                    return b;
                                }
                            };
                            tree.get_siblings = function (b) {
                                var p, siblings;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    p = tree.get_parent_branch(b);
                                    if (p) {
                                        siblings = p.children;
                                    } else {
                                        siblings = scope.treeData;
                                    }
                                    return siblings;
                                }
                            };
                            tree.get_next_sibling = function (b) {
                                var i, siblings;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    siblings = tree.get_siblings(b);
                                    n = siblings.length;
                                    i = siblings.indexOf(b);
                                    if (i < n) {
                                        return siblings[i + 1];
                                    }
                                }
                            };
                            tree.get_prev_sibling = function (b) {
                                var i, siblings;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                siblings = tree.get_siblings(b);
                                n = siblings.length;
                                i = siblings.indexOf(b);
                                if (i > 0) {
                                    return siblings[i - 1];
                                }
                            };
                            tree.select_next_sibling = function (b) {
                                var next;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    next = tree.get_next_sibling(b);
                                    if (next != null) {
                                        return tree.select_branch(next);
                                    }
                                }
                            };
                            tree.select_prev_sibling = function (b) {
                                var prev;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    prev = tree.get_prev_sibling(b);
                                    if (prev != null) {
                                        return tree.select_branch(prev);
                                    }
                                }
                            };
                            tree.get_first_child = function (b) {
                                var _ref;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    if (((_ref = b.children) != null ? _ref.length : void 0) > 0) {
                                        return b.children[0];
                                    }
                                }
                            };
                            tree.get_closest_ancestor_next_sibling = function (b) {
                                var next, parent;
                                next = tree.get_next_sibling(b);
                                if (next != null) {
                                    return next;
                                } else {
                                    parent = tree.get_parent_branch(b);
                                    return tree.get_closest_ancestor_next_sibling(parent);
                                }
                            };
                            tree.get_next_branch = function (b) {
                                var next;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    next = tree.get_first_child(b);
                                    if (next != null) {
                                        return next;
                                    } else {
                                        next = tree.get_closest_ancestor_next_sibling(b);
                                        return next;
                                    }
                                }
                            };
                            tree.select_next_branch = function (b) {
                                var next;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    next = tree.get_next_branch(b);
                                    if (next != null) {
                                        tree.select_branch(next);
                                        return next;
                                    }
                                }
                            };
                            tree.last_descendant = function (b) {
                                var last_child;
                                if (b == null) {
                                }
                                n = b.children.length;
                                if (n === 0) {
                                    return b;
                                } else {
                                    last_child = b.children[n - 1];
                                    return tree.last_descendant(last_child);
                                }
                            };
                            tree.get_prev_branch = function (b) {
                                var parent, prev_sibling;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    prev_sibling = tree.get_prev_sibling(b);
                                    if (prev_sibling != null) {
                                        return tree.last_descendant(prev_sibling);
                                    } else {
                                        parent = tree.get_parent_branch(b);
                                        return parent;
                                    }
                                }
                            };
                            return tree.select_prev_branch = function (b) {
                                var prev;
                                if (b == null) {
                                    b = selected_branch;
                                }
                                if (b != null) {
                                    prev = tree.get_prev_branch(b);
                                    if (prev != null) {
                                        tree.select_branch(prev);
                                        return prev;
                                    }
                                }
                            };
                        }
                    }
                }
                ,
                controller: function($scope) {
                    $scope.dragIndex = 0;
                    $scope.dragImageId = "dragtable";


                    $scope.handleDrop = function (draggedData,
                        targetElem) {
                        function swapArrayElements(array_objectS, array_objectD, array_object_ChildrendD, index_a, index_b) {
        
                            var dataS = angular.fromJson(index_a);
                            var dataD = angular.fromJson(index_b);
                            if (dataS.Id !== dataD.Id) {
                                if (array_objectS != undefined) {
                                    if (
                                      //  dataS.ParentId === dataD.ParentId &&
                                        dataS.ProjectId === dataD.ProjectId
                                       // && dataS.level === dataD.level
                                    ) {
                                        var indexA = array_objectS.children.map(function (e) { return e.Id; }).indexOf(dataS.Id);
                                        var indexB = array_objectS.children.map(function (e) { return e.Id; }).indexOf(dataD.Id);
                                        //var indexA = array_object.children.indexOf(dataS);
                                        //var indexB = array_object.children.indexOf(dataD);
                                        var element = array_objectS.children[indexA];
                                        array_objectS.children.splice(indexA, 1);
                                        array_objectD.children.splice(indexB, 0, element);
                                        //array_object.children.splice(indexA, 0, array_object.children.splice(indexB, 1)[0]);
                                        //$scope.treeControl.add_branch(array_object, dataS);
                                        //for (_i = 0, _len = array_object.children.length; _i < _len; _i++) {
                                        //    if (array_object.children === dataD) {
                                        //        array_object.children.push(dataS);
                                        //    }
                                        //    if (array_object.children === dataS) {
                                        //        array_object.children[i]
                                        //    }
                                        //    child = _ref[_i];
                                        //    _results.push(do_f(child, level + 1));
                                        //}
                                        //array_object.children.splice(dataD, 0, array_object.children.splice(dataS, 1)[0]);
                                        //toastr.error('1', 'Thông báo')
                                        return 11;
                                    }
                                    else if (dataS.ParentId !== dataD.ParentId

                                        && dataS.ProjectId === dataD.ProjectId
                                        && dataS.level === dataD.level) {
                                        var indexSA = array_objectS.children.map(function (e) { return e.Id; }).indexOf(dataS.Id);
                                        var indexDB = array_objectD.children.map(function (e) { return e.Id; }).indexOf(dataD.Id);
                                        array_objectS.children.splice(indexSA, 1);
                                        //dataS.ParentId = array_objectD.ParentId;
                                        array_objectD.children.splice(indexDB + 1, 0, dataS);
                                        //alert("Khac noted"); 
                                        return 21;

                                    }
                                    else if (dataS.ParentId !== dataD.ParentId
                                        && dataS.ProjectId === dataD.ProjectId
                                        && dataS.level !== dataD.level
                                        && dataS.level > dataD.level
                                        && dataS.level - dataD.level <= 1
                                        && array_objectD.expanded === true
                                    ) {
                                        var indexSA = array_objectS.children.map(function (e) { return e.Id; }).indexOf(dataS.Id);
                                        var indexDB = array_object_ChildrendD.children.map(function (e) { return e.Id; }).indexOf(dataD.Id);
                                        array_objectS.children.splice(indexSA, 1);
                                        //dataS.ParentId = array_object_ChildrendD.ParentId;
                                        array_object_ChildrendD.children.splice(indexDB + 1, 0, dataS);
                                        //alert("Khac noted"); bo vao parent khac
                                        return 31;

                                    }
                                }
                                else {
                                    //Project
                                    return 44;
                                }
                            }
                            //var temp = array_object[index_a];
                            //array_object[index_a] = array_object[index_b];
                            //array_object[index_b] = temp;
                        }
                        //var tempFrom = angular.fromJson(draggedData);
                        var brachChildren = $scope.treeControl.get_branch(angular.fromJson(targetElem));
                        var brachS = $scope.treeControl.get_parent_branch(angular.fromJson(draggedData));
                        var brachD = $scope.treeControl.get_parent_branch(angular.fromJson(targetElem));

                        ////debugger;
                        //if (tempTo.children.length==0) {
                        //    swapArrayElements(brachS, tempTo, draggedData, targetElem);
                        //}
                        //else {
                        //    swapArrayElements(brachS, brachD, draggedData, targetElem);
                        //}
                     //var resultDragdrop= swapArrayElements(brachS, brachD, brachChildren, draggedData, targetElem);
                        //alert(resultDragdrop);
                        return $timeout(function () {
                            //brachD.HasLoading = true;
                            return $scope.callbackDragDrop({
                                branchS: brachS,
                                branchD: brachD,
                                branchChildrenD: brachChildren,
                                indexS: draggedData,
                                indexD: targetElem
                            });
                        });
                    };

                    $scope.handleDrag = function (rowIndex) {


                        if (rowIndex !== null) {

                            $scope.dragIndex = rowIndex.replace(/["']/g, "");;

                        }

                    };



                }
                //,
                //compile: function (elem) {
                //    return function (ielem, $scope) {
                //        alert("Compile");
                //        $compile(ielem)($scope);


                //    };


                //}

            };
        }
    ]);

}).call(this);
