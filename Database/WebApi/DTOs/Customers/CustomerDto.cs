using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.Customers
{
    public class CustomerDto
    {
        /// <summary>
        /// Property for getting and setting the username of a Customer
        /// </summary>
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Property for getting and setting the name of a customer
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Property for getting and setting the birthday of a customer
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Property for getting and setting the Email of a customer
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        /// <summary>
        /// Property for getting and setting the favorite bar of a customer
        /// </summary>
        [MaxLength(150)]
        public string FavoriteBar { get; set; }

        /// <summary>
        /// Property for getting and setting the favorite drink of a customer
        /// </summary>
        [MaxLength(50)]
        public string FavoriteDrink { get; set; }
    }
}