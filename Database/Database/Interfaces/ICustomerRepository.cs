namespace Database.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Get(string customer);
        void Add(Customer customer);
        void Delete(string customer);
        void Edit(Customer customer);

    }
}