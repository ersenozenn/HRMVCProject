using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HRMVCProjectEntities.Concrete;

namespace HRMVCProjectWebUI.Areas.UserArea.Controllers
{
    
    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        public UserController()
        {

        }
        public IActionResult Index()
        {

            //if (employee == null)
            //{
            //    TempData["Message"] = "Giriş bilgileri hatalı!";
            //    return View();
            //}
            //ViewBag.KullaniciIsim = employee.FirstName;
            //HttpContext.Session.SetInt32("Id", employee.Id);
            //if (employee.UserPhotoPath != null)
            //{
            //    HttpContext.Session.SetString("picPath", employee.UserPhotoPath);
            //}
            //HttpContext.Session.SetString("userName", employee.FirstName + " " + employee.LastName);
            return RedirectToAction("List", "Employee");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("picPath");
            return RedirectToAction("Index", "Home");
            return View();
        }
    }
}
