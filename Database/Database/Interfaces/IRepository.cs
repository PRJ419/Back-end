namespace Database.Interfaces
{
    public interface IRepository<T>
    {
        T Get(string name);
        void Add(T entity);
        void Delete(string name);
        void Edit(T entity);
    }
}