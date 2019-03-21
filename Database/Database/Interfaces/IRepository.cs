using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Interfaces
{
    public interface IRepository<T>
    {
        T Get(string name);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(string name);
        void Edit(T entity);
    }
}