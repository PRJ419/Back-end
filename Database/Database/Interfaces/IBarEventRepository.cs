using Database.Entities;

namespace Database.Interfaces
{
    public interface IBarEventRepository : IGenericRepository<BarEvent>
    {
        /// <summary>
        /// This method is for editing a BarEvent entity that already exists in the db.
        /// Therefore the keys of the edited entity has to correspond to the one in the db.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited BarEvent entity as parameter and finds the entity in the db,
        /// with the same keys.
        /// </param>
        void Edit(BarEvent entity);
    }
}