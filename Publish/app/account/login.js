// always use IIFE (Immediate Invoke Function Expression)
(function () {
    'use strict';
    var controllerId = 'login';
    angular.module('app').controller(controllerId, Login);
    Login.$inject = ['accountdataservice', 'common', '$location', 'spinner', 'config']; //DI

    function Login(accountdataservice, common, $location, spinner, config) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'error');
        var vm = this;
        vm.loginData = { userName: "", password: "" };
        vm.loginButtonClick = loginClick;

        vm.errorMessage = null;
        activate();

        function activate() {
            accountdataservice.logOut();
            common.$broadcast(config.events.onAuthenticated, false);
            common.activateController([], controllerId);
        }

        function loginClick() {
            spinner.spinnerShow(); // show busy indicator
            accountdataservice.login(vm.loginData).then(function (response) {
                if (response.success) {
                    spinner.spinnerHide();
                    log('Login Successful');
                    common.$broadcast(config.events.onAuthenticated, true); // show side menu
                    $location.path('/home');
                }
            }, function (err) {
                spinner.spinnerHide();
                logError('Authentication failed');
                vm.errorMessage = 'Authentication failed';
            });
        }
    }
})();