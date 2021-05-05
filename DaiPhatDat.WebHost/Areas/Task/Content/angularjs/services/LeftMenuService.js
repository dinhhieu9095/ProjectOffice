app.service("LeftMenuService", function ($http, $q) {
    var service = {
        getData: function (filter) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/Main/GetAdvanceFilterTree?parentID=" + filter.parentID + "&keySearch=" + filter.keySearch),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        getNavigationLeftFilter: function () {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/Main/NavigationLeftFilter"),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        getById: function (id) {
            var request = $http({
                method: 'GET',
                url: CommonUtils.RootURL("Task/ProjectFilterParam/GetAdvancedSearch?id=" + id),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        SaveAdvancedSearch: function (data) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/ProjectFilterParam/SaveAdvancedSearch"),
                data: data,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        deleteAdvancedSearch: function (id) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/ProjectFilterParam/Delete?Id=" + id),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        getDataByProject: function (data) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/Home/getDataByProject"),
                dataType: 'json',
                data: data,
                contentType: 'application/json; charset=utf-8'
            });
            return request;
        }
    }

    return service;
});