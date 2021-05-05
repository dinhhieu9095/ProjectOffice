app.service("ReportManagementService", function ($http, $q, objectToFormData) {
     
    var service = {
        getPaging: function (filter) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/Report/GetPaging"),
                data: filter,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        getById: function (filter) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/Report/GetById"),
                data: filter,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        deleteItem: function (filter) {
            var request = $http({
                method: 'POST',
                url: CommonUtils.RootURL("Task/Report/Delete"),
                data: filter,
                dataType: 'json',
                contentType: 'application/json; charset=utf-8'
            });

            return request;
        },

        edit: function (model, files) {
            //delete $http.defaults.headers.common['X-Requested-With'];
            //$http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
            var request = $http({
                method: 'POST',
                async: true,
                url: CommonUtils.RootURL("Task/Report/Edit"),
                data: model,
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var frm = objectToFormData(model);
                    if (files != null) {
                        angular.forEach(files, function (value, index) {
                            frm.append("Files[" + index + "]", value);
                        });
                    }
                    return frm;
                }
            });

            return request;
        }
    }

    return service;
});