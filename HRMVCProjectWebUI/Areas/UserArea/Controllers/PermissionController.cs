using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectEntities.Concrete.Enums;
using HRMVCProjectWebUI.Areas.UserArea.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HRMVCProjectWebUI.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService permissionService;
        private readonly IPermissionTypeService permissionTypeService;
        private readonly IEmployeeService employeeService;

        public PermissionController(IPermissionService permissionService, IPermissionTypeService permissionTypeService, IEmployeeService employeeService)
        {
            this.permissionService = permissionService;
            this.permissionTypeService = permissionTypeService;
            this.employeeService = employeeService;
        }


        public IActionResult PermissionList(int id)//rename EmployeesPermissions
        {
            ICollection<Permission> permissions = employeeService.GetByIdIncludePermission(id).Permissions;
            return View(permissions);
        }

        public IActionResult PermissionCreate(int id)
        {
            PermissionAndTypesVM permissionAndTypesVM = new PermissionAndTypesVM();
            permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
            permissionAndTypesVM.Employee = employeeService.GetById(id);
            permissionAndTypesVM.EmployeeId = id;
            return View(permissionAndTypesVM);
        }

        [HttpPost]
        public IActionResult PermissionCreate(int id, PermissionAndTypesVM permissionAndTypesVM)
        {
            
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "İzin eklenemedi!";
                permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
                return View(permissionAndTypesVM);
            }
            Permission permission = new Permission();
            permission.StartingDate = permissionAndTypesVM.Permission.StartingDate;
            permission.AdressToGo = permissionAndTypesVM.Permission.AdressToGo;
            permission.PermissionTypeID = permissionAndTypesVM.Permission.PermissionTypeID;

            PermissionType permissionType = permissionTypeService.GetById((int)permissionAndTypesVM.Permission.PermissionTypeID);

            if ( permission.StartingDate.CompareTo(DateTime.Now) != -1 && permission.StartingDate.CompareTo(DateTime.Now) != 0)
            {
                permission.RequestDate = DateTime.Now;
                permission.EndDate = permissionAndTypesVM.Permission.StartingDate.AddDays(permissionType.AllowedDays);
                permission.ReplyState = ReplyState.Beklemede;
                permission.Employees.Add(employeeService.GetById(id));
                permissionService.Add(permission);
                return RedirectToAction("PermissionList", "Permission", new { id });
            }
            else
            {
               ModelState.AddModelError("", "Başlangıç tarihi bugünden sonra olmalıdır.");
                permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
                return View(permissionAndTypesVM);
            }
        }
    }
}
