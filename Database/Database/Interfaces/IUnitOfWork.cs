using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Repository_Implementations;

namespace Database.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBarRepository BarRepository { get; }

        IBarEventRepository BarEventRepository { get; }

        IBarRepresentativeRepository BarRepRepository { get; }

        ICouponRepository CouponRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        IDrinkRepository DrinkRepository { get; }

        IReviewRepository ReviewRepository { get; }

        /// <summary>
        /// Used in conjunction with changes made to the database, is called to save the changes made
        /// </summary>
        /// <returns>
        /// Returns the number of changes made to the database
        /// </returns>
        int Complete();

        /// <summary>
        /// Updates the current rating of a given bar and calculates the average rating of said bar
        /// </summary>
        /// <param name="barID">
        /// Id of the chosen bar, in this case the name of the bar
        /// </param>
        void UpdateBarRating(string barID);
    }
}
