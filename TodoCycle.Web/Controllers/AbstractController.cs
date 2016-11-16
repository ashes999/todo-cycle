using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoCycle.SqlDatabase;
using TodoCycle.SqlDatabase.Repositories;

namespace TodoCycle.Web.Controllers
{
    [Authorize]
    public abstract class AbstractController : Controller
    {
        protected IRepository repository;

        public AbstractController(IRepository repository)
        {
            this.repository = repository;
        }

        protected string GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException("User is not authenticated");
            }

            var userName = User.Identity.Name;
            var userId = repository.ExecuteScalar<string>("SELECT Id FROM AspNetUsers WHERE Email = @userName", new { userName = userName });
            return userId;
        }
    }
}