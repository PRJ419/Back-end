using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class Drink 
    {
        /// <summary>
        /// Property for getting and setting the name of the associated bar
        /// </summary>
        [MaxLength(150)]
        public string BarName { get; set; }

        /// <summary>
        /// Property for getting and setting the name of a drink
        /// </summary>
        [MaxLength(50)]
        public string DrinksName { get; set; }

        /// <summary>
        /// Property for getting and setting the price of a drink
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Property for getting and setting the image of a drink
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Navigational property required by the database for finding the associated bar
        /// </summary>
        public virtual Bar Bar { get; set; }

    }
}