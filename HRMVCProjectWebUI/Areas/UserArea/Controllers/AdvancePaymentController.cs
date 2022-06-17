using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectEntities.Concrete.Enums;
using HRMVCProjectWebUI.Areas.UserArea.Models;
using HRMVCProjectWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HRMVCProjectWebUI.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class AdvancePaymentController : Controller
    {
        private readonly IAdvancePaymentService advancePaymentService;
        private readonly IEmployeeService employeeService;

        public AdvancePaymentController(IAdvancePaymentService advancePaymentService, IEmployeeService employeeService)
        {
            this.advancePaymentService = advancePaymentService;
            this.employeeService = employeeService;
        }

        [HttpGet]
        //[Route("AvansTalebi/{id:int}")]
        public IActionResult AdvancePaymentCreate(int id)
        {
            EmployeeAdvanceVM employeeAdvanceVM = new EmployeeAdvanceVM();
            employeeAdvanceVM.Employee = employeeService.GetById(id);

            return View(employeeAdvanceVM);
        }

        [HttpPost]
        public IActionResult AdvancePaymentCreate(int id, EmployeeAdvanceVM employeeAdvanceVM)
        {
            if (ModelState.IsValid)
            {
                //if(employeeAdvanceVM.AdvancePayment.Amount>employeeAdvanceVM.Employee.Wage*0.3)
                //{
                //    TempData["ErrorMessage"] = "Maaşınızın en fazla %30 u kadar avans talebinde bulunabilirsiniz!!";
                //}
                employeeAdvanceVM.Employee = employeeService.GetById(id);
                employeeAdvanceVM.AdvancePayment.ReplyState = ReplyState.Beklemede;
                employeeAdvanceVM.AdvancePayment.ReplyDate = DateTime.Now;
                advancePaymentService.AddAdvancePayment(employeeAdvanceVM.AdvancePayment, employeeAdvanceVM.Employee);
                return RedirectToAction("AdvancePaymentList", "AdvancePayment");//??
            }
            return View();
        }

        //[Route("AvansListesi/{id:int}")]
        public IActionResult AdvancePaymentList(int id)
        {
            //var employee = employeeService.GetById(id);
            var advances = advancePaymentService.AdvancePaymentList(id);
            return View(advances);
        }

        //[Route("AvansDuzenle/{id:int}")]
        //public IActionResult AdvancePaymentUpdate(int id)
        //{
        //   var advance= advancePaymentService.GetById(id);
        //    return View(advance);

        //}

        [HttpPut]//yapılma aşamasında
        public IActionResult AdvancePaymentUpdate(AdvancePayment advance)
        {
            if (ModelState.IsValid)
            {
                advancePaymentService.Update(advance);
                return RedirectToAction(nameof(AdvancePaymentList));
            }
            return NotFound();//bu da böyle olsun :)

        }

    }
}
