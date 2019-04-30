namespace Database.Interfaces
{
    public interface IDrinkRepository : IRepository<Drink>
    {
        /// <summary>
        /// This function is for editing an already existing drink in the database.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited drink as a parameter. It's a precondition that the keys of the drink
        /// haven't been changed.
        /// </param>
        void Edit(Drink entity);
    }
}