using HRMVCProjectEntities.Concrete;
using System.Collections.Generic;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Models
{
    public class EmployeeAdvancePaymentVM
    {
        public IEnumerable<AdvancePayment> AdvancePayments {get; set;}
        public string EmployeeFullName { get; set; }
        public AdvancePayment AdvancePayment { get; set; }
    }
}
