using Core.Entities;
using HRMVCProjectEntities.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectEntities.Concrete
{
    public class AdvancePayment:BaseEntity
    {
        [Display(Name ="Açıklama")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "3 karakterden az, 250 karakterden fazla giremezsiniz.")]
        public string Description { get; set; }
        [Display(Name = "Tutar")]
        public double Amount { get; set; }
        [Display(Name = "Talep Tarihi")]
        public DateTime RequestDate { get; set; } = DateTime.Now;
        [Display(Name = "Cevaplanma Tarihi")]
        public DateTime ReplyDate { get; set; }
        [Display(Name = "Onay Durumu")]
        public ReplyState ReplyState { get; set; }

        public int EmployeeId { get; set; }    
        public Employee Employee { get; set; }    
    }
}
