using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs.ReviewDto
{
    /// <summary>
    /// Data transfer object of the Review object. <para></para>
    /// Incoming requests holding Dtos that fail to comply will be returned BadRequest(400).
    /// </summary>
    public class ReviewDto
    {
        /// <summary>
        /// Is the rating of the bar, is required. Is nullable because ASP.NET Core cannot enforce the [Required] annotation without it on integers.  
        /// </summary>
        [Required]
        public int? BarPressure { get; set; }

        /// <summary>
        /// Bar name is required and has maximum length of 150 characters. 
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string BarName { get; set; }

        /// <summary>
        /// Username is required and can be 50 characters long at max.   
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
    }
}
