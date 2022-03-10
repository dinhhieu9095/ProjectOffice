//Màn hình quản lý biểu mẫu
app.controller("ReportManagementCtrl", function ($scope, $controller, $q, ReportManagementService) {
    $controller('BaseCtrl', { $scope: $scope });
    $scope.titleModal = "";
    $scope.filter = {
        keyword: '',
        pageIndex: 1,
        PageSize: 10
    };
    $scope.hasSubmit = false;
    $scope.report = {
        filterOrgChart: {
            getUrl: function (node) {
                if (urlParams.get('type') == 'user')
                    return CommonUtils.RootURL("Org/GetOrgUserChart");
                else
                    return CommonUtils.RootURL("Org/GetOrgUserChart");
            },
            modalId: '#org-chart-modal',
            treeId: '#org-user-chart-modal',
            submitId: '#org-chart-submit-id',
            selectNode: function () {
            },
            submitOrgChart: function (data) {
                $scope.report.getOrgUser(data);
            },
        },
        list: [],
        item: {},
        getPaging: function () {
            $scope.showLoading(null);
            ReportManagementService.getPaging($scope.filter).then(function (rs) {
                $scope.report.list = rs.data.Result;
                $scope.hideLoading(null);
            });
        },

        getById: function (id) {
            var filter = {
                Id: id
            }
            //if (id == '00000000-0000-0000-0000-000000000000') {
            //    $scope.titleModal = "Thêm mới tài liệu biểu mẫu";

            //}
            //else {
            $scope.titleModal = "Chỉnh sửa báo cáo";
            //}
            $scope.showLoading(null);
            ReportManagementService.getById(filter).then(function (rs) {
                $scope.report.item = rs.data;
                $(".select-user.select2-hidden-accessible").select2('destroy');
                var html = '';
                for (var i = 0; i < $scope.report.item.Users.length; i++) {
                    html = html + '<option value="' + $scope.report.item.Users[i].UserID + '" selected>' + $scope.report.item.Users[i].FullName + '</option>';
                }
                $('.select-user').html(html);
                $('.select-user').select2({
                    ajax: {
                        url: CommonUtils.RootURL("_apis/org/get-user-departments"),
                        dataType: 'json',
                        method: 'POST',
                        delay: 500,
                        data: function (params) {
                            if (params.term != undefined && params.term.length > 1) {
                                var query = {
                                    Keyword: params.term,
                                    Pagination: {
                                        Perpage: 10,
                                        Page: 1
                                    }
                                }
                                return query;
                            }
                        },
                        processResults: function (result) {
                            var items = [];
                            for (var i = 0; i < result.data.length; i++) {
                                var item = {
                                    id: result.data[i].userID,
                                    text: result.data[i].fullName + " - " + result.data[i].jobTitleName
                                }
                                items.push(item);
                            }
                            // Transforms the top-level key of the response object from 'items' to 'results'
                            return {
                                results: items
                            };
                        }
                    },
                    placeholder: 'Phân quyền theo user',
                    minimumInputLength: 2,

                });
                if ($scope.report.item.IsActive == true) {
                    $(".is-active").prop("checked", true);
                }
                else {
                    $(".is-active").prop("checked", false);
                }

                if ($scope.report.item.IsUser == true) {
                    $(".is-user").prop("checked", true);
                }
                else {
                    $(".is-user").prop("checked", false);
                }
                $scope.hideLoading(null);
                $("#AddEditModal").modal("show");
            });
        },

        deleteItem: function (id) {
            if ($scope.hasSubmit == false) {
                $scope.hasSubmit = true;
                $scope.showConfirm('Bạn có chắc muốn xóa dữ liệu này?', function (rs) {
                    if (rs) {
                        var filter = {
                            Id: id
                        }
                        ReportManagementService.deleteItem(filter).then(function (rs) {
                            toastr["success"]("Thành công");
                            $scope.hasSubmit = false;
                            $scope.report.init();
                        }, function (er) {
                            $scope.hasSubmit = false;
                        });
                    } else {
                        $scope.hasSubmit = false;
                    }
                });
            }

        },

        closePopUp: function () {

        },

        save: function () {
            var files = $("#file-upload")[0].files;
            if ($(".is-active").prop("checked") == true) {
                $scope.report.item.IsActive = true;
            }
            else
                $scope.report.item.IsActive = false;
             
            if ($(".is-user").prop("checked") == true) {
                $scope.report.item.IsUser = true;
            }
            else
                $scope.report.item.IsUser = false;

            if ($('.select-user').val() != null) {
                this.item.Permission = $('.select-user').val().join();
            }
            
            ReportManagementService.edit($scope.report.item, files).then(function (rs) {
                if (rs.data == true) {
                    toastr["success"]("Thành công");
                    $("#AddEditModal").modal("hide");
                    $scope.report.init();
                }
                else {
                    toastr["error"]("Có lỗi xảy ra trong quá tình xử lý");
                }
            })
        },

        getOrgUser: function (data) {
            if (data.filter(x => x.type == 'department').length > 0) {
                var departments = data.filter(x => x.type == 'department');
                $(".form-search-report .department-name-text").val(departments[0].text);
                $(".form-search-report .department-id-hidden").val(departments[0].id);
                $(".form-search-report .user-of-department-id-hidden").val('');
            }
            else {
                var users = data.filter(x => x.type == 'user');
                if (users.length > 0) {
                    $(".form-search-report .department-name-text").val(users[0].text + ' - ' + users[0].jobTitle);
                    $(".form-search-report .user-of-department-id-hidden").val(users[0].id);
                    $(".form-search-report .department-id-hidden").val('');
                }
            }
        },

        initOrgUser: function () {
            $scope.showLoading(null);
            vanPhongDienTuCommon.initJstreeCheckBox($scope.reportCommon.filterOrgChart);
            $scope.hideLoading();
        },

        init: function () {
            this.getPaging();
            //this.getById(null);
           
        }
    }

    $scope.report.init();
});