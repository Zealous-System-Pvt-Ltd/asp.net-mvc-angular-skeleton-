(function () {
    'use strict';

    var app = angular.module('app');

    // Configure Toastr
    toastr.options.timeOut = 4000;
    toastr.options.positionClass = 'toast-bottom-right';

    // OData / Webapi EndPoint base url
    var remoteServiceName = 'http://localhost:4119/';

    var events = {
        controllerActivateSuccess: 'controller.activateSuccess',
        spinnerToggle: 'spinner.toggle',
        onUserClaimsSuccess: 'userclaims.success',
        onAuthenticated: 'onauthentication.success'
    };

    var dataserviceResponse = {
        success: false,
        error: false
    };

    var config = {
        appErrorPrefix: '[Error] ', //Configure the exceptionHandler decorator
        docTitle: 'Demo: ',
        events: events,
        dataserviceResponse: dataserviceResponse,
        remoteServiceName: remoteServiceName,
        version: '1.0.0'
    };

    app.value('config', config);

    app.config(['$logProvider', function ($logProvider) {
        // turn debugging off/on (no info or warn)
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
    }]);

    //#region Configure the common services via commonConfig
    app.config(['commonConfigProvider', function (cfg) {
        cfg.config.controllerActivateSuccessEvent = config.events.controllerActivateSuccess;
        cfg.config.spinnerToggleEvent = config.events.spinnerToggle;
    }]);
    //#endregion
})();