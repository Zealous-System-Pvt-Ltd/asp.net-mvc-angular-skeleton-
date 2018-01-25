(function () {
    'use strict';
    var serviceId = 'accountdataservice';
    angular.module('app').factory(serviceId, accountDataService);
    accountDataService.$inject = ['common', '$http', 'config', 'localStorageService'];
    function accountDataService(common, $http, config, localStorageService) {
        var $q = common.$q;
        var service = {
            login: login,
            createUser: createUser,
            getClaims: getClaims,
            logOut: logOut
        }
        return service;

        //#region internal functions
        function login(loginData) {
            var svcResponse = config.dataserviceResponse;
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
            var deferred = $q.defer();
            $http.post(config.remoteServiceName + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                localStorageService.set('authData', { token: response.access_token, username: loginData.userName });

                svcResponse.success = true;
                svcResponse.error = false;
                deferred.resolve(svcResponse);
            }).error(function (error, status) {
                svcResponse.success = false;
                svcResponse.error = true;
                deferred.reject(error['odata.error'].innererror.message);
            });
            return deferred.promise;
        }

        function createUser() {
            //TODO implement it
        }

        function getClaims() {
            var deferred = $q.defer();

            // check whether it exists in localStorage if not then only shoot http request
            var claimsData = localStorageService.get('claims');
            if (claimsData) {
                deferred.resolve(claimsData);
            }
            else {
                var authData = common.getAuthData();
                var authHeaderValue = "Bearer " + authData.token;
                $http.get(config.remoteServiceName + 'security/claims?userName=' + authData.username, { headers: { 'Authorization': authHeaderValue } }).success(function (response) {
                    localStorageService.set('claims', response);
                    deferred.resolve(response);
                }).error(function (error) {
                    deferred.reject(error);
                });
            }
            return deferred.promise;
        }

        function logOut() {
            localStorageService.set('authData', { token: null, username: null });
            localStorageService.set('claims', null);
        }

        //#endregion
    }
})();