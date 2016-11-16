'use strict';

var app = angular.module('app', ['ui.sortable']);

angular.module('app').controller('TasksController', ['$scope', '$http', 'orderByFilter',
function ($scope, $http, orderBy) {
    var self = this;

    // Configure ui.sortable
    $scope.sortableOptions = {
        stop: function (e, ui)
        {
            // User just re-ordered tasks. It may be inefficient, but we just save all the tasks' new orders.
            var tasks = self.tasks;
            for (var i = 0; i < tasks.length; i++)
            {
                tasks[i].Order = i;
            }

            $http({ method: 'PATCH', url: 'api/Task/Reorder', data: self.tasks });
        },
    };

    $http({ method: 'GET', url: 'api/Task/GetAll' }).success(function (data, status, headers, config) {
        // order by "order" attribute, false = non-reverse
        self.tasks = orderBy(data, 'order', false); 
    });
}]);