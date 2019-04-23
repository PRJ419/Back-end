using System.Collections.Generic;
using System.Linq;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarRepository : Repository<Bar>, IBarRepository
    {
        /// <summary>
        /// Takes the database context and sends it to the base class constructor (Repository).
        /// </summary>
        /// <param name="dbContext">
        /// Takes a database context that's gonna be set, so you can access the db.
        /// </param>
        public BarRepository(BarOMeterContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// This method is for returning X (howManyToReturn parameter) number of bars after skipping
        /// a certain amount of bars. This is primarily used for paging, or dynamic loading.
        /// An empty list is returned, if the parameters are less than 0 (since we can't return
        /// less than zero bars), or if there's no bars to return after skipping Y bars.
        /// </summary>
        /// <param name="howManyToSkip">
        /// How many bars the method should skip, before starting to add the bars to a list.
        /// </param>
        /// <param name="howManyToReturn">
        /// How many bars there should be in the list that's being returned (Or less than, if no more bars).
        /// </param>
        /// <returns>
        /// If successful, returns a list of bars as an IEnumerable.
        /// If unsuccessful(no bars in the db), null.
        /// </returns>
        public IEnumerable<Bar> GetXBars(int howManyToSkip, int howManyToReturn)
        {
            if (howManyToSkip < 0 || howManyToReturn <= 0)
            {
                var emptyList = new List<Bar>();
                return emptyList;
            }
            return _dbContext.Set<Bar>().Skip(howManyToSkip).Take(howManyToReturn).AsEnumerable().ToList();
        }

        /// <summary>
        /// This method returns the bars ordered by rating (best first) and then by barname.
        /// </summary>
        /// <returns>
        /// If successful, returns a list of bars as IEnumerable.
        /// If unsuccessful (no bars in the db), null.
        /// </returns>
        public IEnumerable<Bar> GetBestBars()
        {
            return _dbContext.Set<Bar>().OrderByDescending(b => b.AvgRating).ThenBy(b=>b.BarName).AsEnumerable().ToList();
        }

        /// <summary>
        /// This method returns the bars ordered by rating (worst first) and then by barname.
        /// </summary>
        /// <returns>
        /// If successful, returns a list of bars as IEnumerable.
        /// If unsuccessful (no bars in the db), null.
        /// </returns>
        public IEnumerable<Bar> GetWorstBars()
        {
            return _dbContext.Set<Bar>().OrderBy(b => b.AvgRating).ThenBy(b=>b.BarName).AsEnumerable().ToList();
        }


        /// <summary>
        /// This method is for editing a bar entity that's already in the database.
        /// If the key of the edited bar doesn't exist in the db, then the method fails.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited bar entity as parameter. Therefore the key has to already exist in the db.
        /// </param>
        public void Edit(Bar entity)
        {
            var oldBar = Get(entity.BarName);

            // Assign the changes to the entity from the database, so it's edited
            oldBar.Address = entity.Address;
            oldBar.AvgRating = entity.AvgRating;
            oldBar.Educations = entity.Educations;
            oldBar.LongDescription = entity.LongDescription;
            oldBar.ShortDescription = entity.ShortDescription;
            oldBar.AgeLimit = entity.AgeLimit;
            oldBar.Email = entity.Email;
            oldBar.PhoneNumber = entity.PhoneNumber;
            oldBar.Image = entity.Image;
        }
    }
}
