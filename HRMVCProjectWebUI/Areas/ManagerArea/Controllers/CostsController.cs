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
    public class CostsController : Controller
    {
        private readonly ICostService costService;

        public CostsController(ICostService costService)
        {
            this.costService = costService;
        }

        public IActionResult CostList(int companyId)
        {
            ViewBag.Header = "Harcama Listesi";
            var costs = costService.GetAllByCompanyId(companyId);
            return View(costs);
        }
    }
}
