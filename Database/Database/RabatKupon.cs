using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace Database
{
    public class RabatKupon
    {
        [MaxLength(50)]
        public string BarNavn { get; set; }

        [MaxLength(50)]
        public string RabatKupon { get; set; }

        public DateTime Udløbsdato { get; set; }
    }
}