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

        public List<Drink> Drinks { get; set; }
        public List<BarEvent> BarEvents { get; set; }
        public List<Coupon> Coupons { get; set; }
        public List<Barrepresentative> Barrepresentatives { get; set; }
        public List<Review> Reviews { get; set; }

    }
}