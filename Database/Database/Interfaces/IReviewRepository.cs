namespace Database.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Review Get(string review);
        void Add(Review review);
        void Delete(string review);
        void Edit(Review review);
    }
}

