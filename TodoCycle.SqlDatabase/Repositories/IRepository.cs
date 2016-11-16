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
        IEnumerable<T> GetAll<T>(object parameters = null);

        void Insert<T>(T instance);

        void Execute(string sql, object parameters = null);

        T ExecuteScalar<T>(string sql, object parameters = null);
    }
}
