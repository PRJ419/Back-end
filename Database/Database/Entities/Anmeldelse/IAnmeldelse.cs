using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public interface IAnmeldelse
    {
        int BarTryk { get; set; }

        string BarNavn { get; set; }

        string BrugerNavn { get; set; }

        Bar Bar { get; set; }

        Kunde Kunde { get; set; }
    }
}
