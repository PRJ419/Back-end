using System.Collections.Generic;
using System.Linq;

namespace Database.Interfaces
{
    public interface IRepository<T>
    {
        T Get(string name);
        IEnumerable<T> List();
        void Add(T entity);
        void Delete(string name);
        void Edit(T entity);
    }
}