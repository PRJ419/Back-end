namespace Database.Interfaces
{
    public interface IBarrepresentativeRepository : IRepository<Barrepresentative>
    {
        Barrepresentative Get(string barrepresentative);
        void Add(Barrepresentative barrepresentative);
        void Delete(string barrepresentative);
        void Edit(Barrepresentative barrepresentative);
    }
}