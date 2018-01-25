(function () {
    'use strict';

    var controllerId = 'shell';
    angular.module('app').controller(controllerId,
        ['$rootScope', 'common', 'config', '$scope', '$location', shell]);

    function shell($rootScope, common, config, $scope, $location) {
        var vm = this;
        vm.isAuthenticated = false;
        var logSuccess = common.logger.getLogFn(controllerId, 'success');
        var events = config.events;
        vm.busyMessage = 'Please wait ...';
        vm.isBusy = true;
        vm.spinnerOptions = {
            radius: 40,
            lines: 7,
            length: 0,
            width: 30,
            speed: 1.7,
            corners: 1.0,
            trail: 100,
            color: '#F58A00'
        };

        activate();

        function activate() {
            logSuccess('Demo loaded!', null, true);
            $scope.$on(config.events.onAuthenticated, function (event, data) {
                vm.isAuthenticated = data;
            });
            common.activateController([], controllerId);
        }

        function toggleSpinner(on) { vm.isBusy = on; }

        $rootScope.$on('$routeChangeStart',
            function (event, next, current) {
                if (common.getAuthData().token === null && next.$$route.title != 'Unauthrorized') {
                    $location.path('/unauthorized'); // back to login
                    return;
                } else {
                    toggleSpinner(true);
                }
            }
        );

        // use the built in event routechangeSuccess ( succussfully navigation completed) update the Page title

        $rootScope.$on('$routeChangeSuccess',
                function (event, current, previous) {
                    //handleRouteChangeError = false;
                    var title = config.docTitle + ' ' + (current.title || '');
                    $rootScope.title = title;
                });

        $rootScope.$on(events.controllerActivateSuccess,
            function (data) { toggleSpinner(false); }
        );

        $rootScope.$on(events.spinnerToggle,
            function (event, data) { toggleSpinner(data.show); }
        );
    };
})();