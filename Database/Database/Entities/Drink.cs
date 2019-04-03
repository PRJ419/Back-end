using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Drink 
    {
        [MaxLength(150)]
        public string BarName { get; set; }

        [MaxLength(50)]
        public string DrinksName { get; set; }

        public double Price { get; set; }

        public virtual Bar Bar { get; set; }

    }
}