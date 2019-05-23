using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        /// <summary>
        /// Takes the database context and sends it to the base class constructor (Repository).
        /// </summary>
        /// <param name="dbContext">
        /// Takes a database context that's gonna be set, so you can access the db.
        /// </param>
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
