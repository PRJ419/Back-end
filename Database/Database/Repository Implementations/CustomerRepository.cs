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
        /// <summary>
        /// Takes the database context and sends it to the base class constructor (Repository).
        /// </summary>
        /// <param name="dbContext">
        /// Takes a database context that's gonna be set, so you can access the db.
        /// </param>
        public CustomerRepository(BarOMeterContext dbContext) : base(dbContext)
        {
        }


        /// <summary>
        /// This function is for editing a customer entity in the database. If the keys of the
        /// parameter entity doesn't exist in the database, this function fails.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited customer as a parameter. It is therefore a precondition,
        /// that the keys of the customer hasn't been changed.
        /// </param>
        public void Edit(Customer entity)
        {
            var oldCustomer = Get(entity.Username);
            oldCustomer.Email = entity.Email;
            oldCustomer.FavoriteBar = entity.FavoriteBar;
            oldCustomer.FavoriteDrink = entity.FavoriteDrink;
        }
    }
}
