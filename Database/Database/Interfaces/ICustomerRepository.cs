namespace Database.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Get(Customer customer);
        void Add(Customer customer);
        void Delete(Customer customer);
        void Edit(Customer customer);

    }
}