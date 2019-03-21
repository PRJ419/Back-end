using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Interfaces
{
    public interface IDrinkRepository : IRepository<Drink>
    {
        Drink Get(string drink);
        IEnumerable<Drink> List();
        IEnumerable<Drink> List(Expression<Func<Drink, bool>> predicate);
        void Add(Drink drink);
        void Delete(string drink);
        void Edit(Drink drink);
    }
}