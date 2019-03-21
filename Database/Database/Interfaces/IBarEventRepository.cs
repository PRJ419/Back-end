namespace Database.Interfaces
{
    public interface IBarEventRepository : IRepository<BarEvent>
    {
        BarEvent Get(BarEvent barEvent);
        void Add(BarEvent barEvent);
        void Delete(BarEvent barEvent);
        void Edit(BarEvent barEvent);
    }
}