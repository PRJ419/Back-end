using System.Collections.Generic;
using System.Linq;

namespace Database.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Get(string customer);
        IEnumerable<Customer> List();
        void Add(Customer customer);
        void Delete(string customer);
        void Edit(Customer customer);

    }
}