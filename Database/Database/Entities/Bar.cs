using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities;
using Database.Entities.BarEvent;
using Database.Entities.Barrepræsentant;
using Database.Entities.Drikkevare;
using Database.Entities.RabatKupon;

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

        public List<IDrikkevare> Drikkevarer { get; set; }
        public List<IBarEvent> BarEvents { get; set; }
        public List<IRabatKupon> RabatKuponer { get; set; }
        public List<IBarrepræsentant> Barrepræsentanter { get; set; }
        public List<IAnmeldelse> Anmeldelser { get; set; }

    }
}