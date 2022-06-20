using DataAccess.Repositories;
using DataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Abstract
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Employee GetByIdIncludePermission(int id);
        //Employee GetByEmailAndPassword(string email, string password);
       // Employee GetByEmailAndPassword(string email, string password);
        Employee GetById(int id);
        Employee GetByIdIncludeCosts(int id);
    }
}
