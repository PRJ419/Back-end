using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    class CouponRepository : Repository<Coupon>
    {
        public CouponRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Coupon entity)
        {
            var oldCoupon = Get(entity.CouponID, entity.BarName);

            oldCoupon.ExpirationDate = entity.ExpirationDate;
        }
    }
}
