using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Abstract
{
    public interface IEmployeeService : IGenericService<Employee>
    {
        public Employee GetByIdIncludePermission(int id);
        //Employee GetByEmailAndPassword(string email, string password);
        //Employee GetByEmailAndPassword(string email, string password);
        Employee GetById(int id);
        Employee GetByIdIncludeCosts(int id);
    }
}
