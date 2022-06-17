using HRMVCProjectEntities.Concrete;
using System.Collections.Generic;

namespace HRMVCProjectWebUI.Areas.UserArea.Models
{
    public class CostAndTypesVM
    {
        public Cost Cost { get; set; }
        public Employee Employee { get; set; }
        public IEnumerable<CostType> CostTypes { get; set; }
    }
}
