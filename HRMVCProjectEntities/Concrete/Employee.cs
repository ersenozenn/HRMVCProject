using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectEntities.Concrete
{
    public class Employee : User
    {
        public Employee()
        {
            Permissions = new HashSet<Permission>();
            AdvancePayments = new HashSet<AdvancePayment>();
            Costs = new HashSet<Cost>();
        }

       
        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public double Wage { get; set; }
        [RegularExpression(@"^[\w-._+%]+@(?:[\w-]+.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
       
       // public string Password { get; set; }
        public string Telephone { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateQuit { get; set; }
        public string UserPhotoPath { get; set; }
        public IFormFile UserPhoto { get; set; }
        public ICollection<Permission> Permissions { get; set; }       
       
        public ICollection<AdvancePayment> AdvancePayments { get; set; }    
        public ICollection<Cost> Costs { get; set; }    
    }
}
