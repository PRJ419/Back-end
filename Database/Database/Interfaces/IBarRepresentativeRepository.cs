namespace Database.Interfaces
{
    public interface IBarRepresentativeRepository : IRepository<BarRepresentative>
    {
        /// <summary>
        /// This method is for editing a BarRepresentative entity that's already in the database.
        /// Therefore the key of the edited entity (parameter) has to correspond
        /// to the key of an entity in the database.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited BarRepresentative entity and edits the corresponding entity
        /// in the database.
        /// </param>
        void Edit(BarRepresentative entity);
    }
}