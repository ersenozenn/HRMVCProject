using Core.Entities;
using DataAccess.Repositories.Abstract;
using HRMVCProjectBusiness.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectBusiness.Services.Concrete
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IRepository<T> repository;

        public GenericService(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public bool Add(T entity)
        {
            if (entity != null)
            {
                return repository.Add(entity);
            }
            return false;
        }

        public bool Delete(T entity)
        {
            if (entity != null)
            {
                return repository.Delete(entity);
            }
            return false;
        }

        public IEnumerable<T> GetAll()
        {
            return repository.GetAll();

        }

        //public T GetById(int id)
        //{
        //    if (id >= 0)
        //    {
        //        return repository.GetById(id);
        //    }
        //    else throw new Exception();
        //}

        public bool Update(T entity)
        {
            if (entity != null)
            {
                return repository.Update(entity);
            }
            return false;
        }
    }
}
