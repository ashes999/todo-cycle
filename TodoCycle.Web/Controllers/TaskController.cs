using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoCycle.Core;
using TodoCycle.SqlDatabase;
using TodoCycle.SqlDatabase.Repositories;

namespace TodoCycle.Web.Controllers
{
    public class TaskController : AbstractController
    {
        public TaskController(IRepository repository) : base(repository)
        {
        }

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }
    }
}
