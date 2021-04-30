app.service("SettingsService", function ($http, $q, objectToFormData) {
     
    var service = {
        getPaging: function () {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/SystemManagement/GetByKeys"),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },
         
        saveSettings: function (data) {
            //delete $http.defaults.headers.common['X-Requested-With'];
            //$http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/SystemManagement/SaveSettings"),
                data: data,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        }
    }

    return service;
});