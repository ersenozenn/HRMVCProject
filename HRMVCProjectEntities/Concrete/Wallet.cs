using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectEntities.Concrete
{
    public class Wallet : BaseEntity
    {
        public Wallet()
        {
            Employees = new HashSet<Employee>();
        }
        [Display(Name = "Bakiye")]
        [Required(ErrorMessage = "Bakiye boş geçilemez.")]
        public decimal Balance { get; set; } = 0;
        [Display(Name = "İşlem Tarihi")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public ICollection<Employee> Employees { get; set; }
    }
}
