using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Interfaces;
using Database.Redundancy;
using Database.Repository_Implementations;

namespace Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private BarOMeterContext barContext;

        public IBarRepository Bars { get; private set; }

        public UnitOfWork(BarOMeterContext context)
        {
            barContext = context;
            Bars = new BarRepository(barContext);
        }
        public int Complete()
        {
            return barContext.SaveChanges();
        }

        public void Dispose()
        {
            barContext.Dispose();
        }
    }
}
