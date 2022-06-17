using DataAccess.Repositories;
using HRMVCProjectDataAccess.Data;
using HRMVCProjectDataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Concrete
{
    public class CostTypeRepository : GenericRepository<CostType>, ICostTypeRepository
    {
        private readonly HRMVCProjectDbContext db;

        public CostTypeRepository(HRMVCProjectDbContext db) : base(db)
        {
            this.db = db;
        }
    }
}
