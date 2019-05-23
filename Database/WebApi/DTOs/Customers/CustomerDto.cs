using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Customers
{
    /// <summary>
    /// Data transfer object equivalent of Customer model class. 
    /// Incoming requests holding Dtos that fail to comply with requirements will be returned BadRequest(400).
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Property for getting and setting the username of a Customer. Required and maxlength of 50 characters. 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Property for getting and setting the name of a customer. Required and maxlength of 150 characters.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Property for getting and setting the birthday of a customer. Is required. 
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Property for getting and setting the Email of a customer. Is required and must be 150 characters long maximum.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        /// <summary>
        /// Property for getting and setting the favorite bar of a customer. Maxlength of 150 characters.
        /// </summary>
        [MaxLength(150)]
        public string FavoriteBar { get; set; }

        /// <summary>
        /// Property for getting and setting the favorite drink of a customer. Maxlength of 50 characters. 
        /// </summary>
        [MaxLength(50)]
        public string FavoriteDrink { get; set; }
    }
}