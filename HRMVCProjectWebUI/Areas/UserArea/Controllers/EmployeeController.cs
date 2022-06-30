using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectBusiness.Services.Concrete;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectWebUI.Areas.UserArea.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HRMVCProjectWebUI.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IAdvancePaymentService advancePaymentService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;     
        private readonly ICostService costService;

        public EmployeeController(IEmployeeService employeeService,IAdvancePaymentService advancePaymentService, UserManager<User> userManager,ICostService costService,SignInManager<User> signInManager)
        {
            this.employeeService = employeeService;
            this.advancePaymentService = advancePaymentService;
            this.userManager = userManager;
            this.costService = costService;
            this.signInManager = signInManager;          
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
            List<Employee> list = new List<Employee>();
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("Id"));
            foreach (Employee item in employees)
            {
                if (item.CompanyId == employee.CompanyId)
                {
                    list.Add(item);
                }
            }
            return View(list);
        }

        // [HttpGet("{id}")]
       // [Route("Anasayfa/{id:int}")]
        public IActionResult EmployeeHome(int id)
        {
            ViewBag.Header = "Profilim";
            Employee employee = employeeService.GetById(id);
            if (employee.UserPhotoPath == null)
            {
                HttpContext.Session.SetString("picPath", "~/assets/images/default.png");
            }
            else
            {
                HttpContext.Session.SetString("picPath", employee.UserPhotoPath);

            }
            ViewBag.KullanıcıAdı = $"{employee.FirstName} {employee.LastName}";
            ICollection<Permission> permissions = employeeService.GetByIdIncludePermission(id).Permissions;
            int permissionCount = 0;
            foreach (Permission item in permissions)
            {
                if (item.ReplyState == HRMVCProjectEntities.Concrete.Enums.ReplyState.Beklemede)
                {
                    permissionCount++;
                }
            }
            ViewBag.IzinTalepSayisi = permissionCount;
            ViewBag.AvansMiktari = (employee.Wage*0.3) - advancePaymentService.TotalAdvance(id);
            ICollection<AdvancePayment> advancePayments = (ICollection<AdvancePayment>)advancePaymentService.AdvancePaymentList(id);
            int advancePaymentCount = 0;
            foreach (AdvancePayment item in advancePayments)
            {
                if (item.ReplyState == HRMVCProjectEntities.Concrete.Enums.ReplyState.Beklemede)
                {
                    advancePaymentCount++;
                }
            }
            ViewBag.AvansTalepSayisi = advancePaymentCount;

            ICollection<Cost> costs = (ICollection<Cost>)employeeService.GetByIdIncludeCosts(id).Costs;
            int costsCount = 0;
            foreach (Cost item in costs)
            {
                if (item.ReplyState == HRMVCProjectEntities.Concrete.Enums.ReplyState.Beklemede)
                {
                    costsCount++;
                }
            }
            ViewBag.HarcamaTalepSayisi = costsCount;
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

                if (employee.UserPhotoPath != null)
                {
                    HttpContext.Session.SetString("picPath", employee.UserPhotoPath);
                }

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

        public IActionResult PasswordChange(int id)
        {
            HttpContext.Session.SetInt32("Id", id);
            PasswordChangeVM passwordChangeVM = new PasswordChangeVM();
            passwordChangeVM.EmployeeId = id;
            Employee employee = employeeService.GetById(passwordChangeVM.EmployeeId);           
            return View(passwordChangeVM);
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeVM passwordChangeVM,int id)
        {      
            if (ModelState.IsValid)
            {
                //var user = userManager.GetUserAsync(User);
                passwordChangeVM.EmployeeId = id;
                var user = employeeService.GetById(passwordChangeVM.EmployeeId);
                if (user == null)
                {
                    return RedirectToAction("Login","LogIn");
                }

                // ChangePasswordAsync changes the user password
                var result =await  userManager.ChangePasswordAsync(user,passwordChangeVM.CurrentPassword, passwordChangeVM.NewPassword);

                
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                var resultforUpdate = await userManager.UpdateAsync(user);
                if (resultforUpdate.Succeeded)
                {
                    await signInManager.RefreshSignInAsync(user);
                    return RedirectToAction("EmployeeHome","Employee", new { id = id });
                }
                foreach (var item in resultforUpdate.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    return View();
                }
                
            }

            return View();


        }


    }
}
