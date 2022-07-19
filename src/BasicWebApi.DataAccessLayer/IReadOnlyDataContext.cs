using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebApi.DataAccessLayer
{
    public interface IReadOnlyDataContext
    {
        IQueryable<T> GetData<T>(bool trackingChanges = false, bool ignoreQueryFilters = false) where T : class;
    }
}
