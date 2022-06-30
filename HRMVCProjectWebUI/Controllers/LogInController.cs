using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectEntities.Concrete;
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
        private readonly IEmployeeService employeeService;

        public LogInController(SignInManager<User> signInManager, UserManager<User> userManager,IEmployeeService employeeService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.employeeService = employeeService;
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
               
                if (userResult != null)
                {
                    Employee employee = employeeService.GetById(userResult.Id);
                    var getUser = userManager.IsInRoleAsync(userResult, "User").Result;
                    var getManager = userManager.IsInRoleAsync(userResult, "Manager").Result;
                    var getAdmin = userManager.IsInRoleAsync(userResult, "Admin").Result;//eklenecek


                    if (getUser)
                    {
                        var result = await signInManager.PasswordSignInAsync(userResult.UserName, user.Password, false, true);

                        if (result.Succeeded)
                        {
                            if (user.Password ==employee.Identity + "iK!")
                            {
                                return RedirectToAction("PasswordChange", "Employee", new { id = userResult.Id, Area = "UserArea" });
                            }
                            else
                            {
                                return RedirectToAction("Index", "Employee", new { id = userResult.Id, Area = "UserArea" });
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre girdiniz");
                        }
                    }
                    if (getAdmin)
                    {
                        var result = await signInManager.PasswordSignInAsync(userResult.UserName, user.Password, false, true);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("AdminHome", "Admin", new { id = userResult.Id, Area = "AdminArea" });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre girdiniz");
                        }
                    }
                    if (getManager)
                    {
                        var result = await signInManager.PasswordSignInAsync(userResult.UserName, user.Password, false, true);
                        if (result.Succeeded)
                        {
                            if (user.Password == employee.Identity + "iK!")
                            {
                                // To do manager a şifre değiştirme işlem eklenecek.
                                return RedirectToAction("ManagerHome", "Manager", new { id = userResult.Id, Area = "ManagerArea" });
                            }
                            else
                            {
                                return RedirectToAction("ManagerHome", "Manager", new { id = userResult.Id, Area = "ManagerArea" });
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre girdiniz");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre girdiniz");
                }

            }
            return View();

        }
    }
}
