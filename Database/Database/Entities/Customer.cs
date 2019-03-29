using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Database
{
    public class Customer
    {
        
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string FavoriteBar { get; set; }

        [MaxLength(50)]
        public string FavoriteDrink { get; set; }

        public List<Review> Reviews { get; set; }
    }
}