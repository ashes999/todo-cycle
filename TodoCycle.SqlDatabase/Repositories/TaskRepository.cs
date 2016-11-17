using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TodoCycle.Core;
using Dapper;

namespace TodoCycle.SqlDatabase.Repositories
{
    public class TaskRepository : GenericRepository
    {
        public TaskRepository(ConnectionStringSettings connectionString) : base(connectionString)
        {
        }

        // Task or ScheduledTask
        public void Reorder(IEnumerable<Task> tasks)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var tableName = TableNameFor(tasks.First());

                foreach (var task in tasks)
                {
                    connection.Execute($"UPDATE {tableName} SET [Order] = @order WHERE Id = @id", new { order = task.Order, id = task.Id });
                }
            }
        }
    }
}
