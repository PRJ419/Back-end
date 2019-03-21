using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Interfaces
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Coupon Get(string coupon);
        IEnumerable<Coupon> List();
        IEnumerable<Coupon> List(Expression<Func<Coupon, bool>> predicate);
        void Add(Coupon coupon);
        void Delete(string coupon);
        void Edit(Coupon coupon);
    }
}