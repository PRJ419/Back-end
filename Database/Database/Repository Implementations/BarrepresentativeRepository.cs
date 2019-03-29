using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarrepresentativeRepository : Repository<Barrepresentative>
    {
        public BarrepresentativeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Barrepresentative entity)
        {
            var oldBarrep = Get(entity.Username);

            oldBarrep.Name = entity.Name;
            
        }
    }
}
