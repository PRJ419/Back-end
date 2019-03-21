using System.Collections.Generic;
using System.Linq;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class DrinkRepository : IDrinkRepository
    {
        private DbContext _dbContext;

        public DrinkRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Drink Get(string drink)
        {
            return _dbContext.Set<Drink>().Find(drink);
        }

        public IEnumerable<Drink> List()
        {
            return _dbContext.Set<Drink>().AsEnumerable();
        }

        public void Add(Drink drink)
        {
            _dbContext.Set<Drink>().Add(drink);
            _dbContext.SaveChanges();
        }

        public void Delete(string drink)
        {
            _dbContext.Set<Drink>().Remove(Get(drink));
            _dbContext.SaveChanges();
        }

        public void Edit(Drink drink)
        {
            _dbContext.Entry(drink).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}