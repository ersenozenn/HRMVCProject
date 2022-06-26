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
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly HRMVCProjectDbContext db;

        public CompanyRepository(HRMVCProjectDbContext db) : base(db)
        {
            this.db = db;
        }

        public Company GetByIdWithEmployees(int id)
        {
            return db.Companies.Include(x => x.Employees).FirstOrDefault(a => a.Id == id);
        }
    }
}
