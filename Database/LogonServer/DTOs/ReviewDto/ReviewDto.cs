using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogonServer.DTOs.ReviewDto
{
    public class ReviewDto
    {
        [Required]
        public int BarPressure { get; set; }

        [MaxLength(150)]
        public string BarName { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }
    }
}
