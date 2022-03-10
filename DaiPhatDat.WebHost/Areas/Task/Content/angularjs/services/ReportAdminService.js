app.service("ReportAdminService", function ($http) {
    var service = {
        getReportAdmin: function (data) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/Report/GetReportAdmin"),
                dataType: 'json',
                data: data,
                contentType: 'application/json; charset=utf-8'
            });
            return request;
        }
    };
    return service;
});