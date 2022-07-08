using FluentValidation.Results;
using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectBusiness.Validators;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectWebUI.Areas.ManagerArea.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class ManagerController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IEmployeeService employeeService;
        private readonly IAdvancePaymentService advancePaymentService;
        private readonly ICostService costService;
        private readonly IPermissionService permissionService;
        private readonly ICompanyService companyService;
        private readonly IWalletService walletService;
        private readonly IPackageService packageService;

        public ManagerController(UserManager<User> userManager, RoleManager<UserRole> roleManager, IEmployeeService employeeService, IAdvancePaymentService advancePaymentService, ICostService costService, IPermissionService permissionService, ICompanyService companyService, IWalletService walletService,IPackageService packageService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this.employeeService = employeeService;
            this.advancePaymentService = advancePaymentService;
            this.costService = costService;
            this.permissionService = permissionService;
            this.companyService = companyService;
            this.walletService = walletService;
            this.packageService = packageService;
        }

        public IActionResult ManagerHome(int id)
        {
            Employee employee = employeeService.GetById(id);

            var advancePayments = (List<AdvancePayment>)advancePaymentService.GetPendingAdvancePayments((int)employee.CompanyId);
            ViewBag.PendingAdvancePaymentCount = advancePayments.Count.ToString();

            var costs = (List<Cost>)costService.GetPendingCosts((int)employee.CompanyId);
            ViewBag.PendingCostCount = costs.Count.ToString();

            var permissions = (List<Permission>)permissionService.GetPendingPermissions((int)employee.CompanyId);
            ViewBag.PendingPermissionCount = permissions.Count.ToString();

            //HttpContext.Session.SetInt32("CompanyId", Convert.ToInt32(employee.CompanyId));
            HttpContext.Session.SetInt32("ManagerId", id);
            int manid = (int)HttpContext.Session.GetInt32("ManagerId");
            HttpContext.Session.SetString("userName", employee.FirstName + " " + employee.LastName);
            HttpContext.Session.SetInt32("CompanyId", (int)employee.CompanyId);
            HttpContext.Session.SetInt32("ManagerId", (int)id);

            //package kart
            var packages = packageService.ManagersPackages(id);
            return View(packages);
        }

     

        public IActionResult Wallet(int id)
        {
            Employee employee = employeeService.GetById(id);
            Wallet wallet = null;
            if(employee.WalletId!=null)
            {
                wallet = walletService.GetByIdIncludeEmployee((int)employee.WalletId);
                return View(wallet);
            }
            return View(wallet);
        }

        [HttpPost]
        public IActionResult Wallet(Wallet wallet)
        {
            Employee employee = employeeService.GetById(wallet.Id);
            Wallet walletNew = walletService.GetByIdIncludeEmployee((int)employee.WalletId);
            if (wallet.Balance < 50)
            {
                ModelState.AddModelError("", "Eklemek istediğiniz bakiye tutarı 50 tl'den az olamaz.");
                return View(walletNew);
            }
            walletNew.TransactionDate = DateTime.Now;
            walletNew.Balance = walletNew.Balance + wallet.Balance;
            walletService.Update(walletNew);
            return View(walletNew);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeRegisterVM employeeRegisterVM)
        {
            int Id= (int)HttpContext.Session.GetInt32("ManagerId");
            if (employeeService.CheckIdentity(employeeRegisterVM.IdentityNumber))
            {
                if (ModelState.IsValid)
                {
                    var company=companyService.GetByIdIncludeEmployees((int)HttpContext.Session.GetInt32("CompanyId"));
                    Employee user = new Employee()
                    {                       
                        FirstName = employeeRegisterVM.FirstName,
                        LastName = employeeRegisterVM.LastName,
                        Identity = employeeRegisterVM.IdentityNumber,
                        UserName = employeeRegisterVM.UserName,
                        Email = employeeRegisterVM.UserName.ToLower() + "@" + company.MailExtension,
                        BirthDate = employeeRegisterVM.BirthDate,
                        Gender = employeeRegisterVM.Gender,
                        CompanyId = company.Id,
                        Wage= employeeRegisterVM.Wage

                    };
                    employeeRegisterVM.Password = user.Identity + "iK!";


                    if (string.IsNullOrEmpty(employeeRegisterVM.Password))
                    {
                        ModelState.AddModelError("", "Lütfen geçerli bir şifre giriniz.");
                        return View(employeeRegisterVM);
                    }
                    EmployeeValidator validations = new EmployeeValidator();
                    ValidationResult validationResult = validations.Validate(user);

                    var defaultrole = _roleManager.FindByNameAsync("User").Result;
                    if (defaultrole != null)
                    {
                        if (validationResult.IsValid)
                        {
                            
                                var result = await _userManager.CreateAsync(user, employeeRegisterVM.Password);
                                IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
                                if (result.Succeeded && roleresult.Succeeded)
                                {
                                    MailMessage mail = new MailMessage();
                                    mail.To.Add(user.Email);
                                    mail.From = new MailAddress("ikburada9@gmail.com");
                                    mail.Subject = "Şifre Al";
                                    string Body = $"Merhaba sayın kullanıcı Şifreniz: {user.Identity + "iK!"}";
                                    mail.Body = Body;
                                    mail.IsBodyHtml = true;
                                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                    smtp.Port = 587;
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;//**
                                    smtp.UseDefaultCredentials = false;
                                    smtp.Credentials = new System.Net.NetworkCredential("ikburada9@gmail.com", "oouimajmmdkxwsdw"); // Enter seders User name and password  
                             
                                    smtp.EnableSsl = true;
                                smtp.Send(mail);


                               

                                return RedirectToAction("ManagerHome", "Manager", new { Id });
                                }
                                //AAdd12.sfgd
                                else
                                {
                                    foreach (var item in result.Errors)
                                    {
                                        ModelState.AddModelError("", item.Description);
                                        return View(employeeRegisterVM);
                                    }
                                }                           
                        }
                        else
                        {
                            foreach (var item in validationResult.Errors)
                            {
                                ModelState.AddModelError("", item.ErrorMessage.ToString());
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bir hata oluştu");
                        return View(employeeRegisterVM);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen boş alan bırakmayınız.");
                    return View(employeeRegisterVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "Bu Tc numaralı kullanıcı zaten kayıtlı.");
                return View(employeeRegisterVM);
            }


            return View(employeeRegisterVM);

        }
        public IActionResult EmployeeList()
        {
            ViewBag.Header = "Çalışanlar";
            var employees = employeeService.GetAll();
            List<Employee> list = new List<Employee>();
            int Id = (int)HttpContext.Session.GetInt32("ManagerId");
            Employee employee = employeeService.GetById(Id);
            foreach (Employee item in employees)
            {
                if (item.CompanyId == employee.CompanyId)
                {
                    list.Add(item);
                }
            }
            return View(list);
        }
    }
}
