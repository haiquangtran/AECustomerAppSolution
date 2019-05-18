using AE.CustomerApp.Domain.Interfaces;
using AE.CustomerApp.Domain.Models;
using AE.CustomerApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AE.CustomerApp.Infra.Data.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly CustomerAppDbContext _customerAppDbContext;

        public BaseRepository(CustomerAppDbContext customerAppDbContext)
        {
            _customerAppDbContext = customerAppDbContext;
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
