using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace TodoCycle.SqlDatabase.Repositories
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>(object parameters = null) where T : class;

        void Insert<T>(T instance) where T : class;

        void Update<T>(T instance) where T : class;

        void UpdateAll<T>(IEnumerable<T> tasks) where T : class;

        void Execute(string sql, object parameters = null);

        T ExecuteScalar<T>(string sql, object parameters = null);
    }
}
