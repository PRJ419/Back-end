using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Database.Entities.Kunde;

namespace Database
{
    public class Kunde : IKunde
    {
        
        [MaxLength(50)]
        public string BrugerNavn { get; set; }

        [Required]
        [MaxLength(150)]
        public string Navn { get; set; }

        [Required]
        public DateTime Fødselsdag { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(150)]
        public string FavoritBar { get; set; }

        [MaxLength(50)]
        public string FavoritDrikkevare { get; set; }

        public List<Anmeldelse> Anmeldelser { get; set; }
    }
}