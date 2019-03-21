using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    class ReviewRepository : IReviewRepository
    {
        private DbContext _dbContext;
        public ReviewRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Review> List(Expression<Func<Review, bool>> predicate)
        {
            return _dbContext.Set<Review>()
                .Where(predicate)
                .AsEnumerable();
        }

        public void Add(Review review)
        {
            _dbContext.Set<Review>().Add(review);
            _dbContext.SaveChanges();
        }

        public void Delete(string review)
        {
            _dbContext.Set<Review>().Remove(Get(review));
            _dbContext.SaveChanges();
        }

        public void Edit(Review review)
        {
            _dbContext.Entry(review).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public Review Get(string review)
        {
            return _dbContext.Set<Review>().Find(review);
        }

        public IEnumerable<Review> List()
        {
            return _dbContext.Set<Review>().AsEnumerable();
        }
    }
}
