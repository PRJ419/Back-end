using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarEventRepository : Repository<BarEvent>
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

        /// <summary>
        /// This method is for editing a BarEvent entity that already exists in the db.
        /// Therefore the keys of the edited entity has to correspond to the one in the db.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited BarEvent entity as parameter and finds the entity in the db,
        /// with the same keys.
        /// </param>
        public void Edit(BarEvent entity)
        {
            var OldEvent = Get(entity.BarName, entity.EventName);
            OldEvent.Date = entity.Date;
            OldEvent.Image = entity.Image;
        }
    }
}