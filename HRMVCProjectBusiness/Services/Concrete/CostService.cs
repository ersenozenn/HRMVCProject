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
        private readonly ICostRepositroy costRepositroy;

        public CostService(ICostRepositroy costRepositroy):base(costRepositroy)
        {
            this.costRepositroy = costRepositroy;   
        }
       
    }
}
