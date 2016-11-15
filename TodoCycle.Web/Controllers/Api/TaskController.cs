using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoCycle.Core;
using TodoCycle.SqlDatabase;
using TodoCycle.Web.Helpers;

namespace TodoCycle.Web.Controllers.Api
{
    public class TaskController : AbstractApiController
    {
        public IEnumerable<Task> GetAll()
        {
            var userId = this.GetCurrentUser();
            var tasks = this.repository.GetAll<Task>().Where(t => t.UserId == userId);
            return tasks;
        }
    }
}
