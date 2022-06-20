using DataAccess.Repositories;
using HRMVCProjectDataAccess.Data;
using HRMVCProjectDataAccess.Repositories.Abstract;
using HRMVCProjectEntities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectDataAccess.Repositories.Concrete
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly HRMVCProjectDbContext db;

        public EmployeeRepository(HRMVCProjectDbContext db ) : base(db)
        {
            this.db = db;
        }
        public Employee GetByIdIncludePermission(int id)
        {
            return db.Employees.Include(x=>x.Permissions).FirstOrDefault(a => a.Id == id);
        }

        //public Employee GetByEmailAndPassword(string email, string password)
        //{
            
        //        return db.Employees.FirstOrDefault(a => a.Email == email && a.Password == password);
            
        //}

        public Employee GetById(int id)
        {
            return db.Employees.FirstOrDefault(a => a.Id == id);

        }
        //public Employee GetByEmailAndPassword(string email, string password)
        //{
        //    return db.Employees.FirstOrDefault(a => a.UserMail == email && a.Password == password);
        //}
        public Employee GetByIdIncludeCosts(int id)
        {
            return db.Employees.Include(a => a.Costs).FirstOrDefault(a => a.Id == id);
        }

    }
}
