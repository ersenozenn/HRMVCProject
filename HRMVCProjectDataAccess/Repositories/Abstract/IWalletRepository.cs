using DataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Abstract
{
    public interface IWalletRepository :IRepository<Wallet>
    {
        public Wallet GetById(int id);
        public Wallet GetByIdIncludeEmployee(int id);
    }
}
