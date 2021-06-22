app.controller("CalendarCtrl", function ($scope, $controller, $location, $q, $timeout, MainService) {
    $controller('BaseCtrl', { $scope: $scope });
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    $scope.calendar = {
        filter: {
            type: '',
            fromDate: '',
            toDate: '',
            user: ''
        },

        data: [],

        users: [],

        selectedId: '',

        breadCrumb: [],

        header: [],

        getData: function (item) {
            var parentId = '';
            if ($('#projectFilters').val() != "undefined" && $('#projectFilters').val() != null) {
                parentId = $('#projectFilters').val();
            }
            if (item != null) {
                parentId = item.Id;
            }
            this.getViewBreadCrumbWithParent(parentId);
            var data = JSON.stringify({
                parentId: parentId,
                filterId: urlParams.get('filterId'),
                folderId: urlParams.get('folderId'),
                view: 'kanban',//urlParams.get('view'),
                filter: { keyWord: "", CurrentPage: 1, PageSize: 20 }
            });
            $scope.showLoading();
            let promises = [MainService.GetDataByProject(data)];
            $q.all(promises).then(function (rs) {

                if (rs[0].data.status) {
                    $scope.calendar.convertData(rs[0].data.data.Result);
                }
                else {
                    toastr.error(rs[0].data.data.msg, 'Thông báo')
                }
                $scope.hideLoading();
            }, function (er) {
                toastr.error(er, 'Thông báo')
                $scope.hideLoading();
            });
        },

        convertData: function (data) {
            $scope.calendar.data = [];
            if (this.filter.type == 'week') {
                var startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').startOf('week');
                var endDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').endOf('week');
                for (var i = 0; i < data.length; i++) {
                    var item = angular.copy(data[i]);
                    var datas = $scope.calendar.parseData(item);
                    $scope.calendar.data = $.merge($scope.calendar.data, datas);
                }
            }
            else if (this.filter.type == 'day') {
                var startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY');
                var endDate = angular.copy(startDate);

                for (var i = 0; i < data.length; i++) {
                    var item = angular.copy(data[i]);
                    var datas = $scope.calendar.parseData(item);
                    $scope.calendar.data = $.merge($scope.calendar.data, datas);
                }
            }
            else if (this.filter.type == 'month') {

                // Lấy danh sách user
                $scope.calendar.users = [];
                var rowhaveUser = data.filter(e => e.UserFullName != null && e.UserFullName != '');
                for (var i = 0; i < rowhaveUser.length; i++) {
                    var users = rowhaveUser[i].UserFullName.split(';');
                    for (var j = 0; j < users.length; j++) {
                        users[j] = users[j].trim();
                    }
                    $scope.calendar.users = jQuery.unique($.merge($scope.calendar.users, users));
                }
                 
                var startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').startOf('month');
                for (var i = 0; i < data.length; i++) {
                    
                    var item = angular.copy(data[i]);
                    var datas = $scope.calendar.parseData(item);
                    $scope.calendar.data = $.merge($scope.calendar.data, datas);
                }
            }
        },

        parseData: function (data) {
            var fromDate = moment(data.FromDateText, 'DD/MM/YYYY');
            var toDate = moment(data.ToDateText, 'DD/MM/YYYY');
            var datas = [];
            if (this.filter.type == 'week' || this.filter.type == 'day') {
                var count = 7;
                if (this.filter.type == 'day') {
                    count = 1;
                }
                for (var j = 0; j < count; j++) {
                    let dayOfWeek = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY')._d;
                    dayOfWeek.setDate(dayOfWeek.getDate() + j);
                    if (dayOfWeek >= fromDate && dayOfWeek <= toDate) {
                        var item = angular.copy(data);
                        item["Day"] = dayOfWeek.toLocaleDateString('vi-vn');
                        if (item['UserFullName'] != null) {
                            var fullNames = item['UserFullName'].split(';');
                            var itemSub = [];
                            for (var k = 0; k < fullNames.length; k++) {
                                itemSub[k] = angular.copy(item);
                                itemSub[k]['FullName'] = fullNames[k].trim();
                            }
                            datas = $.merge(datas, itemSub);

                        }
                        else {
                            datas.push(item);
                        }
                    }
                }
            }
            else {
                var startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').startOf('month');
                var endDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').endOf('month');
                var count = endDate._d.getDate() - startDate._d.getDate();
                for (var i = 0; i <= count; i++) {
                    let dayOfWeek = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY')._d;
                    dayOfWeek.setDate(dayOfWeek.getDate() + i);
                    if (dayOfWeek >= fromDate && dayOfWeek <= toDate) {
                        var item = angular.copy(data);
                        item["Day"] = dayOfWeek.getDay();
                        item["Date"] = i + 1;
                        item["Week"] = $scope.calendar.getWeekOfMonth(dayOfWeek);
                        if ($scope.calendar.filter.user.trim() != '') {
                            if (item['UserFullName'] != null) {
                                var fullNames = item['UserFullName'].split(';').filter(x => x.trim() == $scope.calendar.filter.user.trim());
                                var itemSub = [];
                                for (var j = 0; j < fullNames.length; j++) {
                                    if (fullNames[j].trim() == $scope.calendar.filter.user.trim()) {
                                        itemSub[j] = angular.copy(item);
                                        itemSub[j]['FullName'] = fullNames[j].trim();
                                    }
                                }
                                datas = $.merge(datas, itemSub);

                            }
                        }
                        else {
                            datas.push(item);
                        }
                    }
                }
            }

            return datas;
        },

        clearForm: function () {
            this.header = [];
            this.data = [];
        },

        getHeader: function (value) {
            if ($scope.calendar.filter.type == 'week') {
                var dateArray = $scope.calendar.filter.fromDate.split('/');
                var day = parseInt(dateArray[0]);
                var month = parseInt(dateArray[1]);
                var year = parseInt(dateArray[2]);
                var sunday = new Date(year, month - 1, day).toLocaleDateString('vi-vn');
                var monday = new Date(year, month - 1, day + 1).toLocaleDateString('vi-vn');
                var tuesday = new Date(year, month - 1, day + 2).toLocaleDateString('vi-vn');
                var wednesday = new Date(year, month - 1, day + 3).toLocaleDateString('vi-vn');
                var thursday = new Date(year, month - 1, day + 4).toLocaleDateString('vi-vn');
                var friday = new Date(year, month - 1, day + 5).toLocaleDateString('vi-vn');
                var saturday = new Date(year, month - 1, day + 6).toLocaleDateString('vi-vn');
                $scope.calendar.header.push({ text: 'Chủ nhật <br/> ' + sunday, value: sunday });
                $scope.calendar.header.push({ text: 'Thứ 2 <br/> ' + monday, value: monday });
                $scope.calendar.header.push({ text: 'Thứ 3 <br/> ' + tuesday, value: tuesday });
                $scope.calendar.header.push({ text: 'Thứ 4 <br/> ' + wednesday, value: wednesday });
                $scope.calendar.header.push({ text: 'Thứ 5 <br/> ' + thursday, value: thursday });
                $scope.calendar.header.push({ text: 'Thứ 6 <br/> ' + friday, value: friday });
                $scope.calendar.header.push({ text: 'Thứ 7 <br/> ' + saturday, value: saturday });
            }
            else if ($scope.calendar.filter.type == 'day') {
                var startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY');
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = startDate.format('DD/MM/YYYY');
                switch (startDate._d.getDay()) {
                    case 0:
                        $scope.calendar.header.push({ text: 'Chủ nhật <br/> ' + startDate.format('DD/MM/YYYY'), value: startDate.format('DD/MM/YYYY') });
                        break;
                    case 1:
                        $scope.calendar.header.push({ text: 'Thứ 2 <br/> ' + startDate.format('DD/MM/YYYY'), value: startDate.format('DD/MM/YYYY') });
                        break;
                    case 2:
                        $scope.calendar.header.push({ text: 'Thứ <br/> 3 ' + startDate.format('DD/MM/YYYY'), value: startDate.format('DD/MM/YYYY') });
                        break;
                    case 3:
                        $scope.calendar.header.push({ text: 'Thứ <br/> 4 ' + startDate.format('DD/MM/YYYY'), value: startDate.format('DD/MM/YYYY') });
                        break;
                    case 4:
                        $scope.calendar.header.push({ text: 'Thứ <br/> 5 ' + startDate.format('DD/MM/YYYY'), value: startDate.format('DD/MM/YYYY') });
                        break;
                    case 5:
                        $scope.calendar.header.push({ text: 'Thứ <br/> 6 ' + startDate.format('DD/MM/YYYY'), value: startDate.format('DD/MM/YYYY') });
                        break;
                    case 6:
                        $scope.calendar.header.push({ text: 'Thứ <br/> 7 ' + startDate.format('DD/MM/YYYY'), value: startDate.format('DD/MM/YYYY') });
                }
            }
            else if ($scope.calendar.filter.type == 'month') {
                var dateArray = $scope.calendar.filter.fromDate.split('/');
                var day = parseInt(dateArray[0]);
                var month = parseInt(dateArray[1]);
                var year = parseInt(dateArray[2]);
                var sunday = new Date(year, month - 1, day).toLocaleDateString('vi-vn');
                var monday = new Date(year, month - 1, day + 1).toLocaleDateString('vi-vn');
                var tuesday = new Date(year, month - 1, day + 2).toLocaleDateString('vi-vn');
                var wednesday = new Date(year, month - 1, day + 3).toLocaleDateString('vi-vn');
                var thursday = new Date(year, month - 1, day + 4).toLocaleDateString('vi-vn');
                var friday = new Date(year, month - 1, day + 5).toLocaleDateString('vi-vn');
                var saturday = new Date(year, month - 1, day + 6).toLocaleDateString('vi-vn');
                $scope.calendar.header.push({ text: 'Chủ nhật', value: 0 });
                $scope.calendar.header.push({ text: 'Thứ 2', value: 1 });
                $scope.calendar.header.push({ text: 'Thứ 3', value: 2 });
                $scope.calendar.header.push({ text: 'Thứ 4', value: 3 });
                $scope.calendar.header.push({ text: 'Thứ 5', value: 4 });
                $scope.calendar.header.push({ text: 'Thứ 6', value: 5 });
                $scope.calendar.header.push({ text: 'Thứ 7', value: 6 });
            }
        },

        init: function () {
            this.clearForm();
            if (this.filter.type == '')
                this.filter.type = 'week';
            if (this.filter.fromDate == '') {
                if (this.filter.type == 'week') {
                    var startDate = moment().startOf('week');
                    $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                    $scope.calendar.filter.toDate = moment().endOf('week').format('DD/MM/YYYY');

                }
                else if (this.filter.type == 'day') {
                    var startDate = moment();
                    $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                    $scope.calendar.filter.toDate = startDate.format('DD/MM/YYYY');
                }
                else if (this.filter.type == 'month') {
                    var startDate = moment().startOf('month');
                    $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                    $scope.calendar.filter.toDate = moment().endOf('month').format('DD/MM/YYYY');
                }
            }

            this.getHeader();
            this.getData();
        },

        changeType: function (value) {
            this.filter.type = value;
            this.filter.user = '';
            if (this.filter.type == 'week') {
                var startDate = moment().startOf('week');
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = moment().endOf('week').format('DD/MM/YYYY');

            }
            else if (this.filter.type == 'day') {
                var startDate = moment();
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = startDate.format('DD/MM/YYYY');
            }
            else if (this.filter.type == 'month') {
                var startDate = moment().startOf('month');
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = moment().endOf('month').format('DD/MM/YYYY');
            }
            $scope.calendar.init();
        },

        nextTime: function () {
            if (this.filter.type == 'week') {
                let startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').add(1, 'week').startOf('week');
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = startDate.add(6, 'day').format('DD/MM/YYYY');
            }
            else if (this.filter.type == 'day') {
                let startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').add(1, 'day');
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = angular.copy($scope.calendar.filter.fromDate);
            }
            else if (this.filter.type == 'month') {
                let startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').add(1, 'month').startOf('month');
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = startDate.add(1, 'month').add(-1, 'day');
            }
            $scope.calendar.init();

        },

        previousTime: function () {
            if (this.filter.type == 'week') {
                var startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').add(-1, 'week').startOf('week');
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = startDate.add(6, 'day').format('DD/MM/YYYY');

            }
            else if (this.filter.type == 'day') {
                var startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').add(-1, 'day');
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = startDate.format('DD/MM/YYYY');
            }
            else if (this.filter.type == 'month') {
                let startDate = moment($scope.calendar.filter.fromDate, 'DD/MM/YYYY').add(-1, 'month').startOf('month');
                $scope.calendar.filter.fromDate = startDate.format('DD/MM/YYYY');
                $scope.calendar.filter.toDate = startDate.add(1, 'month').add(-1, 'day');
            }
            $scope.calendar.init();
        },

        getViewBreadCrumbWithParent: function (parentId) {
            var data = JSON.stringify({
                parentId: parentId
            });
            let promises = [MainService.GetViewBreadCrumbWithParent(data)];
            $q.all(promises).then(function (rs) {
                if (rs[0].data.status) {
                    $scope.calendar.breadCrumb = rs[0].data.data;

                }
                else {
                    toastr.error(rs[0].data.data.msg, 'Thông báo')
                }
            }, function (er) {
                toastr.error(er, 'Thông báo')
                $scope.hideLoading();
            });
        },

        selectBreadCrumb: function (id) {
            this.getData({ Id: id });
        },

        selectedItem: function (item) {
            this.calendar.selectedId = item.Id;
            $scope.onSelectItem(item);
        },

        getWeekOfMonth: function (val) {
            var month = val.getMonth()
                , year = val.getFullYear()
                , firstWeekday = new Date(year, month, 1).getDay()
                , lastDateOfMonth = new Date(year, month + 1, 0).getDate()
                , offsetDate = val.getDate() + firstWeekday - 1
                , index = 0
                , weeksInMonth = index + Math.ceil((lastDateOfMonth + firstWeekday - 7) / 7)
                , week = index + Math.floor(offsetDate / 7)
                ;
            if (week < 2 + index) return week;
            return week === weeksInMonth ? index + 5 : week;
        },

        changeUser: function () {
            this.init();
        }
    };

    if (urlParams.get('view') == 'calendar') {
        $scope.calendar.init();
    }
});