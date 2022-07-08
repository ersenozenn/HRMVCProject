using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectEntities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace HRMVCProjectWebUI.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]

    public class PackageController : Controller
    {
        private readonly IPackageService packageService;

        public PackageController(IPackageService packageService)
        {
            this.packageService = packageService;
        }
        public IActionResult AddPackage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPackage(Package _package)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Paket eklenemedi";
            }

            Package package = new Package()
            {
                PackageName = _package.PackageName,
                Amount = _package.Amount,
                StartDate = _package.StartDate,
                EndDate = _package.EndDate,
                NumberOfEmployee = _package.NumberOfEmployee,
                Description = _package.Description,
                PackagePhoto = _package.PackagePhoto,
                State = _package.State
            };

            if (package.EndDate >= DateTime.Now) package.State = true;
            else package.State = false;

            if (package.PackagePhoto != null)
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var path = Directory.GetCurrentDirectory() +
                    @"\wwwroot\assets\files\" + ticks + Path.GetExtension(package.PackagePhoto.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    package.PackagePhoto.CopyTo(stream);
                }
                package.PackagePhotoPath = @"\assets\files\" + ticks + Path.GetExtension(package.PackagePhoto.FileName);
            }


            if (package.Amount > 0)
            {
                if (package.Amount >= 1000)
                {
                    if (package.StartDate >= DateTime.Now)
                    {
                        if (package.EndDate > package.StartDate)
                        {
                            packageService.Add(package);
                            return RedirectToAction("PackageList", "Package");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Paket bitiş tarihi başlangıç tarihinden ileri olamaz.");
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Paket başlangıç tarihi bugünden geri olamaz.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Paket Tutarı minimum 1000 TL olmalıdır.");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Paket tutarı negatif bir değer olamaz.");
                return View();
            }
        }

        public IActionResult PackageList()
        {
            return View(packageService.GetAll());
        }

        //public IActionResult PackageActiveList() //home a taşındı
        //{
        //    return View(packageService.PackageActiveList());
        //}

        public IActionResult PackageDetails(int packageId)
        {
            return View(packageService.GetById(packageId));
        }

        public IActionResult UpdatePackage(int packageId)
        {
            Package package = packageService.GetById(packageId);

            return View(package);
        }
        [HttpPost]
        public IActionResult UpdatePackage(Package _package)
        {
            if (ModelState.IsValid)
            {
                Package package = packageService.GetById(_package.Id);
                if (_package.PackagePhoto != null)
                {
                    string ticks = DateTime.Now.Ticks.ToString();
                    var path = Directory.GetCurrentDirectory() + @"\wwwroot\assets\files\" + ticks + Path.GetExtension(_package.PackagePhoto.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        _package.PackagePhoto.CopyTo(stream);
                    }
                    _package.PackagePhotoPath = @"\assets\files\" + ticks + Path.GetExtension(_package.PackagePhoto.FileName);                  
                package.PackagePhotoPath=_package.PackagePhotoPath;
                }

                package.PackageName = _package.PackageName;
                package.Description= _package.Description;
                package.Amount=_package.Amount; 
                package.StartDate=_package.StartDate;
                package.EndDate=_package.EndDate;   
                package.NumberOfEmployee=_package.NumberOfEmployee; 
                packageService.Update(package);             

                return RedirectToAction(nameof(PackageList));
            }
            ModelState.AddModelError("", " Paket Güncellenemedi!");
            return View();
        }
    }
}
