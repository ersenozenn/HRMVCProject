using DataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Abstract
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        public ICollection<Permission> GetAllById(int id);
        public bool AddPermission(Permission permission, Employee employee);
        public IEnumerable<Permission> GetAllByCompanyId(int companyId);
        public IEnumerable<Permission> GetPendingPermissions(int companyId);
    }
}
