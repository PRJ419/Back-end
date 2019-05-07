using System.ComponentModel.DataAnnotations;
using Database;

namespace WebApi.DTOs.Drinks
{
    /// <summary>
    /// A data transfer object version of the Drink. <para></para>
    /// Reasoning behind the attributes can be seen in the Database.Entities.Drink documentation. 
    /// </summary>
    public class DrinkDto
    {
        [Required]
        [MaxLength(150)]
        public string BarName { get; set; }

        [Required]
        [MaxLength(50)]
        public string DrinksName { get; set; }

        
        public double Price { get; set; }

        public string Image { get; set; }
    }
}