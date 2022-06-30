using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Abstract
{
    public interface IPermissionService : IGenericService<Permission>
    {
        public ICollection<Permission> GetAllById(int id);
        public bool AddPermission(Permission permission, Employee employee);
        IEnumerable<Permission> GetAllByCompanyId(int companyId);
        public IEnumerable<Permission> GetPendingPermissions(int companyId);
    }
}
