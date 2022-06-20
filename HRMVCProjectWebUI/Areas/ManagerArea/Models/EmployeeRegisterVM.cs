using System;
using System.ComponentModel.DataAnnotations;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Models
{
    public class EmployeeRegisterVM
    {
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        [Display(Name = "Mail")]
        public string Mail { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "TC Kimlik No")]
        public string IdentityNumber { get; set; }
        [Display(Name = "Doğum Tarihi")]
        public DateTime BirthDate { get; set; }
    }
}
