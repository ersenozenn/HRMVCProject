using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectBusiness.Services.Concrete;
using Core.Entities;
using DataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using HRMVCProjectDataAccess.Repositories.Abstract;

namespace HRMVCProjectBusiness.Services.Concrete
{
    public class EmployeeService : GenericService<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) : base (employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public Employee GetByIdIncludePermission(int id)
        {
            if (id >= 0)
            {
                return employeeRepository.GetByIdIncludePermission(id);
            }
            else throw new Exception("ID Hatalı");
        }

        //public Employee GetByEmailAndPassword(string email, string password)
        //{          
            
        //        return employeeRepository.GetByEmailAndPassword(email, password);
            
        //}

        public Employee GetById(int id)
        {
            return employeeRepository.GetById(id);
        }

        public Employee GetByIdIncludeCosts(int id)
        {
            return employeeRepository.GetByIdIncludeCosts(id);
        }

        public bool CheckIdentity(string identity)
        {
            return employeeRepository.CheckIdentity(identity);
        }

        public Employee GetByManagerIdIncludeCreditCards(int ManagerId)
        {
            return employeeRepository.GetByManagerIdIncludeCreditCards(ManagerId);
        }

        //public Employee GetByEmailAndPassword(string email, string password)
        //{
        //    if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
        //    {
        //        return employeeRepository.GetByEmailAndPassword(email, password);
        //    }
        //    else throw new Exception();
        //}

    }
}
