using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database;

namespace WebApi.DTOs.Bars
{
    /// <summary>
    /// A data transfer object class for Bar. <para></para>
    /// Used in a List&lt;BarSimpleDto&gt; where many properties
    /// of the BarDto is not needed.
    /// Incoming requests holding Dtos that fail to comply will be returned BadRequest(400).
    /// </summary>
    public class BarSimpleDto
    {
        /// <summary>
        /// Required bar name property.
        /// </summary>
        [Required]
        public string BarName { get; set; }

        /// <summary>
        /// Average rating. Must be between 0.0 and 5.0
        /// </summary>
        [Range(0.0, 5.0)]
        public double AvgRating { get; set; }

        /// <summary>
        /// Shortdescription with maxlength of 500 characters.
        /// </summary>
        [MaxLength(500)]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Image string front-end will use to load pictures off the internet. 
        /// </summary>
        public string Image { get; set; }
    }
}