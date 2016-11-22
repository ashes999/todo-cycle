using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using TodoCycle.Core;
using TodoCycle.SqlDatabase.Repositories;

namespace TodoCycle.Web.Controllers.Api
{
    public class ScheduledTaskController : AbstractApiController
    {
        private TaskRepository taskRepository;

        public ScheduledTaskController()
        {
            // TODO: dedupe with DI?
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            taskRepository = new TaskRepository(connectionString);
        }

        /// <summary>
        /// Reorders a list of scheduled tasks.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        [HttpPatch]
        public bool Reorder(IEnumerable<ScheduledTask> tasks)
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
