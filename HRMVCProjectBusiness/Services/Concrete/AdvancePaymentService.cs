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
            var lastAdvance = employee.AdvancePayments.LastOrDefault();
            if (lastAdvance != null)
            {
                var dateControl = advance.ReplyDate - lastAdvance.ReplyDate;
                if (dateControl.TotalDays > 90 && advance.Amount < employee.Wage * 0.3)
                {
                    advancePaymentRepository.AddAdvancePayment(advance, employee);
                    return true;
                }
                else return false;
                //throw new Exception("Hatalı ekleme.(Maaşınızın en fazla %30 u kadar avans talebinde bulunabilirsiniz!)");
            }
            else if (advance.Amount < employee.Wage * 0.3 /*&& advance.Amount>=0*/)//hala eksi değer alıyor
            {
                advancePaymentRepository.AddAdvancePayment(advance, employee);
                return true;
            }
            else return false;
        }

        public IEnumerable<AdvancePayment> AdvancePaymentList(int id)
        {
           return advancePaymentRepository.AdvancePaymentList(id);    
        }

        public float TotalAdvance(int id)
        {
            if (id >= 0)
            {
                return advancePaymentRepository.TotalAdvance(id);
            }
            else throw new Exception("Id hatası");
        }
    }
}
