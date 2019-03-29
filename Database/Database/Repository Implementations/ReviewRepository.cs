using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    class ReviewRepository : Repository<Review>
    {
        public ReviewRepository(DbContext dbcontext) : base(dbcontext)
        {
        }

        public void Edit(Review entity)
        {
            var oldReview = Get(entity.BarName, entity.Username);

            oldReview.BarPressure = entity.BarPressure;
        }
    }
}
