(function () {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('routes', getRoutes());

    // Configure the routes and route resolvers
    app.config(['$routeProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, routes) {
        routes.forEach(function (r) {
            $routeProvider.when(r.url, r.config);
        });
        $routeProvider.otherwise({ redirectTo: '/' });
    }

    // Define the routes this can get be retrieved from db
    function getRoutes() {
        return [
            {
                url: '/',
                config: {
                    templateUrl: 'app/account/login.html',
                    title: 'Login',
                    settings: {
                        nav: 100,
                        content: '<i class="fa fa-dashboard"></i> Logout'
                    },
                    roles: ['Admin', 'Customer']
                }
            },
            {
                url: '/admin',
                config: {
                    title: 'admin',
                    templateUrl: 'app/admin/admin.html',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-lock"></i> Admin'
                    },
                    roles: ['Admin']
                }
            },
            {
                url: '/home',
                config: {
                    title: 'home',
                    templateUrl: 'app/home/home.html',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-lock"></i> Home'
                    },
                    roles: ['Admin', 'Customer']
                }
            },

             {
                 url: '/employees',
                 config: {
                     templateUrl: 'app/employee/employees.html',
                     title: 'Employees',
                     settings: {
                         nav: 3,
                         content: '<i class="fa fa-dashboard"></i> Employees'
                     },
                     roles: ['Admin', 'Customer']
                 }
             },

              {
                  url: '/employee/:id',
                  config: {
                      templateUrl: 'app/employee/employeedetail.html',
                      title: 'Employee Detail',
                      settings: {}
                  }
              },
             {
                 url: '/unauthorized',
                 config: {
                     templateUrl: 'app/account/Unauthroized.html',
                     title: 'Unauthrorized',
                     settings: {
                         nav: 300,
                         content: '<i class="fa fa-dashboard"></i> Unauthorized'
                     },
                     roles: []
                 }
             },

        ];
    }
})();