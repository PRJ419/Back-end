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
        public BarRepresentativeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Edit(BarRepresentative entity)
        {
            var oldBarRep = Get(entity.Username);

            oldBarRep.Name = entity.Name;
            
        }
    }
}
