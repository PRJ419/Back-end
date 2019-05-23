using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Database.Interfaces;

namespace Database.Interfaces
{
    public interface IBarRepository : IGenericRepository<Bar>
    {
        /// <summary>
        /// This method is for returning X (howManyToReturn parameter) number of bars after skipping
        /// a certain amount of bars. This is primarily used for paging, or dynamic loading.
        /// An empty list is returned, if the parameters are less than 0 (since we can't return
        /// less than zero bars), or if there's no bars to return after skipping Y bars.
        /// </summary>
        /// <param name="howManyToSkip">
        /// How many bars the method should skip, before starting to add the bars to a list.
        /// </param>
        /// <param name="howManyToReturn">
        /// How many bars there should be in the list that's being returned (Or less than, if no more bars).
        /// </param>
        /// <returns>
        /// If successful, returns a list of bars as an IEnumerable.
        /// If unsuccessful(no bars in the db), null.
        /// </returns>
        IEnumerable<Bar> GetXBars(int howManyToSkip, int howManyToReturn);

        /// <summary>
        /// This method returns the bars ordered by rating (best first) and then by barname.
        /// </summary>
        /// <returns>
        /// If successful, returns a list of bars as IEnumerable.
        /// If unsuccessful (no bars in the db), null.
        /// </returns>
        IEnumerable<Bar> GetBestBars();

        /// <summary>
        /// This method returns the bars ordered by rating (worst first) and then by barname.
        /// </summary>
        /// <returns>
        /// If successful, returns a list of bars as IEnumerable.
        /// If unsuccessful (no bars in the db), null.
        /// </returns>
        IEnumerable<Bar> GetWorstBars();

        /// <summary>
        /// This method is for editing a bar entity that's already in the database.
        /// If the key of the edited bar doesn't exist in the db, then the method fails.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited bar entity as parameter. Therefore the key has to already exist in the db.
        /// </param>
        void Edit(Bar entity);

    }
}
