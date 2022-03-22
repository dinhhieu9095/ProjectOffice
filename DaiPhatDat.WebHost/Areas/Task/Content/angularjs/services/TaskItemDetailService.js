app.service("TaskItemDetailService", function ($http, $q) {
    var service = {
        getData: function (id) {
            var request = $http({
                method: 'GET',
                url: CommonUtils.RootURL("Task/TaskItem/TaskItemDetail?Id=" + id),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        userInProject: function (id) {
            var request = $http({
                method: 'GET',
                url: CommonUtils.RootURL("Task/Project/UserInProject?Id=" + id),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        getAttachment: function (taskItemId, projectId) {
            var request = $http({
                method: 'GET',
                url: CommonUtils.RootURL("Task/TaskItem/Attachments?projectId=" + projectId + "&taskItemId=" + taskItemId),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        getHistory: function (filter) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/TaskItem/Histories"),
                data: filter,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },


        reportTask: function (id) {
            var request = $http({
                method: 'GET',
                url: CommonUtils.RootURL("Task/TaskItem/ReportTask?TaskItemId=" + id),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        returnTask: function (id) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/TaskItem/ReturnTaskItemAssign?Id=" + id),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        userInTask: function (id) {
            var request = $http({
                method: 'GET',
                url: CommonUtils.RootURL("Task/TaskItem/UserReportInTask?TaskItemId=" + id),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        getComments: function (filter) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/TaskItem/getComments"),
                data: filter,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },
        createComment: function (data) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/TaskItem/createComment"),
                data: data,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },
        updateComment: function (data) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/TaskItem/updateComment"),
                data: data,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });
            return request;
        }
    }

    return service;
});