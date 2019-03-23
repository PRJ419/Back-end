using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Interfaces;

namespace Database.Redundancy
{
    public interface IBarRepository : IRepository<Bar>
    {
        // Tilføjet til noget som Andreas tester
        IEnumerable<Bar> GetXBars(int from, int to);

        IEnumerable<Bar> GetBestBars();

        IEnumerable<Bar> GetWorstBars();

    }
}
