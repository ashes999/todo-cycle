'use strict';

var app = angular.module('app', ['ui.sortable']);

angular.module('app').controller('TasksController', ['$scope', '$http', 'orderByFilter',
function ($scope, $http, orderBy)
{
    var self = this;
    var orderByFields = ['Order', '-CreatedOnUtc']; // Order ASC, then by created-on DESC

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

        $http.patch(url, tasks);
    }

    $scope.createNewTask = function()
    {
        var name = self.newTask;
        // TODO: replace with our good friend NotyJS

        $http.post("api/Task/Create?taskName=" + encodeURI(name))
            .then(function success(response) {
                // Push to front of array
                self.tasks.unshift(response.data);
                self.newTask = null;
            }, function error(response) {
                console.log("Failed, try again");
            });                
    }

    // Startup code

    $http.get('api/Task/GetAll')
        .then(function success(response)
    {
        var tasks = [];
        var scheduledTasks = [];

        // Returns both scheduled and non-scheduled tasks. Distinguishing factor is ScheduleJson field.
        for (var i = 0; i < response.data.length; i++)
        {
            var t = response.data[i];
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
        self.tasks = orderBy(tasks, orderByFields, false);
        self.scheduledTasks = orderBy(scheduledTasks, orderByFields, false);
    });
}]);