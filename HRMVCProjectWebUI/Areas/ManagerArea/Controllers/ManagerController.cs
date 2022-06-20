using HRMVCProjectEntities.Concrete;
using HRMVCProjectWebUI.Areas.ManagerArea.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class ManagerController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;

        public ManagerController(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public IActionResult AddEmployee()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeRegisterVM EmployeeRegisterVM)
        {
            if (ModelState.IsValid)
            {
                Employee user = new Employee()
                {
                    UserName = EmployeeRegisterVM.UserName,
                    FirstName = EmployeeRegisterVM.FirstName,
                    LastName = EmployeeRegisterVM.LastName,
                    Identity = EmployeeRegisterVM.IdentityNumber,
                    Email = EmployeeRegisterVM.Mail,
                    BirthDate = EmployeeRegisterVM.BirthDate
                    //DateStarted = DateTime.Now                    
                };


                var result = await _userManager.CreateAsync(user, EmployeeRegisterVM.Password);
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
                            return View(EmployeeRegisterVM);
                        }
                    }
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        return View(EmployeeRegisterVM);
                    }
                }
            }
            return View(EmployeeRegisterVM);

        }
    }
}
