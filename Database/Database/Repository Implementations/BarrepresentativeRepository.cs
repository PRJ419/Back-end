using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarRepresentativeRepository : Repository<BarRepresentative>
    {
        /// <summary>
        /// Takes the database context and sends it to the base class constructor (Repository).
        /// </summary>
        /// <param name="dbContext">
        /// Takes a database context that's gonna be set, so you can access the db.
        /// </param>
        public BarRepresentativeRepository(BarOMeterContext dbContext) : base(dbContext)
        {
        }


        /// <summary>
        /// This method is for editing a BarRepresentative entity that's already in the database.
        /// Therefore the key of the edited entity (parameter) has to correspond
        /// to the key of an entity in the database.
        /// </summary>
        /// <param name="entity">
        /// Takes an edited BarRepresentative entity and edits the corresponding entity
        /// in the database.
        /// </param>
        public void Edit(BarRepresentative entity)
        {
            var oldBarRep = Get(entity.Username);

            oldBarRep.Name = entity.Name;
            
        }
    }
}
