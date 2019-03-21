namespace Database.Interfaces
{
    public interface IDrinkRepository : IRepository<Drink>
    {
        Drink Get(string drink);
        void Add(Drink drink);
        void Delete(string drink);
        void Edit(Drink drink);
    }
}