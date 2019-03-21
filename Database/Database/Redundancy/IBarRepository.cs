using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Interfaces
{
    public interface IBarRepository : IRepository<Bar>
    {
        Bar Get(string bar);
        IEnumerable<Bar> List();
        IEnumerable<Bar> List(Expression<Func<Bar, bool>> predicate);
        void Add(Bar bar);
        void Delete(string bar);
        void Edit(Bar bar);
    }
}