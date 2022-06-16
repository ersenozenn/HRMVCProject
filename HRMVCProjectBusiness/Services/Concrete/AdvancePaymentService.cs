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
    public class AdvancePaymentService : GenericService<AdvancePayment>, IAdvancePaymentService
    {
        private readonly IAdvancePaymentRepository advancePaymentRepository;

        public AdvancePaymentService(IAdvancePaymentRepository advancePaymentRepository) : base(advancePaymentRepository)
        {
            this.advancePaymentRepository = advancePaymentRepository;
        }

        public bool AddAdvancePayment(AdvancePayment advance, Employee employee)
        {

            if (advancePaymentRepository.AddAdvancePayment(advance, employee))
                return true;
            else
                throw new Exception("Hatalı eklme.(Maaşınızın en fazla %30 u kadar avans talebinde bulunabilirsiniz!)");
        }

        public IEnumerable<AdvancePayment> AdvancePaymentList(int id)
        {
           return advancePaymentRepository.AdvancePaymentList(id);    
        }
    }
}
