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
    public class CreditCardService : GenericService<CreditCard>,ICreditCardService
    {
        private readonly ICreditCardRepository creditCardRepository;

        public CreditCardService(ICreditCardRepository creditCardRepository) : base(creditCardRepository)
        {
            this.creditCardRepository = creditCardRepository;
        }
        public CreditCard GetById(int id)
        {
            return creditCardRepository.GetById(id);
        }
    }
}
