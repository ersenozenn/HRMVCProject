using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Abstract
{
    public interface ICompanyService : IGenericService<Company>
    {
        Company GetByIdIncludeEmployees(int id);
    }
}
