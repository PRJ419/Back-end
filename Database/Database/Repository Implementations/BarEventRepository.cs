using Database.Entities;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarEventRepository : GenericRepository<BarEvent>, IBarEventRepository
    {
        /// <summary>
        /// Takes the database context and sends it to the base class constructor (Repository).
        /// </summary>
        /// <param name="dbContext">
        /// Takes a database context that's gonna be set, so you can access the db.
        /// </param>
        public BarEventRepository(BarOMeterContext dbContext) : base(dbContext)
        {

        }

        
        public void Edit(BarEvent entity)
        {
            var OldEvent = Get(entity.BarName, entity.EventName);
            OldEvent.Date = entity.Date;
            OldEvent.Image = entity.Image;
        }
    }
}