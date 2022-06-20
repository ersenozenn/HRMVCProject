using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectEntities.Concrete
{
    public class Company:BaseEntity
    {
        public Company()
        {
            Employees = new HashSet<Employee>();   
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Sector { get; set; }
        public int NumberOfEmployees { get { return GetEmployeeCount(); } }
        public string MailExtension { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public int GetEmployeeCount()
        {
            return Employees.Count;
        }
    }
}
