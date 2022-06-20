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
    public class Permission : BaseEntity
    {
        public Permission()
        {
            Employees = new HashSet<Employee>();
        }
		[Display(Name = "Talep Tarihi")]
        public DateTime RequestDate { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartingDate { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Başvuru Durumu")]
        public ReplyState ReplyState { get; set; }
        [Display(Name = "Gidilecek Adres")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "10 karakterden az, 250 karakterden fazla giremezsiniz.")]
        public string AdressToGo { get; set; }
        [Display(Name = "İzin Türü")]
        public int? PermissionTypeID { get; set; }
        [Display(Name = "İzin Türü")]
        public PermissionType PermissionType { get; set; }
        [Display(Name = "Çalışan Listesi")]
        public ICollection<Employee> Employees { get; set; }

    }
}
