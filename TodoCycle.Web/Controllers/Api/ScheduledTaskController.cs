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
        private GenericRepository genericRepository;

        public ScheduledTaskController()
        {
            // TODO: dedupe with DI?
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            genericRepository = new GenericRepository(connectionString);
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

            // Tasks are already in their new order
            var userId = this.GetCurrentUsersId();
            this.genericRepository.UpdateAll(tasks);
            return true; // success
        }
    }
}
