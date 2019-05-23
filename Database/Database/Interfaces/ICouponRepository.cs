using Database.Entities;

namespace Database.Interfaces
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {
        /// <summary>
        /// This function is for editing a coupon already existing in the database.
        /// Therefore the keys of the edited entity (parameter) has to correspond to the ones
        /// of an entity in the database.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited entity as parameter. If the keys of the edited entity doesn't exist
        /// in the database, then the function fails.
        /// </param>
        void Edit(Coupon edit);
    }
}