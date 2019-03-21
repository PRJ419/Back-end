using System.Collections.Generic;
using System.Linq;

namespace Database.Interfaces
{
    public interface IBarRepository : IRepository<Bar>
    {
        Bar Get(string bar);
        IEnumerable<Bar> List();
        void Add(Bar bar);
        void Delete(string bar);
        void Edit(Bar bar);
    }
}