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

        [HttpGet]
        public IEnumerable<Task> GetAll()
        {
            var userId = this.GetCurrentUser();
            var tasks = this.repository.GetAll<Task>().Where(t => t.UserId == userId);
            return tasks;
        }

        [HttpPatch]
        public bool Reorder(IEnumerable<Task> tasks)
        {
            if (tasks == null || !tasks.Any())
            {
                return false;
            }

            var userId = this.GetCurrentUser();
            this.taskRepository.Reorder(tasks);
            return true; // success
        }
    }
}
