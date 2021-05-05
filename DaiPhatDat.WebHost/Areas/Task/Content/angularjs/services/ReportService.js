app.service("ReportService", function ($http, $q, objectToFormData) {
     
    var service = {
        getData: function (filter, url) {
            var request = $http({
                method: 'POST',
                url: url,
                data: filter,
                //dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        }
    }

    return service;
});