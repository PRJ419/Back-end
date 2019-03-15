using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities.Barrepræsentant
{
    interface IBarrepræsentant
    {

        string BrugerNavn { get; set; }

        string Navn { get; set; }

        string BarNavn { get; set; }

        string Password { get; set; }

    }
}
