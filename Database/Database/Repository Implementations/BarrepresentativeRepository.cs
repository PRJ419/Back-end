using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarrepresentativeRepository : IBarrepresentativeRepository
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
    }
}