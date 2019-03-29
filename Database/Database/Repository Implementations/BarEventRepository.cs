using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarEventRepository : Repository<BarEvent>
    {
        public BarEventRepository(DbContext dbContext) : base(dbContext)
        {

        }


        public void Edit(BarEvent entity)
        {
            var OldEvent = Get(entity.BarName, entity.EventName);
            OldEvent.Date = entity.Date;
        }
    }
}