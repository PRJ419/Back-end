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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;

        public GenericRepository(DbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(string name)
        {
            _dbContext.Set<T>().Remove(Get(name));
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AsEnumerable().ToList();
        }

        public T Get(string name)
        {
            return _dbContext.Set<T>().Find(name);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }
    }
}
