using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectEntities.Concrete.Enums;
using HRMVCProjectWebUI.Areas.UserArea.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HRMVCProjectWebUI.Controllers
{
    //[Route("Personel")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IPermissionService permissionService;
        private readonly IPermissionTypeService permissionTypeService;
       

        public EmployeeController(IEmployeeService employeeService, IPermissionService permissionService,IPermissionTypeService permissionTypeService)
        {
            this.employeeService = employeeService;
            this.permissionService = permissionService;
            this.permissionTypeService = permissionTypeService;                  
           
        }
        //[Route("")]
        public IActionResult Index()
        {
			IEnumerable<Employee> employees = employeeService.GetAll();
            return View(employees);
        }
        //[Route("Listele")]
        public IActionResult List()
        {
            ViewBag.Header = "Çalışanlar";
            var employees = employeeService.GetAll();
            return View(employees);
        }

        // [HttpGet("{id}")]
        [Route("Anasayfa/{id:int}")]
        public IActionResult Home(int id)
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
            //ICollection<AdvancePayment> advancePayments = (ICollection<AdvancePayment>)advancePaymentService.AdvancePaymentList(id);
            //ViewBag.AvansTalepSayisi = advancePayments.Count;
            return View(employee);
        }
        //[Route("Düzenle/{id:int}")]
        public IActionResult Edit(int id)
        {
            Employee _employee = employeeService.GetById(id);
            ViewBag.Header = "Çalışanlar";
            ViewBag.Header2 = "Düzenleme";
            return View(_employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee _employee)
        {
            if (ModelState.IsValid)
            {
                Employee employee = employeeService.GetById(_employee.Id);

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
                return RedirectToAction(nameof(List));
            }
            TempData["Message"] = "Güncellenemedi!";
            return View(Index());
        }

        public IActionResult EmployeesPermissions(int id)
        {
            ICollection<Permission> permissions = employeeService.GetByIdIncludePermission(id).Permissions;
            return View(permissions);
        }

       
        

        //public IActionResult LogIn()
        //{
        //    LoginVm loginVm = new LoginVm();
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult LogIn(LoginVm loginVm)
        //{
        //   // var employee = employeeService.GetByEmailAndPassword(loginVm.Email, loginVm.Password);
        //    if (employee == null)
        //    {
        //        TempData["Message"] = "Giriş bilgileri hatalı!";
        //        return View();
        //    }
        //    ViewBag.KullaniciIsim = employee.FirstName;
        //    HttpContext.Session.SetInt32("Id", employee.Id);
        //    if (employee.UserPhotoPath != null)
        //    {
        //        HttpContext.Session.SetString("picPath", employee.UserPhotoPath);
        //    }
        //    HttpContext.Session.SetString("userName", employee.FirstName + " " + employee.LastName);
        //    return RedirectToAction("List", "Employee");
        //}
        //public IActionResult LogOut()
        //{
        //    HttpContext.Session.Remove("Id");
        //    HttpContext.Session.Remove("userName");
        //    HttpContext.Session.Remove("picPath");
        //    return RedirectToAction("Index", "Home");
        //}

        public IActionResult PermissionCreate(int id)
        {
            PermissionAndTypesVM permissionAndTypesVM = new PermissionAndTypesVM();
			permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
			permissionAndTypesVM.Employee = employeeService.GetById(id);
			return View(permissionAndTypesVM);
        }

        //[HttpPost]
        //public IActionResult PermissionCreate(int id,PermissionAndTypesVM permissionAndTypesVM)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["Message"] = "İzin eklenemedi!";
        //        permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
        //        return View(permissionAndTypesVM);
        //    }
        //    Permission permission = new Permission();
        //    permission.StartingDate = permissionAndTypesVM.Permission.StartingDate;
        //    permission.AdressToGo = permissionAndTypesVM.Permission.AdressToGo;
        //    permission.PermissionTypeID = permissionAndTypesVM.Permission.PermissionTypeID;

        //    PermissionType permissionType = permissionTypeService.GetById((int)permissionAndTypesVM.Permission.PermissionTypeID);

        //    if (permission.StartingDate.Year == DateTime.Now.Year && permission.StartingDate.CompareTo(DateTime.Now) != -1 && permission.StartingDate.CompareTo(DateTime.Now) != 0)
        //    {
        //        permission.RequestDate = DateTime.Now;
        //        permission.EndDate = permissionAndTypesVM.Permission.StartingDate.AddDays(permissionType.AllowedDays);
        //        permission.ReplyState = ReplyState.Beklemede;
        //        permission.Employees.Add(employeeService.GetById(id));
        //        permissionService.Add(permission);
        //        return RedirectToAction(nameof(List));
        //    }
        //    else
        //    {
        //        throw new Exception("Başlangıç tarihi bugünden sonra olmalıdır.");
        //    }
            
        //}
    }
}