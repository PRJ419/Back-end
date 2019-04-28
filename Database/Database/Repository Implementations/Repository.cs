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
    // Generic repository that works with all kinds of classes. This is the one that should be used for common methods.
    // Also known as the template pattern
    public class Repository<T> : IRepository<T> where T : class
    {
        // The database context for the entire class
        protected readonly BarOMeterContext _dbContext;

        /// <summary>
        /// Takes the database context and sets it for the repository class, so it's the same throughout the
        /// calls of the class.
        /// </summary>
        /// <param name="dbcontext">
        /// Takes the database context and sets it in the class.
        /// </param>
        public Repository(BarOMeterContext dbcontext)
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
