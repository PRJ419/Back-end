using Database.Entities;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class DrinkRepository : GenericRepository<Drink>, IDrinkRepository
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

        
        public void Edit(Drink entity)
        {
            var OldDrink = Get(entity.BarName, entity.DrinksName);
            OldDrink.Price = entity.Price;
            OldDrink.Image = entity.Image;
        }
    }
}