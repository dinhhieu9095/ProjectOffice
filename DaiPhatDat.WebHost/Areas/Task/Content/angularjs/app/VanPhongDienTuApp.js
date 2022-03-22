var app;
(function () {
    app = angular.module("vanphongdientuapp"
        , ['ngSanitize'
            , 'toastr'
            , 'ui.bootstrap'
            , 'ngFileUpload'
            , 'ui.date'
            , 'object-to-form-data',
        'dcbImgFallback',
            'ui.select2',
        'scrollable-table',
     'localytics.directives',
        'bw.paging',
        'rw.moneymask',
            //'ui.mask',
            'ckeditor',
            'angular.filter',
            'treeGrid'
        ]);
    app.config(['$compileProvider', function ($compileProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|javascript):/);
    }]);

    app.filter('propsFilter', function () {
        return function (items, props) {
            var out = [];

            if (angular.isArray(items)) {
                var keys = Object.keys(props);

                items.forEach(function (item) {
                    var itemMatches = false;

                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }

            return out;
        };
    });
    // Config http Interceptor
    app.directive('datePicker', function ($timeout) {
        var linker = function (scope, element, attr) {
            $(element).datepicker({
                format: 'dd/mm/yyyy',
                orientation: "bottom auto"
            }).on('changeDate', function (date) {
                scope.ngModel = $(element).val();
                $(element).trigger('change');
                $('.datepicker').hide();
                scope.$apply();
            });
        };

        return {
            restrict: 'A',
            scope: {
                ngModel: '='
            },
            link: linker
        };
    });
    app.directive('datePickerEvict', function ($timeout) {
        var linker = function (scope, element, attr) {
            $(element).datepicker({
                format: 'dd/mm/yyyy',
            }).on('changeDate', function (date) {
                $(element).trigger('change');
                $('.datepicker').hide();
                scope.$apply();
            });
        };

        return {
            restrict: 'A',
            scope: {
                ngModel: '='
            },
            link: linker
        };
    });
    app.directive('myEnter', function () {
        return function (scope, element, attrs) {
            console.log('test');
            element.bind("keydown", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.myEnter);
                    });

                    event.preventDefault();
                }
            });
        };
    });
    app.directive('ngBlur', function () {
        return function (scope, element, attrs) {
            element.bind("blur", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.myEnter);
                    });

                    event.preventDefault();
                }
            });
        };
    });

    app.directive('dynamicModel', ['$compile', '$parse', function ($compile, $parse) {
        return {
            restrict: 'A',
            terminal: true,
            priority: 100000,
            link: function (scope, elem) {
                var name = $parse(elem.attr('dynamic-model'))(scope);
                elem.removeAttr('dynamic-model');
                elem.attr('ng-model', name);
                $compile(elem)(scope);
            }
        };
    }]);

    app.directive('format', ['$filter', function ($filter) {
        return {
            require: '?ngModel',
            link: function (scope, elem, attrs, ctrl) {
                if (!ctrl) return;

                ctrl.$formatters.unshift(function (a) {
                    return $filter(attrs.format)(ctrl.$modelValue)
                });

                elem.on('blur', function (event) {
                    var plainNumber = elem.val().replace(/[^\d|\-+|\.+]/g, '');
                    ctrl.$setViewValue(plainNumber)
                    //elem.val(plainNumber);
                    elem.val($filter(attrs.format)(plainNumber));
                });
            }
        };
    }]);

    app.directive('changeOnBlur', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, elm, attrs, ngModelCtrl) {
                if (attrs.type === 'radio' || attrs.type === 'checkbox')
                    return;

                var expressionToCall = attrs.changeOnBlur;

                var oldValue = null;
                elm.bind('focus', function () {
                    scope.$apply(function () {
                        oldValue = elm.val();
                        console.log(oldValue);
                    });
                })
                elm.bind('blur', function () {
                    scope.$apply(function () {
                        var newValue = elm.val();
                        console.log(newValue);
                        if (newValue !== oldValue) {
                            scope.$eval(expressionToCall);
                        }
                        //alert('changed ' + oldValue);
                    });
                });
            }
        };
    });
    app.directive('format', ['$filter', function ($filter) {
        return {
            require: '?ngModel',
            link: function (scope, elem, attrs, ctrl) {
                if (!ctrl) return;

                ctrl.$formatters.unshift(function (a) {
                    return $filter(attrs.format)(ctrl.$modelValue)
                });

                elem.on('blur', function (event) {
                    var plainNumber = elem.val().replace(/[^\d|\-+|\.+]/g, '');
                    ctrl.$setViewValue(plainNumber)
                    //elem.val(plainNumber);
                    elem.val($filter(attrs.format)(plainNumber));
                });
            }
        };
    }]);
    app.filter('htmlSafe', [
        '$sce', function ($sce) {
            return $sce.trustAsHtml;
        }
    ]);
    app.config(['$qProvider', function ($qProvider) {
        $qProvider.errorOnUnhandledRejections(false);
    }]);
})();