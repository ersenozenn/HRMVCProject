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
    public class WalletService : GenericService<Wallet> , IWalletService
    {
        private readonly IWalletRepository walletRepository;

        public WalletService(IWalletRepository walletRepository) : base(walletRepository)
        {
            this.walletRepository = walletRepository;
        }
        public Wallet GetById(int id)
        {
            return walletRepository.GetById(id);
        }
        public Wallet GetByIdIncludeEmployee(int id)
        {
            return walletRepository.GetByIdIncludeEmployee(id);
        }
    }
}
