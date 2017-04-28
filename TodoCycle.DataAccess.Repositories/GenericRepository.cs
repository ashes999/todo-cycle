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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The type you want back. Also the type name of the table.</typeparam>
        /// <param name="query">A partial query; no "SELECT [blah]" plzkthx.</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string query, object parameters)
        {
            if (parameters == null)
            {
                parameters = new { };
            }

            // This is a pretty horrible (non-performant) way to avoid writing SQL in the web layer.
            // I don't care about performance for this project; this saves me writing lots of repositories.
            // eg. this allows: repo.Query<Task>("OwnerId = @ownerId", new { ownerId = ... })
            var tableName = typeof(T).Name;
            if (!tableName.EndsWith("s"))
            {
                tableName += "s";
            }

            var sql = $"SELECT * FROM {tableName} WHERE {query}";

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
