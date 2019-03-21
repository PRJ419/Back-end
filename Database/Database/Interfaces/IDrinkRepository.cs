namespace Database.Interfaces
{
    public interface IDrinkRepository : IRepository<Drink>
    {
        Drink Get(Drink drink);
        void Add(Drink drink);
        void Delete(Drink drink);
        void Edit(Drink drink);
    }
}