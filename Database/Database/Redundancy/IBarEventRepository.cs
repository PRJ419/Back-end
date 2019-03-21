using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Interfaces
{
    public interface IBarEventRepository : IRepository<BarEvent>
    {
        BarEvent Get(string barEvent);
        IEnumerable<BarEvent> List();
        IEnumerable<BarEvent> List(Expression<Func<BarEvent, bool>> predicate);
        void Add(BarEvent barEvent);
        void Delete(string barEvent);
        void Edit(BarEvent barEvent);
    }
}