using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Abstract
{
    public interface IPackageService: IGenericService<Package>
    {
        IEnumerable<Package> PackageActiveList();
        IEnumerable<Package> ManagersPackages(int managerId);
        Package GetById(int packageId);
    }
}
