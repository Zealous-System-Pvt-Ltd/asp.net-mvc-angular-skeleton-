(function () {
    'use strict';
    var serviceId = 'employeedataservice';
    angular.module('app').factory(serviceId, employeeDataSvc);
    employeeDataSvc.$inject = ['common','$http','config'];
    function employeeDataSvc(common,$http,config) {
        var $q = common.$q;
        var service = {
            getEmployeesPartial: getEmployeesPartial,
            getEmployeeById: getEmployeeById,
            saveEmployee:    saveEmployee,
            isEmailUnique :isEmailUnique
        };
        return service;

        function getEmployeesPartial(pageIndex, pageSize) {
            var deferred = $q.defer();
            var uri = config.remoteServiceName + 'odata/Employees?$select=EmployeeId,FirstName,LastName,Email,Designation/Name&$top=' + pageSize + '&$skip=' + (pageIndex * pageSize) + '&$inlinecount=allpages' + '&$expand=Designation';
            var authData = common.getAuthData();
            var authHeaderValue = "Bearer " + authData.token;
            var etagHeaderValue = angular.empty;
            if (common.etag !=angular.empty) {
                etagHeaderValue = common.etag;
            }
            $http.get(uri, { headers: { 'Authorization': authHeaderValue,'If-None-Match':etagHeaderValue } }).success(function (response, status, headers, httpconfig) {
                // alert(headers('Etag'));
                common.etag = headers('Etag');
               deferred.resolve(response);
            }).error(function (err, status, headers, httpconfig) {
                if (status === 500) {
                    deferred.reject(err['odata.error'].innererror.message);
                }
                deferred.resolve();

            });

            return deferred.promise;
        }

        function getEmployeeById(id) {
            var deferred = $q.defer();
            var uri = config.remoteServiceName + 'odata/Employees(' + id + ')';
            var authData = common.getAuthData();
            var authHeaderValue = "Bearer " + authData.token;
            $http.get(uri, { headers: { 'Authorization': authHeaderValue } }).success(function (response) {
               deferred.resolve(response);
            }).error(function (err) {
                deferred.reject(err['odata.error'].innererror.message);
            });

            return deferred.promise;
        }

        function insertEmployee(employee) {
            var deferred = $q.defer();
            var uri = config.remoteServiceName + 'odata/Employees';
            var authData = common.getAuthData();
            var authHeaderValue = "Bearer " + authData.token;
            $http.post(uri,employee, { headers: { 'Authorization': authHeaderValue } }).success(function (response) {
                deferred.resolve(response);
            }).error(function (err) {
                deferred.reject(err['odata.error'].innererror.message);
            });

            return deferred.promise;
        }

        function saveEmployee(employee,mode) {
            var deferred = $q.defer();
            var uri = config.remoteServiceName + 'odata/Employees';
            if (mode ==='put') {
                uri = uri + '(' + employee.EmployeeId + ')';
            }
            var authData = common.getAuthData();
            var authHeaderValue = "Bearer " + authData.token;
            $http({method:mode,url:uri,data:employee,headers: { 'Authorization': authHeaderValue } }).success(function (response) {
                deferred.resolve(response);
            }).error(function (err) {
                deferred.reject(err['odata.error'].innererror.message);
            });

            return deferred.promise;
        }

        function isEmailUnique(emailValue) {
            var emailObj = {
                email: emailValue
            };
            var deferred = $q.defer();
            var uri = config.remoteServiceName + 'odata/Employees(10)/IsEmailUnique'; // it requires some identifier to treat it as a single entity, otherwise odata assumes it to be a collection
            var authData = common.getAuthData();
            var authHeaderValue = "Bearer " + authData.token;
            $http.post(uri, emailObj, { headers: { 'Authorization': authHeaderValue } }).success(function (response) {
                if (response.value) {
                    deferred.reject(false);
                } else {
                    deferred.resolve(true);
                }
                
            }).error(function (err) {
                deferred.reject(err['odata.error'].innererror.message);
            });

            return deferred.promise;
        }

    }
})();