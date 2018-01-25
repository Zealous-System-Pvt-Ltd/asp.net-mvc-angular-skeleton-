(function () {
    'use strict';
    var serviceId = 'httpinterceptor';
    angular.module('app').factory(serviceId, httpInterceptor);
    httpInterceptor.$inject = ['localStorageService', 'common', '$injector', '$location'];

    function httpInterceptor(localStorageService, common, $injector, $location) {
        var $q = common.$q;
        var service = {
            request: request,
            response: error
        }
        return service;

        function request(config) {
            config.headers = config.headers || {};
            var authData = localStorageService.get('authData');
            if (authData.token) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        function error(response) {
            if (response.status === 401) {
                var accountSvc = $injector.get('accountdataservice'); // get the instance of account data service
                accountSvc.logOut();
                $location.path('/'); // redirect to login
            }
            return $q.reject(error);
        }
    }
})();