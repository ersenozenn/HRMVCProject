﻿using DataAccess.Repositories;
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
    public class AdvancePaymentRepository : GenericRepository<AdvancePayment>, IAdvancePaymentRepository
    {
        private readonly HRMVCProjectDbContext db;

        public AdvancePaymentRepository(HRMVCProjectDbContext db) : base(db)
        {
            this.db = db;
        }

        public bool AddAdvancePayment(AdvancePayment advance, Employee employee)
        {           
            var _employee = db.Employees.FirstOrDefault(x=>x.Id==employee.Id);
            //var replydatecontrol = employee.AdvancePayments.Where(x => x.ReplyDate < DateTime.Now.AddMonths(-3));
            //var lastAdvance=employee.AdvancePayments.LastOrDefault();
            //var dateControl =advance.ReplyDate- lastAdvance.ReplyDate;
            //var _advancelist = _employee.AdvancePayments;
            if (advance.Amount<_employee.Wage*0.3)
            {
                //if(dateControl.TotalDays>90)//son izin tarihi ile arasında 3 ay olacak kontrolü
                //{
                    _employee.AdvancePayments.Add(advance);
                    return db.SaveChanges() > 0;
                //}                
            }
            return false;           
                
        }

        public IEnumerable<AdvancePayment> AdvancePaymentList(int id)
        {
            return db.AdvancePayment.Where(x=>x.EmployeeId==id).ToList();
        }
    }
}
