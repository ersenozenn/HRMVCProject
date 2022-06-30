using DataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Abstract
{
    public interface ICostRepositroy:IRepository<Cost>
    {
        IEnumerable<Cost> GetAllByCompanyId(int companyId);
        public IEnumerable<Cost> GetPendingCosts(int companyId);
        public Cost GetById(int Id);  
    }
}
