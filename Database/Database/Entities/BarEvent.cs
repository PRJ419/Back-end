﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class BarEvent
    {
        [MaxLength(150)]
        public string BarName { get; set; }
        
        [MaxLength(75)]
        public string EventName { get; set; }

        public DateTime Date { get; set; }

        public virtual Bar Bar { get; set; }
    }
}