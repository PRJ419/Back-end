using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.BarEvent
{
    /// <summary>
    /// Data transfer object for BarEvent. 
    /// </summary>
    public class BarEventDto
    {
        [MaxLength(150)]
        public string BarName { get; set; }

        [MaxLength(75)]
        public string EventName { get; set; }

        public DateTime Date { get; set; }
    }
}