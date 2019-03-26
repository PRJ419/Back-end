using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    // Generic repository that works with all kinds of classes. This is the one that should be used for common methods
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;

        public Repository(DbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(params object [] keys)
        {
            _dbContext.Set<T>().Remove(Get(keys));
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AsEnumerable().ToList();
        }

        // Can take composite keys by typing Get(key1, key2, key3...) there's only the problem that they have to be typed
        // in the exact order they're defined in the fluent api. So for drinks you can't write Get(drinkname, barname)
        // but have to use Get(barname, drinkname) instead.
        public T Get(params object[] keys)
        {
            return _dbContext.Set<T>().Find(keys);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsEnumerable().ToList();
        }
    }
}
