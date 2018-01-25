(function () {
    'use strict';
    var controllerId = 'unauthrorize';
    angular.module('app').controller(controllerId, UnAuth);
    UnAuth.$inject = ['common'];
    function UnAuth(common) {
        var vm = this;
        activate();
        function activate() {
            common.activateController([], controllerId);
        }
    }
})();