using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.ReviewDto
{
    /// <summary>
    /// Data transfer object of the Review object. <para></para>
    /// Reasoning behind attributes on the properties can be seen in Database.Entities.Review documentation. 
    /// </summary>
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
