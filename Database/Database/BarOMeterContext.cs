using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Database
    public class BarOMeterContext : DbContext
    {
        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Bar> Barer { get; set; }
        public DbSet<Barrepræsentant> Barrepræsentanter { get; set; }
        public DbSet<Anmeldelse> Anmeldelser { get; set; }
        public DbSet<BarEvent> BarEvents { get; set; }
        public DbSet<RabatKupon> RabatKuponer { get; set; }
        public DbSet<Drikkevare> Drikkevarer { get; set; }


        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-APD51SV;Initial Catalog=BarOMeter_Database_Test;Integrated Security=True");
        }



    }
}