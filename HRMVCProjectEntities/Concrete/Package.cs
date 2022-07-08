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
    public class Package:BaseEntity
    {
        [Required(ErrorMessage ="Paket adı boş bırakılamaz")]
        [Display(Name = "Paket Adı")]
        public string PackageName { get; set; }

        [Required(ErrorMessage = "Paket açıklaması boş bırakılamaz")]
        [Display(Name = "Paket Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Tutar boş bırakılamaz")]
        [Display(Name = "Tutar")]      
        public double Amount { get; set; }

        [Required(ErrorMessage = "Paket başlangıç tarihi boş bırakılamaz")]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Paket bitiş tarihi boş bırakılamaz")]
        [Display(Name = "Bitiş Tarihi")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        [Display(Name = "Paket Fotoğrafı")]
        public string PackagePhotoPath { get; set; }

        [Required(ErrorMessage = "Fotograf boş bırakılamaz")]
        [Display(Name = "Fotoğraf")]
        public IFormFile PackagePhoto { get; set; }

        [Display(Name = "Kullanım Durumu")]
        public bool State { get; set; } //aktiflik pasiflik durumu

        [Required(ErrorMessage = "Paket kullanıcı sayısı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Sayısı")]
        public int NumberOfEmployee { get; set; }

        public ICollection<Employee> Employees { get; set; }

        //public bool GetState()
        //{
        //    if (this.EndDate >= DateTime.Now)
        //        return true;
        //    else return false;
        //}

    }
}
