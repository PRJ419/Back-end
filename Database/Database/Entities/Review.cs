using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Review 
    {
        [Required]
        public int BarPressure { get; set; }

        [MaxLength(150)]
        public string BarName { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public virtual Bar Bar { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }
    }
}