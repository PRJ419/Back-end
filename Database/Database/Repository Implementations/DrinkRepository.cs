using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class DrinkRepository : Repository<Drink>
    {
        public DrinkRepository(BarOMeterContext dbContext) : base(dbContext)
        {
     
        }

        public void Edit(Drink entity)
        {
            var OldDrink = Get(entity.BarName, entity.DrinksName);
            OldDrink.Price = entity.Price;
        }
    }
}