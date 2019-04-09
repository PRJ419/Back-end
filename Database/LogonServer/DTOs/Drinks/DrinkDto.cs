using System.ComponentModel.DataAnnotations;
using Database;

namespace LogonServer.DTOs.Drinks
{
    public class DrinkDto
    {
        [MaxLength(150)]
        public string BarName { get; set; }

        [MaxLength(50)]
        public string DrinksName { get; set; }

        
        public double Price { get; set; }
    }
}