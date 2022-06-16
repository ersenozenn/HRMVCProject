using Core.Entities;
using DataAccess.Repositories.Abstract;
using HRMVCProjectDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class 
    {
        private readonly HRMVCProjectDbContext db;
        public GenericRepository(HRMVCProjectDbContext db)
        {
            this.db = db;
        }
        public bool Add(T entity)
        {
            try
            {
                db.Set<T>().Add(entity);
                return db.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                db.Set<T>().Remove(entity);
                return db.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>();
        }

        //public T GetById(int id)
        //{
        //    return db.Set<T>().FirstOrDefault(a => a.Id == id);
        //}

        public bool Update(T entity)
        {
            try
            {
                db.Set<T>().Update(entity);
                return db.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
