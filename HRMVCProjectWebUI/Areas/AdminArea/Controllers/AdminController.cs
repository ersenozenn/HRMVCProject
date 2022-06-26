using FluentValidation.Results;
using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectBusiness.Validators;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectWebUI.Areas.AdminArea.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HRMVCProjectWebUI.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class AdminController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IAdvancePaymentService advancePaymentService;        
        private readonly ICompanyService companyService;        
        private readonly UserManager<User> userManager;
        private readonly RoleManager<UserRole> roleManager;

        public AdminController(IEmployeeService employeeService, IAdvancePaymentService advancePaymentService, UserManager<User> userManager,RoleManager<UserRole> roleManager, ICompanyService companyService)
        {
            this.employeeService = employeeService;
            this.advancePaymentService = advancePaymentService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.companyService = companyService;
        }
        public IActionResult AdminHome(int id)
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

            //List Companies Start

            IEnumerable<Company> companyList = companyService.GetAll(); 



            //List Companies End



            return View(companyList);
        }
        public IActionResult AddManager(int? Id)
        {
            int companyId = (int)Id;
            HttpContext.Session.SetInt32("companyId", companyId);
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddManager(ManagerRegisterVM managerRegisterVM)
        {
            int companyId = (int)HttpContext.Session.GetInt32("companyId");
            var company = companyService.GetByIdIncludeEmployees(companyId);    

            if (ModelState.IsValid)
            {
                Employee user = new Employee()
                {                    
                    FirstName = managerRegisterVM.FirstName,
                    LastName = managerRegisterVM.LastName,
                    Identity = managerRegisterVM.IdentityNumber,
                    UserName =managerRegisterVM.UserName,
                    Email = managerRegisterVM.UserName.ToLower()+"@"+company.MailExtension,
                    BirthDate = managerRegisterVM.BirthDate,
                    Gender = managerRegisterVM.Gender,
                    CompanyId = company.Id,  
                    
                    //DateStarted = DateTime.Now                    
                };
                managerRegisterVM.Password = "258iK!";
                if (string.IsNullOrEmpty(managerRegisterVM.Password))
                {
                    ModelState.AddModelError("", "Lütfen geçerli bir şifre giriniz.");
                    return View(managerRegisterVM);
                }
                
                EmployeeValidator validations = new EmployeeValidator();
                ValidationResult validationResult = validations.Validate(user);

                var defaultrole = roleManager.FindByNameAsync("Manager").Result;
                if (defaultrole != null)
                {
                    if (validationResult.IsValid)
                    {
                        try
                        {
                            var result = await userManager.CreateAsync(user, managerRegisterVM.Password);
                            IdentityResult roleresult = await userManager.AddToRoleAsync(user, defaultrole.Name);
                            if (result.Succeeded && roleresult.Succeeded)
                            {
                                MailMessage mail = new MailMessage();
                                mail.To.Add(user.Email);
                                mail.From = new MailAddress("ikburada9@gmail.com");
                                mail.Subject = "Şifre Al";
                                string Body = $"Merhaba sayın yönetici. Şifreniz: 258iK!";
                                mail.Body = Body;
                                mail.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.Port = 587;
                                smtp.UseDefaultCredentials = false;
                                //smtp.Credentials = new System.Net.NetworkCredential("ikburadasite", "Fatma123.44"); // Enter seders User name and password  
                                smtp.Credentials = new System.Net.NetworkCredential("ikburada9@gmail.com", "oouimajmmdkxwsdw"); // Enter seders User name and password  
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                                //return View("Index" /*, _objModelMail*/);

                                int Id = (int)HttpContext.Session.GetInt32("Id");

                                return RedirectToAction("AdminHome", "Admin",new {Id});
                            }
                            //AAdd12.sfgd
                            else
                            {
                                foreach (var item in result.Errors)
                                {
                                    ModelState.AddModelError("", item.Description);
                                    return View(managerRegisterVM);
                                }
                            }
                        }
                        catch (System.Exception)
                        {

                            ModelState.AddModelError("", "Şifreniz bir sayı,bir büyük,bir küçük harf ve bir de özel karakter içermelidir.");
                            return View(managerRegisterVM);
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
                    return View(managerRegisterVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "Lütfen boş alan bırakmayınız.");
                return View(managerRegisterVM);
            }

            return View(managerRegisterVM);

        }
        public IActionResult AddCompany()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult AddCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                companyService.Add(company);
                int Id = (int)HttpContext.Session.GetInt32("Id");

                return RedirectToAction("AdminHome", "Admin", new { Id });
            }
            else
            {
                ModelState.AddModelError("", "Lütfen boş alan bırakmayınız.");
                return View(company);
            }
           
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("picPath");
            HttpContext.Session.Remove("companyId");
            return RedirectToAction("Index", "Home");
        }
    }
}
