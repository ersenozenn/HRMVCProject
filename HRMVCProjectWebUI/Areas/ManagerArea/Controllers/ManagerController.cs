using FluentValidation.Results;
using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectBusiness.Validators;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectWebUI.Areas.ManagerArea.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public ManagerController(UserManager<User> userManager, RoleManager<UserRole> roleManager, IEmployeeService employeeService,IAdvancePaymentService advancePaymentService)
        {
            _roleManager = roleManager;
            this.employeeService = employeeService;
            this.advancePaymentService = advancePaymentService;
            _userManager = userManager;
        }

        public IActionResult ManagerHome(int id)
        {
            Employee employee = employeeService.GetById(id);
            var advancePayments=(List<AdvancePayment>)advancePaymentService.GetPendingAdvancePayments();
            ViewBag.PendingAdvancePaymentCount = advancePayments.Count.ToString();
            HttpContext.Session.SetInt32("CompanyId", (int)employee.CompanyId);
            HttpContext.Session.SetInt32("ManagerId", (int)id);

            return View();
        }

        public IActionResult AddEmployee()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeRegisterVM EmployeeRegisterVM,int id)
        {

            if (ModelState.IsValid)
            {
                Employee user = new Employee()
                {
                    UserName = EmployeeRegisterVM.Mail,
                    FirstName = EmployeeRegisterVM.FirstName,
                    LastName = EmployeeRegisterVM.LastName,
                    Identity = EmployeeRegisterVM.IdentityNumber,
                    Email = EmployeeRegisterVM.Mail,
                    BirthDate = EmployeeRegisterVM.BirthDate,
                    Gender = EmployeeRegisterVM.Gender
                    //CompanyId=
                    //DateStarted = DateTime.Now                    
                };
                EmployeeRegisterVM.Password = "258iK!";


                if (string.IsNullOrEmpty(EmployeeRegisterVM.Password))
                {
                    ModelState.AddModelError("", "Lütfen geçerli bir şifre giriniz.");
                    return View(EmployeeRegisterVM);
                }
                EmployeeValidator validations = new EmployeeValidator();
                ValidationResult validationResult = validations.Validate(user);
                
                var defaultrole = _roleManager.FindByNameAsync("User").Result;
                if (defaultrole != null)
                {
                    if (validationResult.IsValid)
                    {
                        try
                        {
                            var result = await _userManager.CreateAsync(user, EmployeeRegisterVM.Password);
                            IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
                            if (result.Succeeded && roleresult.Succeeded)
                            {
                                MailMessage mail = new MailMessage();
                                mail.To.Add(user.Email);
                                mail.From = new MailAddress("ikburada9@gmail.com");
                                mail.Subject = "Şifre Al";
                                string Body = $"Şifreniz: 258iK!";
                                mail.Body = Body;
                                mail.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com");/*"domain-com.mail.protection.outlook.com"*/
                               smtp.Port = 587;   
                                smtp.DeliveryMethod=SmtpDeliveryMethod.Network;//**
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = new System.Net.NetworkCredential("ikburada9@gmail.com", "oouimajmmdkxwsdw"); // Enter seders User name and password  
                                //smtp.Credentials = new System.Net.NetworkCredential(user.UserName, userRegisterVM.Password); // Enter seders User name and password  
                                smtp.EnableSsl = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                smtp.Send(mail);
                                


                                return RedirectToAction("Login", "LogIn");
                            }
                            //AAdd12.sfgd
                            else
                            {
                                foreach (var item in result.Errors)
                                {
                                    ModelState.AddModelError("", item.Description);
                                    return View(EmployeeRegisterVM);
                                }
                            }
                        }
                        catch (System.Exception)
                        {

                            ModelState.AddModelError("", "Şifreniz bir sayı,bir büyük,bir küçük harf ve bir de özel karakter içermelidir.");
                            return View(EmployeeRegisterVM);
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
                        return View(EmployeeRegisterVM);                   
                }
            }
            else
            {
                ModelState.AddModelError("", "Lütfen boş alan bırakmayınız.");
                return View(EmployeeRegisterVM);
            }

            return View(EmployeeRegisterVM);

        }
    }
}
