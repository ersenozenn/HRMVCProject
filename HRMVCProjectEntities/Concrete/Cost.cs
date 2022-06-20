using Core.Entities;
using HRMVCProjectEntities.Concrete.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectEntities.Concrete
{
    public class Cost:BaseEntity
    {
        public Cost()
        {
            Employees = new HashSet<Employee>();
        }
        [Display(Name = "Tutar")]
        [Required(ErrorMessage = "Lütfen tutar giriniz")]
        public double Amount { get; set; }
        [Display(Name = "Talep Tarihi")]
        public DateTime RequestDate { get; set; } 
        [Display(Name = "Cevaplanma Tarihi")]
        public DateTime ReplyDate { get; set; }
        [Display(Name = "Onay Durumu")]
        public ReplyState ReplyState { get; set; }
        [Display(Name = "Harcama Dosyası")]
        public IFormFile CostFile { get; set; }
        public string CostFilePath { get; set; }
        [Display(Name ="Harcama Türü")]
        public int? CostTypeId { get; set; }
        public CostType CostType { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
