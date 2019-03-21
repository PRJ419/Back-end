using System.Collections.Generic;
using System.Linq;

namespace Database.Interfaces
{
    public interface IBarrepresentativeRepository : IRepository<Barrepresentative>
    {
        Barrepresentative Get(string barrepresentative);
        IEnumerable<Barrepresentative> List();
        void Add(Barrepresentative barrepresentative);
        void Delete(string barrepresentative);
        void Edit(Barrepresentative barrepresentative);
    }
}