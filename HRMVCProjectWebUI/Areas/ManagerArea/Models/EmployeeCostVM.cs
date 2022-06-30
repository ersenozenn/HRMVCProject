using HRMVCProjectEntities.Concrete;
using System.Collections.Generic;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Models
{
    public class EmployeeCostVM
    {
        public IEnumerable<Cost> Costs { get; set; }
        public string EmployeeFullName { get; set; }
        public Cost Cost { get; set; }
    }
}
