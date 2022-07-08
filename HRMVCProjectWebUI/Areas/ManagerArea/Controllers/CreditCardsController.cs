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
using HRMVCProjectBusiness.Validators;
using FluentValidation.Results;

namespace HRMVCProjectWebUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class CreditCardsController : Controller
    {
        private readonly ICreditCardService creditCardService;
        private readonly IEmployeeService employeeService;

        public CreditCardsController(ICreditCardService creditCardService, IEmployeeService employeeService )
        {
            this.creditCardService = creditCardService;
            this.employeeService = employeeService;
        }

        // GET: ManagerArea/CreditCards
        public IActionResult Index()
        {
            Employee employee = employeeService.GetByManagerIdIncludeCreditCards((int)HttpContext.Session.GetInt32("ManagerId"));
            return View(employee.CreditCards);
        }

        // GET: ManagerArea/CreditCards/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            CreditCard creditCard = creditCardService.GetById(id);

            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // GET: ManagerArea/CreditCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManagerArea/CreditCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreditCard creditCard)
        {
            CreditCardValidator validations = new CreditCardValidator();
            ValidationResult validationResult = validations.Validate(creditCard);
            if (validationResult.IsValid)
            {
                creditCard.EmployeeId = (int)HttpContext.Session.GetInt32("ManagerId");
                creditCard.IsActive = true;
                if (ModelState.IsValid)
                {
                    creditCardService.Add(creditCard);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Bilgiler hatalı.");
                return View(creditCard);
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage.ToString());
                    
                }
            }
            return View(creditCard);
        }

        // GET: ManagerArea/CreditCards/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            CreditCard creditCard = creditCardService.GetById(id);
            if (creditCard == null)
            {
                return NotFound();
            }
            return View(creditCard);
        }

        // POST: ManagerArea/CreditCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreditCard creditCard)
        {
            if (id != creditCard.Id)
            {
                return NotFound();
            }
            CreditCardValidator validations = new CreditCardValidator();
            ValidationResult validationResult = validations.Validate(creditCard);
            if (validationResult.IsValid)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        creditCard.EmployeeId = (int)HttpContext.Session.GetInt32("ManagerId");
                        creditCardService.Update(creditCard);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CreditCardExists(creditCard.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Hata oluştu.");
                return View(creditCard);
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage.ToString());

                }
            }
            return View(creditCard);
        }

        // GET: ManagerArea/CreditCards/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            CreditCard creditCard = creditCardService.GetById(id);

            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // POST: ManagerArea/CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            CreditCard creditCard = creditCardService.GetById(id);
            creditCardService.Delete(creditCard);
            return RedirectToAction(nameof(Index));
        }

        private bool CreditCardExists(int id)
        {
            return creditCardService.GetById(id)!=null;
        }
    }
}
