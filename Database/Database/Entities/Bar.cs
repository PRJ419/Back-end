using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Bar 
    {
        /// <summary>
        /// The name of the given bar. It has a maximum length of 150
        /// </summary>
        [MaxLength(150)]
        public string BarName { get; set; }

        /// <summary>
        /// The address for the given bar. This is required to make a bar, and has a max length of 255
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        /// <summary>
        /// The age limit of the bar. This is required to make a bar
        /// </summary>
        [Required]
        public int AgeLimit { get; set; }


        /// <summary>
        /// A property for setting the names of the educations that's connected to the bar.
        /// This has a max length of 255.
        /// </summary>
        [MaxLength(255)]
        public string Educations { get; set; }

        /// <summary>
        /// Used for a short description of the bar. This has a max length of 500.
        /// </summary>
        [MaxLength(500)]
        public string ShortDescription { get; set; }


        /// <summary>
        /// Used for a longer and more detailed description of the bar. This has a max length of 2500.
        /// </summary>
        [MaxLength(2500)]
        public string LongDescription { get; set; }

        /// <summary>
        /// The CVR of the bar. This is required, since all bars must have a CVR to work as an enterprise.
        /// </summary>
        [Required]
        public int CVR { get; set; }


        /// <summary>
        /// For the phone number of the bar.
        /// </summary>
        public int PhoneNumber { get; set; }

        /// <summary>
        /// This property is for the email of the bar. It has a max length of 150 and MUST be unique.
        /// </summary>
        [MaxLength(150)]
        public string Email { get; set; }


        /// <summary>
        /// The average rating of the bar. This is saved in the bar itself, so it doesn't have to be
        /// calculated every time it's pulled out of the database.
        /// </summary>
        [Range(0.0, 5.0)]
        public double AvgRating { get; set; }


        /// <summary>
        /// String for saving the picture of the bar.
        /// </summary>
        public string Image { get; set; }

        
        /// <summary>
        /// Navigation property to find the drinks for the bar.
        /// </summary>
        public virtual List<Drink> Drinks { get; set; }

        /// <summary>
        /// Navigation property to find the bar events for the bar.
        /// </summary>
        public virtual List<BarEvent> BarEvents { get; set; }

        /// <summary>
        /// Navigation property to find the coupons for the bar.
        /// </summary>
        public virtual List<Coupon> Coupons { get; set; }

        /// <summary>
        /// Navigation property to find the bar representatives connected to the bar.
        /// </summary>
        public virtual List<BarRepresentative> BarRepresentatives { get; set; }

        /// <summary>
        /// Navigation property to find the reviews for the bar.
        /// </summary>
        public virtual List<Review> Reviews { get; set; }

    }
}