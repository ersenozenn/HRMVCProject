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

        public IActionResult AdvancePaymentList(int companyId)
        {
            ViewBag.Header = "Avans Listesi";
            var advances = advancePaymentService.GetAllByCompanyId(companyId);
            return View(advances);
        }

        public IActionResult AdvancePaymentCheck()
        {
            EmployeeAdvancePaymentVM employeeAdvancePaymentVM = new EmployeeAdvancePaymentVM();
            employeeAdvancePaymentVM.AdvancePayments = advancePaymentService.GetPendingAdvancePayments();
            foreach (AdvancePayment item in employeeAdvancePaymentVM.AdvancePayments)
            {
                employeeAdvancePaymentVM.EmployeeFullName = employeeService.GetById(item.EmployeeId).FirstName + " " + employeeService.GetById(item.EmployeeId).LastName;
            }
            return View(employeeAdvancePaymentVM);
        }

        [HttpPost]
        public IActionResult AdvancePaymentCheck(EmployeeAdvancePaymentVM employeeAdvancePaymentVM,int id)
        {
            employeeAdvancePaymentVM.AdvancePayments = advancePaymentService.GetPendingAdvancePayments();
            
            foreach (AdvancePayment item in employeeAdvancePaymentVM.AdvancePayments)
            {
                if(item.Id == id)
                {
                    item.ReplyState = ReplyState.Onaylandı;
                    item.ReplyDate = DateTime.Now;
                    advancePaymentService.Update(item);
                }                
            }
            return View(employeeAdvancePaymentVM);
        }

        [HttpPost]
        public IActionResult AdvancePaymentCheckRed(EmployeeAdvancePaymentVM employeeAdvancePaymentVM, int id)
        {
            foreach (AdvancePayment item in employeeAdvancePaymentVM.AdvancePayments)
            {
                if (item.Id == id)
                {
                    item.ReplyState = ReplyState.OnaylanMAdı;
                    item.ReplyDate = DateTime.Now;
                    advancePaymentService.Update(item);
                }
            }
            return View(employeeAdvancePaymentVM);
        }
    }
}
