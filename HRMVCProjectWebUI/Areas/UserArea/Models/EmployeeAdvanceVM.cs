using HRMVCProjectEntities.Concrete;

namespace HRMVCProjectWebUI.Areas.UserArea.Models
{
    public class EmployeeAdvanceVM
    {
        public Employee Employee { get; set; }
        public AdvancePayment AdvancePayment { get; set; }
    }
}
