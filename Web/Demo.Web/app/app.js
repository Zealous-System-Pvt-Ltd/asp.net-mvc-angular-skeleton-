(function () {
    'use strict';

    var app = angular.module('app', [
        // Angular modules
        'ngAnimate',        // animations
        'ngRoute',          // routing
        'ngSanitize',       // sanitizes html bindings (ex: sidebar.js)
        'ngMessages',       // showing error messages

        // Custom modules
        'common',           // common functions, logger, spinner
        'common.bootstrap', // bootstrap dialog wrapper functions
        'LocalStorageModule', // for local Storage

        // 3rd Party Modules
        'ui.bootstrap',     // ui-bootstrap (ex: carousel, pagination, dialog)
        'kendo.directives'  // for kendo ui
    ]);

    //app.config(function($httpProvider) {
    //        $httpProvider.interceptors.push('httpinterceptor');
    //    }
    //);

    // Handle routing errors and success events
    app.run(['$route', function ($route) {
        // Include $route to kick start the router.
    }]);
})();