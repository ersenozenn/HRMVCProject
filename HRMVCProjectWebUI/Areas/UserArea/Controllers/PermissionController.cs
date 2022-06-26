using FluentValidation.Results;
using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectBusiness.Validators;
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
            ViewBag.Header = "İzin Listesi";
            ICollection<Permission> permissions = employeeService.GetByIdIncludePermission(id).Permissions;
            return View(permissions);
        }

        public IActionResult PermissionCreate(int id)
        {
            ViewBag.Header = "İzin Talebi Oluştur";
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
            permission.EndDate = permissionAndTypesVM.Permission.EndDate;
            permission.AdressToGo = permissionAndTypesVM.Permission.AdressToGo;
            permission.PermissionTypeID = permissionAndTypesVM.Permission.PermissionTypeID;

            Employee employee = employeeService.GetById(id);

            PermissionType permissionType = permissionTypeService.GetById((int)permissionAndTypesVM.Permission.PermissionTypeID);

            PermissionValidator validations = new PermissionValidator();
            ValidationResult validationResult = validations.Validate(permissionAndTypesVM.Permission);

            if ( validationResult.IsValid)
            {
                if (employee.Gender == true && permission.PermissionTypeID == 1)
                {
                    ModelState.AddModelError("", "Erkek çalışanlarımız doğum izni kullanamaz.");
                    permissionAndTypesVM.EmployeeId = id;
                    permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
                    return View(permissionAndTypesVM);
                }
                if (employee.Gender == false && permission.PermissionTypeID == 2)
                {
                    ModelState.AddModelError("", "Kadın çalışanlarımız babalık izni kullanamaz.");
                    permissionAndTypesVM.EmployeeId = id;
                    permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
                    return View(permissionAndTypesVM);
                }
                if (permission.EndDate.CompareTo(permission.StartingDate) <= 0)
                {
                    ModelState.AddModelError("", "İzin bitiş tarihi başlangıç tarihinden önce olamaz.");
                    permissionAndTypesVM.EmployeeId = id;
                    permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
                    return View(permissionAndTypesVM);
                }
                if (permission.StartingDate.CompareTo(DateTime.Now) <= 0)
                {
                    ModelState.AddModelError("", "İzin başlangıç tarihi bugünden önce olamaz.");
                    permissionAndTypesVM.EmployeeId = id;
                    permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
                    return View(permissionAndTypesVM);
                }
                if (permission.EndDate.CompareTo(permission.StartingDate.AddDays(permissionType.AllowedDays + 2)) >=0 )
                {
                    ModelState.AddModelError("", $"İstediğiniz izin için verilen gün sayısı ; {permissionType.AllowedDays}");
                    permissionAndTypesVM.EmployeeId = id;
                    permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
                    return View(permissionAndTypesVM);
                }
                permission.RequestDate = DateTime.Now;
                //permission.EndDate = permissionAndTypesVM.Permission.StartingDate.AddDays(permissionType.AllowedDays);
                
                permission.ReplyState = ReplyState.Beklemede;
                permission.Employees.Add(employeeService.GetById(id));
                permissionService.Add(permission);
                return RedirectToAction("PermissionList", "Permission", new { id });
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage.ToString());
                }
                permissionAndTypesVM.EmployeeId = id;
                permissionAndTypesVM.PermissionTypes = permissionTypeService.GetAll();
                return View(permissionAndTypesVM);
            }
        }
    }
}
