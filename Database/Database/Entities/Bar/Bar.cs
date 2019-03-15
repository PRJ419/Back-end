using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Bar : IBar
    {
        
        [MaxLength(150)]
        public string BarNavn { get; set; }

        [Required]
        [MaxLength(255)]
        public string Adresse { get; set; }

        [Required]
        public int Aldersgrænse { get; set; }

        [MaxLength(255)]
        public string Uddannelser { get; set; }

        public List<Drikkevare> Drikkevarer { get; set; }
        public List<BarEvent> BarEvents { get; set; }
        public List<RabatKupon> RabatKuponer { get; set; }
        public List<Barrepræsentant> Barrepræsentanter { get; set; }
        public List<Anmeldelse> Anmeldelser { get; set; }

    }
}