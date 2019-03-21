using System.Collections.Generic;
using System.Linq;

namespace Database.Interfaces
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Coupon Get(string coupon);
        IEnumerable<Coupon> List();
        void Add(Coupon coupon);
        void Delete(string coupon);
        void Edit(Coupon coupon);
    }
}