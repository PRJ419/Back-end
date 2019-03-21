using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Get(string customer);
        IEnumerable<Customer> List();
        IEnumerable<Customer> List(Expression<Func<Customer, bool>> predicate);
        void Add(Customer customer);
        void Delete(string customer);
        void Edit(Customer customer);

    }
}