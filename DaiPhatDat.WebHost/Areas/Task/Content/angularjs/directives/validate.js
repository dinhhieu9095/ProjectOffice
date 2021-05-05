(function () {
    app.directive('bootstrapValidationDecorator', function () {
        return {
            scope: {
                bootstrapValidationDecorator: '@'
            },
            restrict: 'A',
            require: '^form',
            link: function (scope, el, attrs, formCtrl) {
                scope.form = formCtrl;
                if (scope.bootstrapValidationDecorator != undefined && scope.bootstrapValidationDecorator !== "") {
                    scope.fieldName = scope.bootstrapValidationDecorator;
                } else {
                    scope.fieldName = angular.element(el[0].querySelector("[name]")).attr('name');
                }

                scope.$watch(
                    function (scope) {
                        return (scope.form[scope.fieldName].$touched || scope.form.$submitted) && scope.form[scope.fieldName].$invalid;
                    },
                    function (newVal, oldVal) {
                        if (newVal !== oldVal) {
                            el.toggleClass('has-error', newVal);
                        }
                    }
                );
            }
        }
    });
})();