'use strict';

var app = angular.module('app', ['ui.sortable']);

angular.module('app').controller('TasksController', ['$scope', '$http',
function ($scope, $http) {
    var self = this;

    // Configure ui.sortable
    $scope.sortableOptions = {
        stop: function (e, ui) {
            console.log("You just re-ordered some stuff, eh? " + JSON.stringify(self.tasks));
        },
    };

    $http({ method: 'GET', url: 'api/Task/GetAll' }).success(function (data, status, headers, config) {
        self.tasks = data;
    });
}]);