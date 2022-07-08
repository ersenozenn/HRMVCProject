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
    public class PackageRepository: GenericRepository<Package>, IPackageRepository
    {
        private readonly HRMVCProjectDbContext db;

        public PackageRepository(HRMVCProjectDbContext db) : base(db)
        {
            this.db = db;
        }

        public IEnumerable<Package> PackageActiveList()
        {
            return db.Packages.Where(x => x.State == true);
        }
        
        public IEnumerable<Package> ManagersPackages(int managerId)
        {
            List<Package> packages = new List<Package>();

            foreach (Package item in db.Packages.Include(a => a.Employees))
            {
                foreach (Employee emp in item.Employees)
                {
                    if (emp.Id == managerId)
                    {
                        packages.Add(item);
                    }
                }
            }
            return packages;

        }

        public Package GetById(int packageId)
        {
            return db.Packages.Where(x => x.Id == packageId).SingleOrDefault();
        }
    }
}
