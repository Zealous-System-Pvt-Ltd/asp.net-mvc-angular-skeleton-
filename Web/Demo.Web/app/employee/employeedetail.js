(function () {
    'use strict';
    var controllerId = 'employeedetail';
    angular.module('app').controller(controllerId, EmployeeDetail);
    EmployeeDetail.$inject = ['common', 'employeedataservice', '$routeParams', 'designationdataservice', '$timeout', '$scope', '$location'];

    function EmployeeDetail(common, employeedataservice, $routeParams, designationdataservice, $timeout, $scope, $location) {
        var vm = this;
        vm.save = save;
        vm.employee = undefined;
        vm.designations = [];
        vm.opened = false;
        vm.title = angular.empty;
        vm.goBack = goBack;
        vm.douniqueEmailValidation = false;
        vm.dateOptions = {
            'year-format': "'yy'",
            'starting-day': 1
        };
        vm.open = open;
        var logError = common.logger.getLogFn(controllerId, 'error');
        var log = common.logger.getLogFn(controllerId);
        var mode = angular.empty;
        activate();
        function activate() {
            common.activateController([getDesignations(), getEmployeeById()], controllerId).then(function () {
                log('Data successfully loaded');
            });
        }

        function getEmployeeById() {
            var val = $routeParams.id;
            if (val === 'new') {
                vm.douniqueEmailValidation = true;
                vm.title = "Add new Employee";
                mode = 'post';
                return vm.employee = {}
            }
            else {
                return employeedataservice.getEmployeeById(val).then(function (data) {
                    vm.douniqueEmailValidation = false;
                    vm.employee = data;
                    vm.title = "Edit " + vm.employee.FirstName + ' ' + vm.employee.LastName;
                    mode = 'put';
                }, function (err) {
                    logError(err);
                });
            }
        }

        function getDesignations() {
            return designationdataservice.getDesignations().then(function (data) {
                vm.designations = data.value;
            }, function (err) {
                logError(err);
            });
        }
        function save() {
            if ($scope.empForm.$valid) {
                employeedataservice.saveEmployee(vm.employee, mode).then(function (data) {
                    log('Employee Saved successfully');
                }, function (err) {
                    logError(err);
                });
            }
        }

        function goBack() {
            $location.path('/employees');
        }

        function open() {
            $timeout(function () {
                vm.opened = true;
            });
        }
    }
})();