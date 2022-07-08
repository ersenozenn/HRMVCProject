using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectEntities.Concrete
{
    public class CreditCard : BaseEntity
    {
        [Display(Name = "Kart Numarası")]
        [Required(ErrorMessage = "Kart Numarası boş geçilemez.")]
        public double CardNumber { get; set; }
        [Display(Name = "Son Kullanma Tarihi")]
        [Required(ErrorMessage = "Son Kullanma Tarihi boş geçilemez.")]
        public DateTime ExpirationDate { get; set; }
        [Display(Name = "Kart Bakiyesi")]
        [Required(ErrorMessage = "Kart Bakiyesi boş geçilemez.")]
        public decimal CardBalance { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
