namespace Database.Interfaces
{
    public interface IBarrepresentativeRepository : IRepository<Barrepresentative>
    {
        Barrepresentative Get(Barrepresentative barrepresentative);
        void Add(Barrepresentative barrepresentative);
        void Delete(Barrepresentative barrepresentative);
        void Edit(Barrepresentative barrepresentative);
    }
}