using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class CouponRepository : Repository<Coupon>
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


        /// <summary>
        /// This function is for editing a coupon already existing in the database.
        /// Therefore the keys of the edited entity (parameter) has to correspond to the ones
        /// of an entity in the database.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited entity as parameter. If the keys of the edited entity doesn't exist
        /// in the database, then the function fails.
        /// </param>
        public void Edit(Coupon entity)
        {
            var oldCoupon = Get(entity.CouponID, entity.BarName);

            oldCoupon.ExpirationDate = entity.ExpirationDate;
        }
    }
}
