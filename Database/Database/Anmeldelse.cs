﻿using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Anmeldelse
    {
        [Required]
        public int BarTryk { get; set; }
    }
}