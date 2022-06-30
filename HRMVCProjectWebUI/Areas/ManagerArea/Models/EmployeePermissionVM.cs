using HRMVCProjectEntities.Concrete;
using System.Collections.Generic;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Models
{
    public class EmployeePermissionVM
    {
        public EmployeePermissionVM()
        {
            Permissions = new HashSet<Permission>();
        }
        public IEnumerable<Permission> Permissions { get; set; }
        public string EmployeeFullName { get; set; }
        public Permission Permission { get; set; }
    }
}
