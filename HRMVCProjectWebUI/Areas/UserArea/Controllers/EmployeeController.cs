using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectBusiness.Services.Concrete;
using HRMVCProjectEntities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;

namespace HRMVCProjectWebUI.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IAdvancePaymentService advancePaymentService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public EmployeeController(IEmployeeService employeeService,IAdvancePaymentService advancePaymentService)
        {
            this.employeeService = employeeService;
            this.advancePaymentService = advancePaymentService;
        }
        public IActionResult Index(int id)
        {
            ViewBag.Header = "Ana Sayfa";
            var employee = employeeService.GetById(id);
            
            
            ViewBag.KullaniciIsim = employee.FirstName;
            HttpContext.Session.SetInt32("Id", employee.Id);
            if (employee.UserPhotoPath != null)
            {
                HttpContext.Session.SetString("picPath", employee.UserPhotoPath);
            }
            HttpContext.Session.SetString("userName", employee.FirstName + " " + employee.LastName);
            
            return View();
        }

        //[Route("Listele")]
        public IActionResult EmployeeList()
        {
            ViewBag.Header = "Çalışanlar";
            var employees = employeeService.GetAll();
            return View(employees);
        }

        // [HttpGet("{id}")]
       // [Route("Anasayfa/{id:int}")]
        public IActionResult EmployeeHome(int id)
        {
            Employee employee = employeeService.GetById(id);
            if (employee.UserPhotoPath == null)
            {
                ViewBag.Foto = "/assets/images/default.jfif";
            }
            else
            {
                ViewBag.Foto = employee.UserPhotoPath;

            }
            ViewBag.KullanıcıAdı = $"{employee.FirstName} {employee.LastName}";
            ICollection<Permission> permissions = employeeService.GetByIdIncludePermission(id).Permissions;
            ViewBag.IzinTalepSayisi = permissions.Count;
            ViewBag.AvansMiktari = (employee.Wage*0.3) - advancePaymentService.TotalAdvance(id);
            ICollection<AdvancePayment> advancePayments = (ICollection<AdvancePayment>)advancePaymentService.AdvancePaymentList(id);
            ViewBag.AvansTalepSayisi = advancePayments.Count;
            return View(employee);
        }


        //[Route("Düzenle/{id:int}")]
        //public IActionResult EmployeeUpdate(int id)
        //{
        //    //int id =(int)HttpContext.Session.GetInt32("Id");
        //    Employee _employee = employeeService.GetById(id);
        //    //ViewBag.Header = "Çalışanlar";
        //    //ViewBag.Header2 = "Düzenleme";
        //    return View(_employee);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmployeeUpdate(Employee _employee)
        {
            Employee employee = employeeService.GetById(_employee.Id);
            if (ModelState.IsValid)
            {

                if (_employee.UserPhoto != null)
                {
                    string ticks = DateTime.Now.Ticks.ToString();
                    var path = Directory.GetCurrentDirectory() + @"\wwwroot\assets\images\" + ticks + Path.GetExtension(_employee.UserPhoto.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        _employee.UserPhoto.CopyTo(stream);
                    }
                    _employee.UserPhotoPath = @"\assets\images\" + ticks + Path.GetExtension(_employee.UserPhoto.FileName);
                    employee.UserPhotoPath = _employee.UserPhotoPath;
                }
                
                employee.PhoneNumber = _employee.PhoneNumber;

                employeeService.Update(employee);
                return RedirectToAction(nameof(EmployeeList));
            }
            ModelState.AddModelError("","Güncellenemedi!");
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("picPath");
            return RedirectToAction("Index", "Home");
        }


    }
}
