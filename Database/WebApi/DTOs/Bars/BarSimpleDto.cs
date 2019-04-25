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
    /// Reasoning for attributes can be seen in the Database.Entities.Bar documentation
    /// </summary>
    public class BarSimpleDto
    {
        [Required]
        public string BarName { get; set; }
        [Range(0.0, 5.0)]
        public double AvgRating { get; set; }
        [MaxLength(500)]
        public string ShortDescription { get; set; }

        public string Image { get; set; }
    }
}