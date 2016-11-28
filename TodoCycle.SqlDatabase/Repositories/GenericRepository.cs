using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using TodoCycle.Core;

namespace TodoCycle.SqlDatabase.Repositories
{
    public class GenericRepository : IRepository
    {
        protected string connectionString;

        public GenericRepository(ConnectionStringSettings connectionString)
        {
            this.connectionString = connectionString.ConnectionString;
        }

        public IEnumerable<T> GetAll<T>(object parameters = null) where T : class
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.GetAll<T>();
            }
        }

        public void Insert<T>(T instance) where T : class
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Insert(instance);
            }
        }

        public void Update<T>(T instance) where T : class
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Update(instance);
            }
        }

        public void UpdateAll<T>(IEnumerable<T> tasks) where T : class
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Update(tasks);
            }
        }

        public T ExecuteScalar<T>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.ExecuteScalar<T>(sql, parameters);
            }
        }

        public void Execute(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }

    }
}
