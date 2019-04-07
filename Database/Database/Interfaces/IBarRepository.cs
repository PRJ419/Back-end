﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Interfaces;

namespace Database.Interfaces
{
    public interface IBarRepository : IRepository<Bar>
    {
        IEnumerable<Bar> GetXBars(int from, int to);

        IEnumerable<Bar> GetBestBars();

        IEnumerable<Bar> GetWorstBars();

        void Edit(Bar entity);

    }
}
