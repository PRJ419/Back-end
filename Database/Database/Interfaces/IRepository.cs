namespace Database.Interfaces
{
    public interface IRepository<T>
    {
        T Get(string name);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}