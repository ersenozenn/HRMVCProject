using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Abstract
{
    public interface IWalletService : IGenericService<Wallet>
    {
        public Wallet GetById(int id);
        public Wallet GetByIdIncludeEmployee(int id);
    }
}
