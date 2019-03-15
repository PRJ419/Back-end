using System.ComponentModel.DataAnnotations;
using Database.Entities.Drikkevare;

namespace Database
{
    public class Drikkevare : IDrikkevare
    {
        [MaxLength(150)]
        public string BarNavn { get; set; }

        [MaxLength(50)]
        public string DrinksNavn { get; set; }

        public double Pris { get; set; }

        public IBar Bar { get; set; }

    }
}