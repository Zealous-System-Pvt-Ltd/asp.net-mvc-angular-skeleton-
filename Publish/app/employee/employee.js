(function () {
    'use strict';
    var controllerId = 'employee';
    angular.module('app').controller(controllerId, Employee);

    Employee.$inject = ['common', 'employeedataservice', 'bootstrap.dialog'];
    function Employee(common, employeedataservice, bsDialog) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Employees';
        vm.mainGridOptions = {};
        vm.pageSize = 2;
        vm.currentPage = 1;
        vm.employees = [];
        vm.orderBy = 'FirstName';
        vm.reverse = false;
        vm.setOrder = setOrder;
        vm.totalRecords = 0;
        vm.pageChanged = pageChanged;
        vm.deleteEmployee = deleteEmployee;

        activate();

        // #region Controller Activate
        function activate() {
            common.activateController([getEmployeesPartial()], controllerId)
                .then(function () {
                    log('Employees data Loaded');
                });
        }
        // #endregion

        // #region Controller Promises
        function getEmployeesPartial() {
            return employeedataservice.getEmployeesPartial(vm.currentPage - 1, vm.pageSize).then(function (data) {
                vm.employees = data.value;
                vm.totalRecords = data['odata.count'];
            });
        }

        // #endregion

        // #region vm methods
        function setOrder(orderBy) {
            if (orderBy === vm.orderBy) {
                vm.reverse = !vm.reverse;
            }
            vm.orderBy = orderBy; // set the new sortOrder
        }

        function pageChanged(page) {
            vm.currentPage = page;
            getEmployeesPartial();
        }

        function deleteEmployee(employeetobeDeleted) {
            return bsDialog.deleteDialog(employeetobeDeleted.FirstName + ' ' + employeetobeDeleted.LastName).then(confirmDelete);
            function confirmDelete() {
                alert('deleted');
            }
        }
        // #endregion
    }
})();