using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Şirket Adı")]
        [Required(ErrorMessage = "Şirket Adı boş geçilemez.")]
        public string Name { get; set; }
        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Adres boş geçilemez.")]
        public string Address { get; set; }
        [Display(Name = "Sektör")]
        [Required(ErrorMessage = "Sektör boş geçilemez.")]
        public string Sector { get; set; }
        [Display(Name = "Çalışan Sayısı")]
        public int NumberOfEmployees { get { return GetEmployeeCount(); } }
        [Display(Name = "Mail Uzantısı")]
        [Required(ErrorMessage = "Mail Uzantısı boş geçilemez.")]
        public string MailExtension { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public int GetEmployeeCount()
        {
            return Employees.Count;
        }
    }
}
