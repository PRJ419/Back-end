using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.Entities;

namespace Database
{
    public class Anmeldelse : IAnmeldelse
    {
        [Required]
        public int BarTryk { get; set; }

        public string BarNavn { get; set; }

        public string BrugerNavn { get; set; }
        public Bar Bar { get; set; }

        public Kunde Kunde { get; set; }
    }
}