using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using Database.Entities.RabatKupon;

namespace Database
{
    public class RabatKupon : IRabatKupon
    {
        [MaxLength(50)]
        public string BarNavn { get; set; }

        [MaxLength(50)]
        public string RabatKuponID { get; set; }

        public DateTime Udløbsdato { get; set; }

        public Bar Bar { get; set; }
    }
}