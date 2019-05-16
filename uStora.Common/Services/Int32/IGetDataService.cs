using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uStora.Common.Services.Int32
{
    public interface IGetDataService<T> where T : class
    {
        T FindById(int id);
        IEnumerable<T> GetAll(string keyword = null);
    }
}
