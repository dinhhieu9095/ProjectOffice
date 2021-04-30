app.directive('icheck', function ($timeout, $parse) {
    return {
        require: 'ngModel',
        restrict: 'A',
        scope: {
            ngModel: "="
        },
        link: function ($scope, element, $attrs) {
            console.log(element);
            return $timeout(function () {
                var value;
                value = $attrs['value'];
                console.log(value);

                $scope.$watch($(element), function (newValue) {
                    $(element).iCheck('update');
                })
                
                return $(element).on('ifChanged', function (event) {
                    console.log('yes');
                    if ($(element).attr('type') === 'checkbox' && $attrs['ngModel']) {
                        $scope.$apply(function () {
                            console.log(event.target.checked);
                            return $scope.ngModel = event.target.checked;
                        });
                    }
                    if ($(element).attr('type') === 'radio' && $attrs['ngModel']) {
                        console.log(value);
                        return $scope.$apply(function () {
                            return $scope.ngModel = value;
                        });
                    }
                });
            });
        }
    };
});