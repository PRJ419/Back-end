using System.Collections.Generic;
using System.Linq;
using Database.Redundancy;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarRepository : Repository<Bar>, IBarRepository
    {
        public BarRepository(DbContext dbcontext) : base(dbcontext)
        {
        }

            /*
            Der returneres ikke IQueryable da det ikke skal være muligt i lagene længere oppe, at lave queries på det vi sender.
            Det er vores job at query databasen, samt sørge for at der ikke bliver queriet på ting som der ikke er ment til at kunne
            queries på.
            */
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
        }
    }
}
