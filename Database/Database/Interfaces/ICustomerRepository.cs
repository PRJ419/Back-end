namespace Database.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        /// <summary>
        /// This function is for editing a customer entity in the database. If the keys of the
        /// parameter entity doesn't exist in the database, this function fails.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited customer as a parameter. It is therefore a precondition,
        /// that the keys of the customer hasn't been changed.
        /// </param>
        void Edit(Customer entity);
    }
}