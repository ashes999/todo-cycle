using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

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
        public IEnumerable<T> Query<T>(string query, object parameters) where T : class
        {
            if (parameters == null)
            {
                parameters = new { };
            }

            // This is a pretty horrible (non-performant) way to avoid writing SQL in the web layer.
            // I don't care about performance for this project; this saves me writing lots of repositories.
            // eg. this allows: repo.Query<Task>("OwnerId = @ownerId", new { ownerId = ... })
            var tableName = this.GetTableName(typeof(T));
            var sql = $"SELECT * FROM {tableName} WHERE {query}";

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                return connection.Query<T>(sql, parameters);
            }
        }

        public void Save<T>(T obj) where T : class
        {
            var tableName = this.GetTableName(obj.GetType());
            // Please don't use this to save users.
            var idProperty = obj.GetType().GetProperty("Id");

            if (idProperty == null)
            {
                throw new InvalidOperationException("Can't save something without an ID property.");
            }

            var id = idProperty.GetValue(obj);
            var operation = "INSERT";

            if (id is int && (int)id > 0)
            {
                operation = "UPDATE";
            } else if (id is string && !string.IsNullOrEmpty((string)id)) {
                operation = "UPDATE";
            }
            else if (id is Guid && (Guid)id != Guid.Empty )
            {
                operation = "UPDATE";
            }

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                if (operation == "INSERT")
                {
                    connection.Insert<T>(obj);
                }
                else if (operation == "UPDATE")
                {
                    connection.Update<T>(obj);
                }
                else
                {
                    throw new InvalidOperationException($"Not sure how to treat the operation '{operation}'");
                }
            }
        }

        /// <summary>
        /// Cop-out if you just need some quick-and-dirty SQL.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, object parameters) where T : class
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

        private string GetTableName(Type type)
        {
            var tableName = type.Name;
            if (!tableName.EndsWith("s"))
            {
                tableName += "s";
            }
            return tableName;
        }
    }
}
