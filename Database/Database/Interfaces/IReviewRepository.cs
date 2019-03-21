namespace Database.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Review Get(Review review);
        void Add(Review review);
        void Delete(Review review);
        void Edit(Review review);
    }
}