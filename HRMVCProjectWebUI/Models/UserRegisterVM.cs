using System;

namespace HRMVCProjectWebUI.Models
{
    public class UserRegisterVM
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }

    }
}
