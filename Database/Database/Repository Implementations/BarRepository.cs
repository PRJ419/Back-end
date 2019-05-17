using System.Collections.Generic;
using System.Linq;
using Database.Interfaces;

namespace Database.Repository_Implementations
{
    public class BarRepository : GenericRepository<Bar>, IBarRepository
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

        
        public IEnumerable<Bar> GetXBars(int howManyToSkip, int howManyToReturn)
        {
            if (howManyToSkip < 0 || howManyToReturn <= 0)
            {
                var emptyList = new List<Bar>();
                return emptyList;
            }
            return _dbContext.Set<Bar>().Skip(howManyToSkip).Take(howManyToReturn).AsEnumerable().ToList();
        }

        
        public IEnumerable<Bar> GetBestBars()
        {
            return _dbContext.Set<Bar>().OrderByDescending(b => b.AvgRating).ThenBy(b=>b.BarName).AsEnumerable().ToList();
        }

        
        public IEnumerable<Bar> GetWorstBars()
        {
            return _dbContext.Set<Bar>().OrderBy(b => b.AvgRating).ThenBy(b=>b.BarName).AsEnumerable().ToList();
        }


        
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
