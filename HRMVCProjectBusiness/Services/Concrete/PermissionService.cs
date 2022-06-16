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
    public class PermissionService : GenericService<Permission>, IPermissionService
    {
        private readonly IPermissionRepository permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository) : base(permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        public bool AddPermission(Permission permission, Employee employee)
        {
            if (permission != null && employee != null)
            {
                return permissionRepository.AddPermission(permission, employee);
            }
            else
            {
                throw new Exception("Hatalı giriş!!");
            }
        }

        public ICollection<Permission> GetAllById(int id)
        {
            if (id >= 0)
            {
                return permissionRepository.GetAllById(id);
            }
            else
            {
                throw new Exception("Id hatalı");
            }
        }
    }
}
