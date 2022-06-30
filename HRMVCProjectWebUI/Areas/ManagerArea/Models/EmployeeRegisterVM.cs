using System;
using System.ComponentModel.DataAnnotations;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Models
{
    public class EmployeeRegisterVM
    {
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get { return StringReplace(FirstName + LastName).ToLower(); } }
        [Required(ErrorMessage = "Ad boş geçilemez.")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad boş geçilemez.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        [Display(Name = "Mail")]
        public string Mail { get; set; }
        [Display(Name = "Şifre")]
       
        public string Password { get; set; }
        [Display(Name = "TC Kimlik No")]
        [Required(ErrorMessage = "Kimlik numarası boş geçilemez.")]
        public string IdentityNumber { get; set; }
        [Display(Name = "Doğum Tarihi")]
        [Required(ErrorMessage = "Doğum tarihi boş geçilemez.")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Cinsiyet")]
        [Required(ErrorMessage = "Cinsiyet boş geçilemez.")]
        public bool Gender { get; set; }
        [Display(Name = "Maaş")]
        [Required(ErrorMessage = "Maaş boş geçilemez.")]
        public int Wage { get; set; }
        public int CompanyId { get; set; }
        public string StringReplace(string text)
        {
            text = text.Replace("İ", "I");
            text = text.Replace("ı", "i");
            text = text.Replace("Ğ", "G");
            text = text.Replace("ğ", "g");
            text = text.Replace("Ö", "O");
            text = text.Replace("ö", "o");
            text = text.Replace("Ü", "U");
            text = text.Replace("ü", "u");
            text = text.Replace("Ş", "S");
            text = text.Replace("ş", "s");
            text = text.Replace("Ç", "C");
            text = text.Replace("ç", "c");
            text = text.Replace(" ", ".");
            return text;
        }

    }
}
