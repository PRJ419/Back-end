using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Bar
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

    }
}