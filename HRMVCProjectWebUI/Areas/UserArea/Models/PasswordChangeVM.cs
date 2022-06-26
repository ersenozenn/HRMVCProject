using System.ComponentModel.DataAnnotations;

namespace HRMVCProjectWebUI.Areas.UserArea.Models
{
    public class PasswordChangeVM
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Şifreniz")]       
        [Required(ErrorMessage = "Lütfen açıklama giriniz")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Şifreler eşleşmiyor")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Şifreniz 6 ile 12 karakter arasında olmalıdır..")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Şifreler eşleşmiyor")]
        [Display(Name = "Şifre Tekrar")]
        public string ConfirmPassword { get; set; }    
    }
}
