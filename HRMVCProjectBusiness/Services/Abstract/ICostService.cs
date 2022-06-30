using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Abstract
{
    public interface ICostService:IGenericService<Cost>
    {
        IEnumerable<Cost> GetAllByCompanyId(int companyId);
        IEnumerable<Cost> GetPendingCosts(int companyId);
        Cost GetById(int Id);
    }
}
