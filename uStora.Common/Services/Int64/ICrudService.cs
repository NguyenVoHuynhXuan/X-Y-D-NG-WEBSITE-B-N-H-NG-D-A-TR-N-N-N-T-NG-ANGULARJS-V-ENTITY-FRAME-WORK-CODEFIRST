using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uStora.Common.Services.Int64
{
    public interface ICrudService<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);
        void Delete(long id);
        void IsDeleted(long id);
        void SaveChanges();

    }
}
