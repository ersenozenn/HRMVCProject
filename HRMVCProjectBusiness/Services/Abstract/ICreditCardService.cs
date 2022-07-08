using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Abstract
{
    public interface ICreditCardService : IGenericService<CreditCard>
    {
        public CreditCard GetById(int id);
    }
}
