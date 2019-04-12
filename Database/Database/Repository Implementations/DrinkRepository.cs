using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class DrinkRepository : Repository<Drink>
    {
        /// <summary>
        /// Takes the database context and sends it to the base class constructor (Repository).
        /// </summary>
        /// <param name="dbContext">
        /// Takes a database context that's gonna be set, so you can access the db.
        /// </param>
        public DrinkRepository(BarOMeterContext dbContext) : base(dbContext)
        {
     
        }

        /// <summary>
        /// This function is for editing an already existing drink in the database.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited drink as a parameter. It's a precondition that the keys of the drink
        /// haven't been changed.
        /// </param>
        public void Edit(Drink entity)
        {
            var OldDrink = Get(entity.BarName, entity.DrinksName);
            OldDrink.Price = entity.Price;
        }
    }
}