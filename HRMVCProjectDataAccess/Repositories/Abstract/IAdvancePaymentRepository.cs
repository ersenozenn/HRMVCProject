using DataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Abstract
{
    public interface IAdvancePaymentRepository : IRepository<AdvancePayment>
    {
        bool AddAdvancePayment(AdvancePayment advance, Employee employee);
        IEnumerable<AdvancePayment> AdvancePaymentList(int id);
        public float TotalAdvance(int id);
    }
}
