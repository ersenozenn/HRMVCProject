using DataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Abstract
{
    public interface IPermissionTypeRepository : IRepository<PermissionType>
    {
        public PermissionType GetById(int id);
    }
}
