using System;
using System.ComponentModel.DataAnnotations;
using Database.Entities.BarEvent;

namespace Database
{
    public class BarEvent : IBarEvent
    {
        [MaxLength(150)]
        public string BarNavn { get; set; }
        
        [MaxLength(75)]
        public string EventNavn { get; set; }

        public DateTime Dato { get; set; }

        public Bar Bar { get; set; }
    }
}