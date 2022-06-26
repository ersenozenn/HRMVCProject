using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRMVCProjectDataAccess.Data;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectBusiness.Services.Abstract;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class PermissionsController : Controller
    {
        private readonly IPermissionService permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }
        public IActionResult PermissionList(int companyId)
        {
            ViewBag.Header = "Avans Listesi";
            var permissions = permissionService.GetAllByCompanyId(companyId);
            return View(permissions);
        }

    }
}
