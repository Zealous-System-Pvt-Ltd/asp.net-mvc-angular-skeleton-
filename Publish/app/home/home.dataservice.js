(function () {
    'use strict';
    var serviceId = 'homedataservice';
    angular.module('app').factory(serviceId, homeDataService);
    homeDataService.$inject = ['$http', 'common'];

    function homeDataService($http, common) {
        var $q = common.$q;
    }
})();