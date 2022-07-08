using System.ComponentModel.DataAnnotations;

namespace HRMVCProjectWebUI.Areas.UserArea.Models
{
    public class PasswordChangeVM
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Eski Şifreniz")]       
        [Required(ErrorMessage = "Lütfen açıklama giriniz")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]        
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Şifreniz 6 ile 12 karakter arasında olmalıdır..")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Şifreler eşleşmiyor")]
        [Display(Name = "Yeni Şifre Tekrar")]
        public string ConfirmPassword { get; set; }           
    }
}
