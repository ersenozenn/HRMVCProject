﻿using HRMVCProjectEntities.Concrete;
using HRMVCProjectWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRMVCProjectWebUI.Controllers
{
    public class LogInController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public LogInController(SignInManager<User> signInManager,UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
      
        public IActionResult Login()
        {         

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm user)
        {
            if (ModelState.IsValid)
            {
                var userResult = userManager.FindByEmailAsync(user.Email).Result;
                var getAdmin = userManager.IsInRoleAsync(userResult, "User").Result;
                
                    if (getAdmin)
                    {
                        var result = await signInManager.PasswordSignInAsync(userResult.UserName, user.Password, false, true);//user name olacak
                    if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Employee", new { id = userResult.Id, Area = "UserArea" });
                       
                        }
                        else
                        {
                            ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre girdiniz");
                        }
                    }               
            }
            return View();

        }
    }
}
