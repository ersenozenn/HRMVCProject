using DataAccess.Repositories;
using HRMVCProjectDataAccess.Data;
using HRMVCProjectDataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectEntities.Concrete.Enums;
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

        public IEnumerable<Cost> GetAllByCompanyId(int companyId)
        {
            List<Cost> costs = new List<Cost>();

            foreach (Cost item in db.Costs.Include(x=>x.Employees))
            {
                foreach (Employee item2 in item.Employees)
                {
                    if (item2.CompanyId == companyId)
                    {
                        costs.Add(item);
                    }
                }
            }
            return costs;
        }

        public Cost GetById(int Id)
        {
            return db.Costs.Where(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Cost> GetPendingCosts(int companyId)
        {
            List<Cost> costs = new List<Cost>();

            foreach (Cost item in db.Costs.Include(x => x.Employees))
            {
                foreach (Employee item2 in item.Employees)
                {
                    if (item2.CompanyId == companyId && item.ReplyState == ReplyState.Beklemede)
                    {
                        costs.Add(item);
                    }
                }
            }
            return costs;
        }
    }
}
