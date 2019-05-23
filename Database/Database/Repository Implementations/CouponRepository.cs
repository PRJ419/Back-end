using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        /// <summary>
        /// Takes the database context and sends it to the base class constructor (Repository).
        /// </summary>
        /// <param name="dbContext">
        /// Takes a database context that's gonna be set, so you can access the db.
        /// </param>
        public CouponRepository(BarOMeterContext dbContext) : base(dbContext)
        {
        }


     
        public void Edit(Coupon entity)
        {
            var oldCoupon = Get(entity.BarName, entity.CouponID);

            oldCoupon.ExpirationDate = entity.ExpirationDate;
        }
    }
}
