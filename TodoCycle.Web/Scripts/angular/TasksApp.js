'use strict';

var app = angular.module('app', []);

angular.module('app').controller('TasksController', ['$scope', '$http',
function ($scope, $http) {
    var self = this;

    $http({ method: 'GET', url: 'api/Task/GetAll' }).success(function (data, status, headers, config) {
        self.tasks = data;
    });
}]);