'use strict';

var app = angular.module('app', ['ui.sortable']);

angular.module('app').controller('TasksController', ['$scope', '$http', 'orderByFilter',
function ($scope, $http, orderBy)
{
    var self = this;

    // Configure ui.sortable
    $scope.sortableOptions = {
        stop: function (e, ui)
        {
            // User just re-ordered tasks. It may be inefficient, but we just save all the tasks' new orders.
            $scope.orderAndPatch(self.tasks, "api/Task/Reorder");            
        },
    };

    $scope.scheduledSortableOptions = {
        stop: function (e, ui)
        {
            // User just re-ordered tasks. It may be inefficient, but we just save all the tasks' new orders.
            $scope.orderAndPatch(self.scheduledTasks, "api/ScheduledTask/Reorder");
        },
    };

    $scope.orderAndPatch = function(tasks, url)
    {
        // We have tasks in an array. Use their indicies as the order.
        for (var i = 0; i < tasks.length; i++)
        {
            tasks[i].Order = i;
        }

        $http({ method: 'PATCH', url: url, data: tasks });
    }

    $scope.createNewTask = function()
    {
        var name = self.newTask;
        console.log("Created task: " + name);
        self.newTask = null;
    }

    // Startup code

    $http({ method: 'GET', url: 'api/Task/GetAll' })
        .success(function (data, status, headers, config)
    {
        var tasks = [];
        var scheduledTasks = [];

        // Returns both scheduled and non-scheduled tasks. Distinguishing factor is ScheduleJson field.
        for (var i = 0; i < data.length; i++)
        {
            var t = data[i];
            if (t.ScheduleJson != null)
            {
                scheduledTasks.push(t);
            }
            else
            {
                tasks.push(t);
            }
        }        

        // order by "Order" attribute, false = non-reverse
        self.tasks = orderBy(tasks, 'Order', false);
        self.scheduledTasks = orderBy(scheduledTasks, 'Order', false);
    });
}]);