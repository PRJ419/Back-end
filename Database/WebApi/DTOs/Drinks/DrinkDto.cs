using System.ComponentModel.DataAnnotations;
using Database;

namespace WebApi.DTOs.Drinks
{
    /// <summary>
    /// A data transfer object version of the Drink. <para></para>
    /// Incoming requests holding Dtos that fail to comply will be returned BadRequest(400).
    /// </summary>
    public class DrinkDto
    {
        /// <summary>
        /// Barname is required and must be 150 characters long at max. 
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string BarName { get; set; }

        /// <summary>
        /// Drink name is required and must be 50 characters long at max. 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string DrinksName { get; set; }

        /// <summary>
        /// Price of the drink
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Image URL string. 
        /// </summary>
        public string Image { get; set; }
    }
}