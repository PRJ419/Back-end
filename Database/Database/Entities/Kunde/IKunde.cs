using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities.Kunde
{
    interface IKunde
    {

        string BrugerNavn { get; set; }

        string Navn { get; set; }

        DateTime Fødselsdag { get; set; }

        string Email { get; set; }

        string Password { get; set; }

    }
}
