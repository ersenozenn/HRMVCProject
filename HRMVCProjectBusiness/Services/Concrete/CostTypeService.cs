using DataAccess.Repositories.Abstract;
using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Concrete
{
    public class CostTypeService : GenericService<CostType>, ICostTypeService
    {
        public CostTypeService(IRepository<CostType> repository) : base(repository)
        {
        }
    }
}
