using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoCycle.DataAccess.Repositories
{
    public interface IGenericRepository
    {
        IEnumerable<T> Query<T>(string sql, object parameters = null) where T : class;
        void Save<T>(T obj) where T : class;
        T ExecuteScalar<T>(string sql, object parameters) where T : class;
    }
}
