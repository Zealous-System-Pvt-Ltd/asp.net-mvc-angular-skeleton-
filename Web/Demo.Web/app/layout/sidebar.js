(function () {
    'use strict';

    var controllerId = 'sidebar';
    angular.module('app').controller(controllerId,
        ['$route', 'config', 'routes', '$scope', sidebar]);

    function sidebar($route, config, routes, $scope) {
        var vm = this;

        vm.isCurrent = isCurrent;

        activate();

        function activate() {
            onClaimsReceived();
        }

        function getNavRoutes(userRoles) {
            vm.navRoutes = routes.filter(function (r) {
                return r.config.title != 'Unauthrorized' && r.config.settings && r.config.settings.nav;
            }).sort(function (r1, r2) {
                return r1.config.settings.nav - r2.config.settings.nav;
            });
        }

        function onClaimsReceived() {
            $scope.$on(config.events.onUserClaimsSuccess, function (event, data) {
                getNavRoutes(data)
            });
        }
        function isCurrent(route) {
            if (!route.config.title || !$route.current || !$route.current.title) {
                return '';
            }
            var menuName = route.config.title;
            return $route.current.title.substr(0, menuName.length) === menuName ? 'current' : '';
        }
    };
})();