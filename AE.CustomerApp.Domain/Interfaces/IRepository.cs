using AE.CustomerApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AE.CustomerApp.Domain.Interfaces
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        
        IEnumerable<T> GetAll();

        T GetById(int id);
        
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Save();
    }
}
