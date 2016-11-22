using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using TodoCycle.Core;
using TodoCycle.SqlDatabase.Repositories;

namespace TodoCycle.Web.Controllers.Api
{
    public class TaskController : AbstractApiController
    {
        private TaskRepository taskRepository;

        public TaskController()
        {
            // TODO: dedupe with DI?
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            taskRepository = new TaskRepository(connectionString);
        }

        /// <summary>
        /// Gets all tasks (including scheduled tasks).
        /// </summary>
        [HttpGet]
        public IEnumerable<Task> GetAll()
        {
            var userId = this.GetCurrentUsersId();
            var tasks = this.repository.GetAll<Task>().Where(t => t.UserId == userId);
            var scheduledTasks = this.repository.GetAll<ScheduledTask>().Where(t => t.UserId == userId);
            return tasks.Concat(scheduledTasks);
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        [HttpPost]
        public Task Create(string taskName)
        {
            var task = new Task();
            task.Name =  Uri.UnescapeDataString(taskName);
            task.UserId = this.GetCurrentUsersId();
            task.Order = 0; // top of the list
            task.CreatedOnUtc = DateTime.UtcNow;

            this.taskRepository.Insert<Task>(task);

            return task;
        }

        /// <summary>
        /// Reorders a set of tasks (for scheduled tasks, use the ScheduledTask API instead).
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        [HttpPatch]
        public bool Reorder(IEnumerable<Task> tasks)
        {
            if (tasks == null || !tasks.Any())
            {
                return false;
            }

            var userId = this.GetCurrentUsersId();
            this.taskRepository.Reorder(tasks);
            return true; // success
        }
    }
}
