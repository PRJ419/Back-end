using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarrepresentativeRepository : IRepository<Barrepresentative>
    {
        private DbContext _dbContext;

        public BarrepresentativeRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Barrepresentative Get(string name)
        {
            return _dbContext.Set<Barrepresentative>().Find(name);
        }

        public IEnumerable<Barrepresentative> List()
        {
            return _dbContext.Set<Barrepresentative>().AsEnumerable();
        }

        public void Add(Barrepresentative name)
        {
            _dbContext.Set<Barrepresentative>().Add(name);
            _dbContext.SaveChanges();
        }

        public void Delete(string name)
        {
            _dbContext.Set<Barrepresentative>().Remove(Get(name));
            _dbContext.SaveChanges();
        }

        public void Edit(Barrepresentative name)
        {
            _dbContext.Entry(name).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Barrepresentative> List(Expression<Func<Barrepresentative, bool>> predicate)
        {
            return _dbContext.Set<Barrepresentative>()
                .Where(predicate)
                .AsEnumerable();
        }
    }
}