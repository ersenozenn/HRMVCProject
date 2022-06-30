using DataAccess.Repositories.Abstract;
using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectDataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Concrete
{
    public class CostService : GenericService<Cost>, ICostService
    {
        private readonly ICostRepositroy costRepository;

        public CostService(ICostRepositroy costRepositroy):base(costRepositroy)
        {
            this.costRepository = costRepositroy;   
        }

        public IEnumerable<Cost> GetAllByCompanyId(int companyId)
        {
            return costRepository.GetAllByCompanyId(companyId);
        }

        public Cost GetById(int Id)
        {
            return costRepository.GetById(Id);
        }

        public IEnumerable<Cost> GetPendingCosts(int companyId)
        {
            return costRepository.GetPendingCosts(companyId);
        }
    }
}
