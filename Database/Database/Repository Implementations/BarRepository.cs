﻿using System.Collections.Generic;
using System.Linq;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarRepository : IBarRepository
    {
        private DbContext _dbContext;

        public BarRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Bar Get(string bar)
        {
            return _dbContext.Set<Bar>().Find(bar);
        }

        public IEnumerable<Bar> List()
        {
            return _dbContext.Set<Bar>().AsEnumerable();
        }

        public void Add(Bar bar)
        {
            _dbContext.Set<Bar>().Add(bar);
            _dbContext.SaveChanges();
        }

        public void Delete(string bar)
        {
            _dbContext.Set<Bar>().Remove(Get(bar));
            _dbContext.SaveChanges();
        }

        public void Edit(Bar bar)
        {
            _dbContext.Entry(bar).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}