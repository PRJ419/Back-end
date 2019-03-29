using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(BarOMeterContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Customer entity)
        {
            var oldCustomer = Get(entity.Username);
            oldCustomer.Email = entity.Email;
            oldCustomer.FavoriteBar = entity.FavoriteBar;
            oldCustomer.FavoriteDrink = entity.FavoriteDrink;
        }
    }
}
