using HRMVCProjectEntities.Concrete;
using HRMVCProjectWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HRMVCProjectWebUI.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [Route("")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Header = "Anasayfa";
            await SeedSuperAdminAsync(_userManager, _roleManager);
            await SeedManagerAsync(_userManager, _roleManager);
            return View();
        }               

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AboutUs()
        {
            ViewBag.Header = "Hakkımızda";
            return View();
        }

        public IActionResult ContactUs()
        {
            ViewBag.Header = "İletişim";
            return View();
        }
        public async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new Employee
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Mukesh",
                LastName = "Murugan",
                Wage = 152000,
                CompanyId = 1,
                DateStarted = DateTime.Now,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin123.");                   
                    await userManager.AddToRoleAsync(defaultUser, "Admin");
                }

            }
        }
        public async Task SeedManagerAsync(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            //Seed Manager User
            var defaultUser = new Employee
            {
                UserName = "Fatma",
                Email = "fatma@gmail.com",
                FirstName = "Fatma",
                LastName = "Eraslan",
                Wage = 152000,
                CompanyId = 1,
                DateStarted = DateTime.Now,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Fatma123.");
                    await userManager.AddToRoleAsync(defaultUser, "Manager");
                }

            }
        }
    }
}
