app.service("AdminCategoryService", function ($http, $q, objectToFormData) {
    this.GetAdminCategories = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Admin/GetAdminCategories"),
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
            url: CommonUtils.RootURL("Task/Admin/MoveDataByTask"),
            dataType: 'json',
            async: true,
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.GetAdminCategory = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Admin/GetAdminCategory"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.SaveAdminCategory = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Admin/SaveAdminCategory"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.DeleteAdminCategory = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Admin/DeleteAdminCategory"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }

    //-----Task -------
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
            url: CommonUtils.RootURL("Task/Admin/GetTaskItem"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetNewTaskItem = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Admin/GetNewTaskItem"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.SaveTaskItem = function (task, files) {
        var url = CommonUtils.RootURL("Task/Admin/SaveTaskItem");
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
});