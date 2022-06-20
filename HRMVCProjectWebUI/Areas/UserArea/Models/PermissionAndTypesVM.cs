using HRMVCProjectEntities.Concrete;
using System.Collections.Generic;

namespace HRMVCProjectWebUI.Areas.UserArea.Models
{
    public class PermissionAndTypesVM
    {
        public PermissionAndTypesVM()
        {
            PermissionTypes = new List<PermissionType>();
        }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Permission Permission { get; set; }
        public IEnumerable<PermissionType> PermissionTypes { get; set; }
    }
}
