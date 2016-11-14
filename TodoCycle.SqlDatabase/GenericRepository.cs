using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace TodoCycle.SqlDatabase
{
    public class GenericRepository
    {
        private string connectionString;

        public GenericRepository(ConnectionStringSettings connectionString)
        {
            this.connectionString = connectionString.ConnectionString;
        }

        public IEnumerable<T> GetAll<T>(object parameters = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = $"SELECT * FROM {typeof(T).Name}";
                return connection.Query<T>(sql, parameters);
            }
        }

        public void Insert<T>(T instance)
        {
            var tableName = typeof(T).Name;
            tableName = this.pluralize(tableName);

            var properties = GetProperties<T>();
            var values = GetValues<T>(properties);
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, string.Join(",", properties), values), instance);
            }
        }

        private string pluralize(string tableName)
        {
            var toReturn = tableName;

            if (!toReturn.EndsWith("s"))
            {
                toReturn += "s";
            }

            return toReturn;
        }

        public void Execute(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }

        private string GetValues<T>(IEnumerable<string> fields)
        {
            var type = typeof(T);
            var builder = new StringBuilder();
            foreach (var field in fields)
            {
                var value = "@" + field;
                builder.Append(value).Append(",");
            }

            var toReturn = builder.ToString();
            return toReturn.Substring(0, toReturn.LastIndexOf(',')); // Trim trailing comma
        }

        private IEnumerable<string> GetProperties<T>()
        {
            var type = typeof(T);
            return type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Select(f => f.Name).ToList();
        }
    }
}
