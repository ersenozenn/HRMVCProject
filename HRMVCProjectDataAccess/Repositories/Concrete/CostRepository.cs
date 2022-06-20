using DataAccess.Repositories;
using HRMVCProjectDataAccess.Data;
using HRMVCProjectDataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Concrete
{
    public class CostRepository : GenericRepository<Cost>, ICostRepositroy
    {
        private readonly HRMVCProjectDbContext db;

        public CostRepository(HRMVCProjectDbContext db) : base(db)
        {
            this.db = db;
        }

        //public IEnumerable<Cost> CostList(int id)
        //{
        //    return db.Costs.Include(a => a.Employees).Where(a => a.Id == id).ToList();
        //}

       
    }
}
