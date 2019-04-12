using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Review 
    {
        /// <summary>
        /// Property for getting and setting the rating of a bar
        /// </summary>
        [Required]
        public int BarPressure { get; set; }

        /// <summary>
        /// Property for getting and setting the name of a bar associated to the review
        /// </summary>
        [MaxLength(150)]
        public string BarName { get; set; }

        /// <summary>
        /// Property for getting and setting the username of the user associated to the review
        /// </summary>
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Navigational property required by the database to find the associated bar
        /// </summary>
        public virtual Bar Bar { get; set; }

        /// <summary>
        /// Navigational property required by the database to find the associated Customer
        /// </summary>
        public virtual Customer Customer { get; set; }
    }
}