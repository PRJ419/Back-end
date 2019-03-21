using System.Collections.Generic;
using System.Linq;

namespace Database.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Review Get(string review);
        IEnumerable<Review> List();
        void Add(Review review);
        void Delete(string review);
        void Edit(Review review);
    }
}

