using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Drikkevare
    {
        [MaxLength(150)]
        public string BarNavn { get; set; }

        [MaxLength(50)]
        public string DrinksNavn { get; set; }

        public double Pris { get; set; }

    }
}