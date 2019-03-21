﻿using System.Collections.Generic;
using System.Linq;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarEventRepository : IBarEventRepository
    {
        private DbContext _dbContext;

        public BarEventRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BarEvent Get(string barEvent)
        {
            return _dbContext.Set<BarEvent>().Find(barEvent);
        }

        public IEnumerable<BarEvent> List()
        {
            return _dbContext.Set<BarEvent>().AsEnumerable();
        }

        public void Add(BarEvent barEvent)
        {
            _dbContext.Set<BarEvent>().Add(barEvent);
            _dbContext.SaveChanges();
        }

        public void Delete(string barEvent)
        {
            _dbContext.Set<BarEvent>().Remove(Get(barEvent));
            _dbContext.SaveChanges();
        }

        public void Edit(BarEvent barEvent)
        {
            _dbContext.Entry(barEvent).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}