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
    public class PermissionTypeService : GenericService<PermissionType> , IPermissionTypeService
    {
        private readonly IPermissionTypeRepository permissionTypeRepository;

        public PermissionTypeService( IPermissionTypeRepository permissionTypeRepository ) : base(permissionTypeRepository)
        {
            this.permissionTypeRepository = permissionTypeRepository;
        }
    }
}
