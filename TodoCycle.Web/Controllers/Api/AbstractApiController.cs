using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using TodoCycle.SqlDatabase;

namespace TodoCycle.Web.Controllers
{
    [Authorize]
    public abstract class AbstractApiController : ApiController
    {
        protected GenericRepository repository;

        public AbstractApiController()
        {
            // TODO: dedupe with DI?
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            repository = new GenericRepository(connectionString);
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