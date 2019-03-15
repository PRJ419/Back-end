using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities;
using Database.Entities.Kunde;

namespace Database
{
    public class Anmeldelse : IAnmeldelse
    {
        [Required]
        public int BarTryk { get; set; }

        [MaxLength(150)]
        public string BarNavn { get; set; }

        [MaxLength(50)]
        public string BrugerNavn { get; set; }

        public IBar Bar { get; set; }

        public IKunde Kunde { get; set; }
    }
}