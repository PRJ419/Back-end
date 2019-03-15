using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities.BarEvent
{
    interface IBarEvent
    {
       
       string BarNavn { get; set; }

       string EventNavn { get; set; }

       DateTime Dato { get; set; }

       Bar Bar { get; set; }
    }
}
