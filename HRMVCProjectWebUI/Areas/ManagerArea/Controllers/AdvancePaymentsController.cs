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
using Microsoft.AspNetCore.Identity;
using HRMVCProjectWebUI.Areas.ManagerArea.Models;
using HRMVCProjectEntities.Concrete.Enums;
using Microsoft.AspNetCore.Http;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class AdvancePaymentsController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IAdvancePaymentService advancePaymentService;

        public AdvancePaymentsController(IEmployeeService employeeService, IAdvancePaymentService advancePaymentService)
        {
            this.employeeService = employeeService;
            this.advancePaymentService = advancePaymentService;
        }

        public IActionResult AdvancePaymentList()
        {
            ViewBag.Header = "Avans Listesi";
            EmployeeAdvancePaymentVM employeeAdvancePaymentVM = new EmployeeAdvancePaymentVM();
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeeAdvancePaymentVM.AdvancePayments = advancePaymentService.GetAllByCompanyId((int)employee.CompanyId);
            foreach (AdvancePayment item in employeeAdvancePaymentVM.AdvancePayments)
            {
                employeeAdvancePaymentVM.EmployeeFullName = employeeService.GetById(item.EmployeeId).FirstName + " " + employeeService.GetById(item.EmployeeId).LastName;
            }
            return View(employeeAdvancePaymentVM);
        }

        public IActionResult AdvancePaymentCheck()
        {
            EmployeeAdvancePaymentVM employeeAdvancePaymentVM = new EmployeeAdvancePaymentVM();
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeeAdvancePaymentVM.AdvancePayments = advancePaymentService.GetPendingAdvancePayments((int)employee.CompanyId);
            foreach (AdvancePayment item in employeeAdvancePaymentVM.AdvancePayments)
            {
                employeeAdvancePaymentVM.EmployeeFullName = employeeService.GetById(item.EmployeeId).FirstName + " " + employeeService.GetById(item.EmployeeId).LastName;
            }
            return View(employeeAdvancePaymentVM);
        }

        [HttpPost]
        public IActionResult AdvancePaymentCheck(EmployeeAdvancePaymentVM employeeAdvancePaymentVM,int id)
        {
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeeAdvancePaymentVM.AdvancePayments = advancePaymentService.GetPendingAdvancePayments((int)employee.CompanyId);
            
            foreach (AdvancePayment item in employeeAdvancePaymentVM.AdvancePayments)
            {
                if(item.Id == id)
                {
                    item.ReplyState = ReplyState.Onaylandı;
                    item.ReplyDate = DateTime.Now;
                    advancePaymentService.Update(item);
                }                
            }
            employeeAdvancePaymentVM.AdvancePayments = advancePaymentService.GetPendingAdvancePayments((int)employee.CompanyId);
            foreach (AdvancePayment item in employeeAdvancePaymentVM.AdvancePayments)
            {
                employeeAdvancePaymentVM.EmployeeFullName = employeeService.GetById(item.EmployeeId).FirstName + " " + employeeService.GetById(item.EmployeeId).LastName;
            }
            return View(employeeAdvancePaymentVM);
        }

        [HttpPost]
        public IActionResult AdvancePaymentCheckRed(EmployeeAdvancePaymentVM employeeAdvancePaymentVM, int id)
        {
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeeAdvancePaymentVM.AdvancePayments = advancePaymentService.GetPendingAdvancePayments((int)employee.CompanyId);
            foreach (AdvancePayment item in employeeAdvancePaymentVM.AdvancePayments)
            {
                if (item.Id == id)
                {
                    item.ReplyState = ReplyState.OnaylanMAdı;
                    item.ReplyDate = DateTime.Now;
                    advancePaymentService.Update(item);
                }
            }
            employeeAdvancePaymentVM.AdvancePayments = advancePaymentService.GetPendingAdvancePayments((int)employee.CompanyId);
            foreach (AdvancePayment item in employeeAdvancePaymentVM.AdvancePayments)
            {
                employeeAdvancePaymentVM.EmployeeFullName = employeeService.GetById(item.EmployeeId).FirstName + " " + employeeService.GetById(item.EmployeeId).LastName;
            }
            return View("AdvancePaymentCheck",employeeAdvancePaymentVM);
        }
    }
}
