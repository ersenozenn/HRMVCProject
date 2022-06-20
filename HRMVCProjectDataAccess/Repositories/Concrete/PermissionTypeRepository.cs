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
    public class PermissionTypeRepository : GenericRepository<PermissionType> , IPermissionTypeRepository
    {
        private readonly HRMVCProjectDbContext db;

        public PermissionTypeRepository( HRMVCProjectDbContext db) : base(db)
        {
            this.db = db;
        }
        public PermissionType GetById(int id)
        {
            return db.Set<PermissionType>().FirstOrDefault(a => a.Id == id);
        }
    }
}
