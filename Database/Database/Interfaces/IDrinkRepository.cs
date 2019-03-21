using System.Collections.Generic;
using System.Linq;

namespace Database.Interfaces
{
    public interface IDrinkRepository : IRepository<Drink>
    {
        Drink Get(string drink);
        IEnumerable<Drink> List();
        void Add(Drink drink);
        void Delete(string drink);
        void Edit(Drink drink);
    }
}