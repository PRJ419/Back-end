using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Redundancy;

namespace Database.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBarRepository Bars { get; }

        int Complete();
    }
}
