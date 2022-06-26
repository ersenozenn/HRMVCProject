using System;
using System.ComponentModel.DataAnnotations;


namespace HRMVCProjectWebUI.Areas.AdminArea.Models
{
    public class ManagerRegisterVM
    {
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get { return StringReplace(FirstName + LastName).ToLower(); } }
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
        [Display(Name = "Cinsiyet")]
        public bool Gender { get; set; }
        [Display(Name = "Maaş")]
        public bool Wage { get; set; }
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
