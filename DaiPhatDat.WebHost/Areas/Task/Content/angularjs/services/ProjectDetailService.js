app.service("ProjectDetailService", function ($http, $q) {
    var service = {
        getData: function (id) {
            var request = $http({
                method: 'GET',
                url: CommonUtils.RootURL("Task/Project/ProjectDetail?Id=" + id),
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

        attachmentProject: function (id) {
            var request = $http({
                method: 'GET',
                url: CommonUtils.RootURL("Task/Project/AttachmentProject?Id=" + id),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        reportProject: function (id) {
            var request = $http({
                method: 'GET',
                url: CommonUtils.RootURL("Task/Project/ReportProject?Id=" + id),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },
        getHistory: function (filter) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/Project/Histories"),
                data: filter,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });
            return request;
        }
    }

    return service;
});