using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace TodoCycle.DataAccess.Repositories
{
    // Dapper's pretty smart these days. I don't even have to write SQL any more.
    // We can devolve this into something domain-object specific if necessary.
    public class GenericRepository : IGenericRepository
    {
        private readonly string connectionString;

        public GenericRepository(ConnectionStringSettings connectionString)
        {
            this.connectionString = connectionString.ConnectionString;
        }

        public IEnumerable<T> Query<T>(string sql, object parameters)
        {
            if (parameters == null)
            {
                parameters = new { };
            }

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                return connection.Query<T>(sql, parameters);
            }
        }

        public T ExecuteScalar<T>(string sql, object parameters)
        {
            if (parameters == null)
            {
                parameters = new { };
            }

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                return (T)connection.ExecuteScalar<T>(sql, parameters);
            }
        }
    }
}
