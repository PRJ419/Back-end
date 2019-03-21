using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class CouponRepository : ICouponRepository
    {
        private DbContext _dbContext;

        public CouponRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Coupon Get(string coupon)
        {
            return _dbContext.Set<Coupon>().Find(coupon);
        }

        public void Add(Coupon coupon)
        {
            _dbContext.Set<Coupon>().Add(coupon);
            _dbContext.SaveChanges();
        }

        public void Delete(string coupon)
        {
            _dbContext.Set<Coupon>().Remove(Get(coupon));
            _dbContext.SaveChanges();
        }

        public void Edit(Coupon coupon)
        {
            _dbContext.Entry(coupon).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}