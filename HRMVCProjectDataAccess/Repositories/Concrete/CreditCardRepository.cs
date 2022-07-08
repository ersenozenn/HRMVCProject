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
    public class CreditCardRepository : GenericRepository<CreditCard> , ICreditCardRepository
    {
        private readonly HRMVCProjectDbContext db;
        public CreditCardRepository(HRMVCProjectDbContext db):base(db)
        {
            this.db = db;
        }
        public CreditCard GetById(int id)
        {
            return db.CreditCards.FirstOrDefault(a => a.Id == id);

        }
    }
}
