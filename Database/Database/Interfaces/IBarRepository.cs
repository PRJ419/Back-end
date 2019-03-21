namespace Database.Interfaces
{
    public interface IBarRepository : IRepository<Bar>
    {
        Bar Get(string bar);
        void Add(Bar bar);
        void Delete(Bar bar);
        void Edit(Bar bar);
    }
}