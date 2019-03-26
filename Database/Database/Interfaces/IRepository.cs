using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(params object [] keys);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(params object [] keys);
    }
}
