app.service("HomeService", function ($http, $q) {
    this.GetAdvanceFilter = function () {
        var request = $http({
            method: "GET",
            url: CommonUtils.RootURL("Task/Home/GetAdvanceFilter"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    };
    this.SaveAdvanceFilter = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Home/GetAdvanceFilter"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.DeleteAdvanceFilter = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Home/DeleteAdvanceFilter"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetAdvanceFilterPermission = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Home/GetAdvanceFilterPermission"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
    this.GetCategory = function (data) {
        var request = $http({
            method: "POST",
            data: data,
            url: CommonUtils.RootURL("Task/Home/GetCategory"),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8'
        });
        return request;
    }
});