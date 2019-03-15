using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public interface IBar
    {
        
        string BarNavn { get; set; }

        
        string Adresse { get; set; }


        int Aldersgrænse { get; set; }

        string Uddannelser { get; set; }

        List<Drikkevare> Drikkevarer { get; set; }
        List<BarEvent> BarEvents { get; set; }
        List<RabatKupon> RabatKuponer { get; set; }
        List<Barrepræsentant> Barrepræsentanter { get; set; }
        List<Anmeldelse> Anmeldelser { get; set; }
    }
}