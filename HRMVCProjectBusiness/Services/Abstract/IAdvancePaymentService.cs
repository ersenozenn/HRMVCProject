using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Abstract
{
    public interface IAdvancePaymentService : IGenericService<AdvancePayment>
    {
        bool AddAdvancePayment(AdvancePayment advance, Employee employee);
        IEnumerable<AdvancePayment> AdvancePaymentList(int id);
        public float TotalAdvance(int id);
    }
}
