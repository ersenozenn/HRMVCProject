using HRMVCProjectEntities.Concrete;
using HRMVCProjectWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HRMVCProjectWebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        public RegisterController(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userRegisterVM)
        {
            if (ModelState.IsValid)
            {
                Employee user = new Employee()
                {
                    UserName = userRegisterVM.UserName,
                    FirstName = userRegisterVM.FirstName,
                    LastName = userRegisterVM.LastName,
                    Identity = userRegisterVM.IdentityNumber,                
                    Email = userRegisterVM.Mail,
                    BirthDate = userRegisterVM.BirthDate
                    //DateStarted = DateTime.Now                    
                };


                var result = await _userManager.CreateAsync(user, userRegisterVM.Password);
                var defaultrole = _roleManager.FindByNameAsync("User").Result;
                if (defaultrole != null)
                {
                    IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultrole.Name);
                    if (result.Succeeded && roleresult.Succeeded)
                    {
                        return RedirectToAction("Login", "LogIn");
                    }
                    //AAdd12.sfgd
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                            return View(userRegisterVM);
                        }

                    }

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return View(userRegisterVM);
                    }

                }
            }
            return View(userRegisterVM);

        }







    }
}
