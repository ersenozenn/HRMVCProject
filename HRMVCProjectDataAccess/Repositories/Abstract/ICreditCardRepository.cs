using DataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Abstract
{
    public interface ICreditCardRepository : IRepository<CreditCard>
    {
        public CreditCard GetById(int id);
    }
}
