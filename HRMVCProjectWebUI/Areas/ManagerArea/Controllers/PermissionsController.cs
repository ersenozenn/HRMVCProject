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
    public class PermissionsController : Controller
    {
        private readonly IPermissionService permissionService;
        private readonly IEmployeeService employeeService;

        public PermissionsController(IPermissionService permissionService,IEmployeeService employeeService)
        {
            this.permissionService = permissionService;
            this.employeeService = employeeService;
        }
        public IActionResult PermissionList()
        {
            ViewBag.Header = "İzin Listesi";
            EmployeePermissionVM employeePermissionVM = new EmployeePermissionVM();
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeePermissionVM.Permissions = permissionService.GetAllByCompanyId((int)employee.CompanyId);
            
            foreach (Permission item in employeePermissionVM.Permissions)
            {
                foreach (Employee item2 in item.Employees)
                {
                    employeePermissionVM.EmployeeFullName = employeeService.GetById(item2.Id).FirstName + " " + employeeService.GetById(item2.Id).LastName;
                }
            }
            return View(employeePermissionVM);
        }

        public IActionResult PermissionCheck()
        {
            EmployeePermissionVM employeePermissionVM = new EmployeePermissionVM();
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeePermissionVM.Permissions = permissionService.GetPendingPermissions((int)employee.CompanyId);
            foreach (Permission item in employeePermissionVM.Permissions)
            {
                foreach (Employee item2 in item.Employees)
                {
                    employeePermissionVM.EmployeeFullName = employeeService.GetById(item2.Id).FirstName + " " + employeeService.GetById(item2.Id).LastName;
                }
            }
            return View(employeePermissionVM);
        }

        [HttpPost]
        public IActionResult PermissionCheck(EmployeePermissionVM employeePermissionVM, int id)
        {
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeePermissionVM.Permissions = permissionService.GetPendingPermissions((int)employee.CompanyId);

            foreach (Permission item in employeePermissionVM.Permissions)
            {
                if (item.Id == id)
                {
                    item.ReplyState = ReplyState.Onaylandı;
                    item.ReplyDate = DateTime.Now;
                    permissionService.Update(item);
                }
            }
            employeePermissionVM.Permissions = permissionService.GetPendingPermissions((int)employee.CompanyId);
            foreach (Permission item in employeePermissionVM.Permissions)
            {
                foreach (Employee item2 in item.Employees)
                {
                    employeePermissionVM.EmployeeFullName = employeeService.GetById(item2.Id).FirstName + " " + employeeService.GetById(item2.Id).LastName;
                }
            }
            return View(employeePermissionVM);
        }

        [HttpPost]
        public IActionResult PermissionCheckRed(EmployeePermissionVM employeePermissionVM, int id)
        {
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("ManagerId"));
            employeePermissionVM.Permissions = permissionService.GetPendingPermissions((int)employee.CompanyId);
            foreach (Permission item in employeePermissionVM.Permissions)
            {
                if (item.Id == id)
                {
                    item.ReplyState = ReplyState.OnaylanMAdı;
                    item.ReplyDate = DateTime.Now;
                    permissionService.Update(item);
                }
            }
            employeePermissionVM.Permissions = permissionService.GetPendingPermissions((int)employee.CompanyId);
            foreach (Permission item in employeePermissionVM.Permissions)
            {
                foreach (Employee item2 in item.Employees)
                {
                    employeePermissionVM.EmployeeFullName = employeeService.GetById(item2.Id).FirstName + " " + employeeService.GetById(item2.Id).LastName;
                }
            }
            return View("PermissionCheck", employeePermissionVM);
        }
    }
}
