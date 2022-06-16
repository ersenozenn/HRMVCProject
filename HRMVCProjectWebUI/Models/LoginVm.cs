using System.ComponentModel.DataAnnotations;

namespace HRMVCProjectWebUI.Models
{
    public class LoginVm
    {
        [Required]
        public string Email { get; set; }
        //public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
