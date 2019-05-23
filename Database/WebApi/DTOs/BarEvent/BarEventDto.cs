using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.BarEvent
{
    /// <summary>
    /// Data transfer object for BarEvent. <para></para>
    /// See Database.Entities.BarEvent for model equivalent.
    /// Incoming requests holding Dtos that fail to comply will be returned BadRequest(400).
    /// </summary>
    public class BarEventDto
    {
        /// <summary>
        /// Is required and has a maxlength of 150 characters. 
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string BarName { get; set; }

        /// <summary>
        /// Is required and has a maxlength of 75 characters. 
        /// </summary>
        [Required]
        [MaxLength(75)]
        public string EventName { get; set; }

        /// <summary>
        /// Normal property storing Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Image of the bar
        /// </summary>
        public string Image { get; set; }
    }
}