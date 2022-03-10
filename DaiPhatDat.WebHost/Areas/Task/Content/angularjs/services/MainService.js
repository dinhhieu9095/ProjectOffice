app.service("MainService", function ($http, $q, objectToFormData) {
    this.GetDataByProject = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Home/GetDataByProject"),
            dataType: 'json',
            async: true,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.GetViewBreadCrumbWithParent = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Home/GetViewBreadCrumbWithParent"),
            dataType: 'json',
            async: true,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.moveDataByTask = function (data)
    {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Home/MoveDataByTask"),
            dataType: 'json',
            async: true,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.GetDataByTitleTable = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Home/GetDataByTitleTable"),
            dataType: 'json',
            async: true,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.GetProject = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Project/GetProject"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.SaveProject = function (project, files) {
        var url = CommonUtils.RootURL("Task/Project/SaveProject");
        var defer = $q.defer();
        delete $http.defaults.headers.common['X-Requested-With'];
        $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var frm = objectToFormData(project);
                if (files != null) {
                    angular.forEach(files, function (value, index) {
                        frm.append("File[" + index + "]", value);
                    });
                }
                return frm;
            },
            data: project
        }).then(
            function (rs) {
                defer.resolve(rs.data);
            }, function (data, status, headers, config) {
                defer.reject();
            });

        return defer.promise;
    }
    this.UpdateStatusProject = function (project, files) {
        var url = CommonUtils.RootURL("Task/Project/UpdateStatusProject");
        var defer = $q.defer();
        delete $http.defaults.headers.common['X-Requested-With'];
        $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var frm = objectToFormData(project);
                if (files != null) {
                    angular.forEach(files, function (value, index) {
                        frm.append("File[" + index + "]", value);
                    });
                }
                return frm;
            },
            data: project
        }).then(
            function (rs) {
                defer.resolve(rs.data);
            }, function (data, status, headers, config) {
                defer.reject();
            });

        return defer.promise;
    }

    this.ExportExcel = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Project/ExportExcel"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request; 
    }//

    this.DeleteProject = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Project/DeleteProject"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetProjectTypes = function () {
        var request = $http({
            method: "GET",
            url: CommonUtils.RootURL("Task/Project/GetProjectTypes"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetProjectStatuses = function () {
        var request = $http({
            method: "GET",
            url: CommonUtils.RootURL("Task/Project/GetProjectStatuses"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetProjectCategories = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Project/GetProjectCategories"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetProjectPriorities = function () {
        var request = $http({
            method: "GET",
            url: CommonUtils.RootURL("Task/Project/GetProjectPriorities"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.PostTrackingUpdateDB = function () {
        var request = $http({
            method: "GET",
            url: CommonUtils.RootURL("Task/Project/PostTrackingUpdateDB"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    //-----Task -------
    this.CheckActionsProject = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Project/CheckActionsProject"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.CheckActionsTask = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/TaskItem/CheckActionsTask"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.DeleteTaskItem = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/TaskItem/DeleteTaskItem"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetTaskItem = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/TaskItem/GetTaskItem"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetNewTaskItem = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/TaskItem/GetNewTaskItem"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.UpdateStatusTaskItem = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/TaskItem/UpdateStatusTaskItem"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.SaveTaskItem = function (task, files) {
        var url = CommonUtils.RootURL("Task/TaskItem/SaveTaskItem");
        var defer = $q.defer();
        delete $http.defaults.headers.common['X-Requested-With'];
        $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var frm = objectToFormData(task);
                if (files != null) {
                    angular.forEach(files, function (value, index) {
                        frm.append("File[" + index + "]", value);
                    });
                }
                return frm;
            },
            data: task
        }).then(
            function (rs) {
                defer.resolve(rs.data);
            }, function (data, status, headers, config) {
                defer.reject();
            });

        return defer.promise;
    }
    this.SaveDraftTaskItem = function (task, files) {
        var url = CommonUtils.RootURL("Task/TaskItem/SaveDraftTaskItem");
        var defer = $q.defer();
        delete $http.defaults.headers.common['X-Requested-With'];
        $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var frm = objectToFormData(task);
                if (files != null) {
                    angular.forEach(files, function (value, index) {
                        frm.append("File[" + index + "]", value);
                    });
                }
                return frm;
            },
            data: task
        }).then(
            function (rs) {
                defer.resolve(rs.data);
            }, function (data, status, headers, config) {
                defer.reject();
            });

        return defer.promise;
    }
    this.GetTaskPriorities = function () {
        var request = $http({
            method: "GET",
            url: CommonUtils.RootURL("Task/TaskItem/GetTaskPriorities"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.SearchTaskAssign = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/TaskItem/SearchTaskAssign"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    //----------------
    //----- Task Assign -------
    this.GetTaskItemAssign = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/TaskItemAssign/GetTaskItemAssign"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetTaskItemAssignConfigPoint = function () {
        var request = $http({
            method: "GET",
            url: CommonUtils.RootURL("Task/TaskItemAssign/GetTaskItemAssignConfigPoint"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    //this.ProcessTaskItemAssign = function (data) {
    //    var request = $http({
    //        method: "POST",
    //        data: data,
    //        url: CommonUtils.RootURL("Task/TaskItemAssign/ProcessTaskItemAssign"),
    //        dataType: 'json',
    //        contentType: 'application/json; charset=utf-8'
    //    });
    //    return request;
    //}
    this.ProcessTaskItemAssign = function (task, files) {
        var url = CommonUtils.RootURL("Task/TaskItemAssign/ProcessTaskItemAssign");
        var defer = $q.defer();
        delete $http.defaults.headers.common['X-Requested-With'];
        $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var frm = objectToFormData(task);
                if (files != null) {
                    angular.forEach(files, function (value, index) {
                        frm.append("File[" + index + "]", value);
                    });
                }
                return frm;
            },
            data: task
        }).then(
            function (rs) {
                defer.resolve(rs.data);
            }, function (data, status, headers, config) {
                defer.reject();
            });

        return defer.promise;
    }
    //-------------------------
    // -----------------------
    this.ImportExcel = function (param, files) {
        var url = CommonUtils.RootURL("Task/Project/ImportExcel");
        var defer = $q.defer();
        delete $http.defaults.headers.common['X-Requested-With'];
        $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var frm = objectToFormData(param);
                if (files != null) {
                    angular.forEach(files, function (value, index) {
                        frm.append("File[" + index + "]", value);
                    });
                }
                return frm;
            },
            data: param
        }).then(
            function (rs) {
                defer.resolve(rs.data);
            }, function (data, status, headers, config) {
                defer.reject();
            });

        return defer.promise;
    }
    this.ImportMSProject = function (param, files) {
        var url = CommonUtils.RootURL("Task/Project/ImportMSProject");
        var defer = $q.defer();
        delete $http.defaults.headers.common['X-Requested-With'];
        $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': undefined },
            transformRequest: function (data) {
                var frm = objectToFormData(param);
                if (files != null) {
                    angular.forEach(files, function (value, index) {
                        frm.append("File[" + index + "]", value);
                    });
                }
                return frm;
            },
            data: param
        }).then(
            function (rs) {
                defer.resolve(rs.data);
            }, function (data, status, headers, config) {
                defer.reject();
            });

        return defer.promise;
    }
    // ------------------------
});