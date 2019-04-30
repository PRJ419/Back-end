namespace Database.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        /// <summary>
        /// This method is for editing a review entity by finding the corresponding entity in the database
        /// and setting the changeable properties equal to the edited ones.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited review as a parameter. If the keys of the review isn't in the database, the method fails.
        /// </param>
        void Edit(Review entity);
    }
}