(function () {
    'use strict';
    var serviceId = 'designationdataservice';
    angular.module('app').factory(serviceId, designationDataSvc);

    designationDataSvc.$inject = ['$http', 'config', 'common'];

    function designationDataSvc($http, config, common) {
        var $q = common.$q;
        var service = {
            getDesignations: getDesignations
        };
        return service;

        function getDesignations() {
            var deferred = $q.defer();
            var uri = config.remoteServiceName + 'odata/Designations' + '?$inlinecount=allpages';
            var authData = common.getAuthData();
            var authHeaderValue = "Bearer " + authData.token;
            $http.get(uri, { headers: { 'Authorization': authHeaderValue } }).success(function (response) {
                deferred.resolve(response);
            }).error(function (err) {
                deferred.reject(err['odata.error'].innererror.message);
            });

            return deferred.promise;
        }
    }
})();