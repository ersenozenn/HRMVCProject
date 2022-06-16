using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Abstract
{
    public interface IGenericService<T> where T : class
    {
        IEnumerable<T> GetAll();
        //T GetById(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
