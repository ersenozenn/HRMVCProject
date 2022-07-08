using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectDataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System.Collections.Generic;

namespace HRMVCProjectBusiness.Services.Concrete
{
    public class PackageService : GenericService<Package>, IPackageService
    {
        private readonly IPackageRepository packageRepository;

        public PackageService(IPackageRepository packageRepository) : base(packageRepository)    
        {            
            this.packageRepository = packageRepository;
        }

        public Package GetById(int packageId)
        {
            return packageRepository.GetById(packageId);
        }

        public IEnumerable<Package> ManagersPackages(int managerId)
        {
            return this.packageRepository.ManagersPackages(managerId);
        }

        public IEnumerable<Package> PackageActiveList()
        {
            return packageRepository.PackageActiveList();
        }
    }
}
