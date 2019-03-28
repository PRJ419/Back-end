using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Bar 
    {
        
        [MaxLength(150)]
        public string BarName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        public int AgeLimit { get; set; }

        [MaxLength(255)]
        public string Educations { get; set; }

        [MaxLength(500)]
        public string ShortDescription { get; set; }

        [MaxLength(2500)]
        public string LongDescription { get; set; }

        [MaxLength(8)]
        [Required]
        public int CVR { get; set; }

        [MaxLength(10)]
        public int PhoneNumber { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [Range(0.0, 5.0)]
        public double AvgRating { get; set; }

        
        
        public List<Drink> Drinks { get; set; }
        public List<BarEvent> BarEvents { get; set; }
        public List<Coupon> Coupons { get; set; }
        public List<Barrepresentative> Barrepresentatives { get; set; }
        public List<Review> Reviews { get; set; }

    }
}