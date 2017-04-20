using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoCycle.DataAccess.Repositories
{
    public interface IGenericRepository
    {
        IEnumerable<T> Query<T>(string sql, object parameters);
    }
}
