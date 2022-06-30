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
using Microsoft.AspNetCore.Http;
using HRMVCProjectWebUI.Areas.ManagerArea.Models;
using HRMVCProjectEntities.Concrete.Enums;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class CostsController : Controller
    {
        private readonly ICostService costService;
        private readonly IEmployeeService employeeService;

        public CostsController(ICostService costService, IEmployeeService employeeService)
        {
            this.costService = costService;
            this.employeeService = employeeService;
        }

        public IActionResult CostList()
        {
            ViewBag.Header = "Harcama Listesi";
            EmployeeCostVM employeeCostVM = new EmployeeCostVM();
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeeCostVM.Costs = costService.GetAllByCompanyId((int)employee.CompanyId);
            foreach (Cost item in employeeCostVM.Costs)
            {
                foreach (Employee item2 in item.Employees)
                {
                    employeeCostVM.EmployeeFullName = employeeService.GetById(item2.Id).FirstName + " " + employeeService.GetById(item2.Id).LastName;
                }
            }
            return View(employeeCostVM);
        }
        public IActionResult DownloadPdf(int Id)
        {
            Cost cost=costService.GetById(Id);

            return File(cost.CostFilePath, "application/pdf");
            
           
        }

        public IActionResult CostCheck()
        {
            EmployeeCostVM employeeCostVM = new EmployeeCostVM();
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeeCostVM.Costs = costService.GetPendingCosts((int)employee.CompanyId);
            foreach (Cost item in employeeCostVM.Costs)
            {
                foreach (Employee item2 in item.Employees)
                {
                    employeeCostVM.EmployeeFullName = employeeService.GetById(item2.Id).FirstName + " " + employeeService.GetById(item2.Id).LastName;
                }
            }
            return View(employeeCostVM);
        }

        [HttpPost]
        public IActionResult CostCheck(EmployeeCostVM employeeCostVM, int id)
        {
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeeCostVM.Costs = costService.GetPendingCosts((int)employee.CompanyId);

            foreach (Cost item in employeeCostVM.Costs)
            {
                if (item.Id == id)
                {
                    item.ReplyState = ReplyState.Onaylandı;
                    item.ReplyDate = DateTime.Now;
                    costService.Update(item);
                }
            }
            employeeCostVM.Costs = costService.GetPendingCosts((int)employee.CompanyId);
            foreach (Cost item in employeeCostVM.Costs)
            {
                foreach (Employee item2 in item.Employees)
                {
                    employeeCostVM.EmployeeFullName = employeeService.GetById(item2.Id).FirstName + " " + employeeService.GetById(item2.Id).LastName;
                }
            }
            return View(employeeCostVM);
        }

        [HttpPost]
        public IActionResult CostCheckRed(EmployeeCostVM employeeCostVM, int id)
        {
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeeCostVM.Costs = costService.GetPendingCosts((int)employee.CompanyId);
            foreach (Cost item in employeeCostVM.Costs)
            {
                if (item.Id == id)
                {
                    item.ReplyState = ReplyState.OnaylanMAdı;
                    item.ReplyDate = DateTime.Now;
                    costService.Update(item);
                }
            }
            employeeCostVM.Costs = costService.GetPendingCosts((int)employee.CompanyId);
            foreach (Cost item in employeeCostVM.Costs)
            {
                foreach (Employee item2 in item.Employees)
                {
                    employeeCostVM.EmployeeFullName = employeeService.GetById(item2.Id).FirstName + " " + employeeService.GetById(item2.Id).LastName;
                }
            }
            return View("CostCheck",employeeCostVM);
        }
    }
}
