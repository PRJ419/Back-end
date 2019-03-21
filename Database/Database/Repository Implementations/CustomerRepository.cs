using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private DbContext _dbContext;

        public CustomerRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer Get(string customer)
        {
            return _dbContext.Set<Customer>().Find(customer);
        }

        public IEnumerable<Customer> List()
        {
            return _dbContext.Set<Customer>().AsEnumerable();
        }

        public void Add(Customer customer)
        {
            _dbContext.Set<Customer>().Add(customer);
            _dbContext.SaveChanges();
        }

        public void Delete(string customer)
        {
            _dbContext.Set<Customer>().Remove(Get(customer));
            _dbContext.SaveChanges();
        }

        public void Edit(Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Customer> List(Expression<Func<Customer, bool>> predicate)
        {
            return _dbContext.Set<Customer>()
                .Where(predicate)
                .AsEnumerable();
        }
    }
}