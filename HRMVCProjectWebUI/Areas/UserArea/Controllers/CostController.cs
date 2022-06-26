using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectEntities.Concrete.Enums;
using HRMVCProjectWebUI.Areas.UserArea.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace HRMVCProjectWebUI.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class CostController : Controller
    {
        private readonly ICostService costService;
        private readonly ICostTypeService costTypeService;
        private readonly IEmployeeService employeeService;

        public CostController(ICostService costService,ICostTypeService costTypeService,IEmployeeService employeeService)
        {
            this.costService = costService;
            this.costTypeService = costTypeService;
            this.employeeService = employeeService;
        }


        public IActionResult CostList(int id)
        {
            ViewBag.Header = "Harcama Listesi";
            var costs = employeeService.GetByIdIncludeCosts(id).Costs;
            return View(costs);
        }

        
        public IActionResult CostCreate(int id)
        {
            ViewBag.Header = "Harcama Talebi Oluştur";
            var costTypes = costService.GetAll();
            if(costTypes!=null)
            {
                ViewBag.CostTypes = costTypes;
            }

            CostAndTypesVM costAndTypesVM = new CostAndTypesVM();
            costAndTypesVM.CostTypes = costTypeService.GetAll();
            costAndTypesVM.Employee = employeeService.GetById(id);
            costAndTypesVM.EmployeeId= id; 
            return View(costAndTypesVM);
        }
        [HttpPost]
        public IActionResult CostCreate(int id, CostAndTypesVM costAndTypesVM)
        {
            if(!ModelState.IsValid)
            {
                TempData["Message"] = "Harcama eklenemedi";
                costAndTypesVM.CostTypes = costTypeService.GetAll();
                return View(costAndTypesVM);
            }

            Cost cost = new Cost();
            cost.RequestDate = costAndTypesVM.Cost.RequestDate;
            cost.CostTypeId=costAndTypesVM.Cost.CostTypeId;
            cost.Amount = costAndTypesVM.Cost.Amount;
            cost.RequestDate = DateTime.Now;
            cost.ReplyState= ReplyState.Beklemede;
            cost.CostFile = costAndTypesVM.Cost.CostFile;
            if(cost.CostFile!=null)
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var path = Directory.GetCurrentDirectory() +
                    @"\wwwroot\assets\files\" + ticks + Path.GetExtension(cost.CostFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    cost.CostFile.CopyTo(stream);
                }
                cost.CostFilePath = @"\assets\files\" + ticks + Path.GetExtension(cost.CostFile.FileName);
            }
            if (cost.Amount > 0)
            {
                if (cost.Amount >= 50)
                {
                    cost.Employees.Add(employeeService.GetById(id));
                    costService.Add(cost);
                    return RedirectToAction("CostList", "Cost", new { id });
                }
                else
                {
                    ModelState.AddModelError("", "Harcama talebiniz minimum 50 TL olmalıdır.");
                    costAndTypesVM.EmployeeId = id;
                    costAndTypesVM.CostTypes = costTypeService.GetAll();
                    return View(costAndTypesVM);
                }
            }
            else
            {
                ModelState.AddModelError("", "Harcama tutarı negatif bir değer olamaz.");
                costAndTypesVM.EmployeeId = id;
                costAndTypesVM.CostTypes = costTypeService.GetAll();
                return View(costAndTypesVM);
            }
        }

    }
}
