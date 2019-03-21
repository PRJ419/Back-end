namespace Database.Interfaces
{
    public interface IBarEventRepository : IRepository<BarEvent>
    {
        BarEvent Get(string barEvent);
        void Add(BarEvent barEvent);
        void Delete(string barEvent);
        void Edit(BarEvent barEvent);
    }
}