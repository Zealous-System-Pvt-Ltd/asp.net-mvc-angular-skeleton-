(function () {
    'use strict';
    var controllerId = 'home';
    angular.module('app').controller(controllerId, Home);
    Home.$inject = ['common', 'accountdataservice', '$route', 'routes', 'config'];

    function Home(common, accountdataservice, $route, routes, config) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Home';
        vm.userName = angular.empty;
        var claims = null;
        activate();

        function activate() {
            common.activateController([getClaims()], controllerId).then(function () {
                // claims have been accquired broadcast to sidebar controller so that the menu can be updated based on the user role
                common.$broadcast(config.events.onUserClaimsSuccess, claims.RolesList);
            });
        }

        function getClaims() {
            return accountdataservice.getClaims().then(function (data) {
                vm.userName = data.Name;

                claims = data;
                return data;
            });
        }
    }
})();