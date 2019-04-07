using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class ReviewRepository : Repository<Review>
    {
        /// <summary>
        /// Takes the database context and sends it to the base class constructor (Repository).
        /// </summary>
        /// <param name="dbContext">
        /// Takes a database context that's gonna be set, so you can access the db.
        /// </param>
        public ReviewRepository(BarOMeterContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// This method is for editing a review entity by finding the corresponding entity in the database
        /// and setting the changeable properties equal to the edited ones.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited review as a parameter. If the keys of the review isn't in the database, the method fails.
        /// </param>
        public void Edit(Review entity)
        {
            var oldReview = Get(entity.BarName, entity.Username);

            oldReview.BarPressure = entity.BarPressure;
        }
    }
}
