using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Review Get(string review);
        IEnumerable<Review> List();
        IEnumerable<Review> List(Expression<Func<Review, bool>> predicate);
        void Add(Review review);
        void Delete(string review);
        void Edit(Review review);
    }
}

