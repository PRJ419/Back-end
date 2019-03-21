using System.Collections.Generic;
using System.Linq;

namespace Database.Interfaces
{
    public interface IBarEventRepository : IRepository<BarEvent>
    {
        BarEvent Get(string barEvent);
        IEnumerable<BarEvent> List();
        void Add(BarEvent barEvent);
        void Delete(string barEvent);
        void Edit(BarEvent barEvent);
    }
}