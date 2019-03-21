using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Interfaces
{
    public interface IBarrepresentativeRepository : IRepository<Barrepresentative>
    {
        Barrepresentative Get(string barrepresentative);
        IEnumerable<Barrepresentative> List();
        IEnumerable<Barrepresentative> List(Expression<Func<Barrepresentative, bool>> predicate);
        void Add(Barrepresentative barrepresentative);
        void Delete(string barrepresentative);
        void Edit(Barrepresentative barrepresentative);
    }
}