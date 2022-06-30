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

        public ManagerController(UserManager<User> userManager, RoleManager<UserRole> roleManager, IEmployeeService employeeService, IAdvancePaymentService advancePaymentService, ICostService costService, IPermissionService permissionService, ICompanyService companyService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            this.employeeService = employeeService;
            this.advancePaymentService = advancePaymentService;
            this.costService = costService;
            this.permissionService = permissionService;
            this.companyService = companyService;   
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

            return View();
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
    }
}
