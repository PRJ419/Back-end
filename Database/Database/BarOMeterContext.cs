﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Database { 
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Anmeldelse

            modelBuilder.Entity<Anmeldelse>()
                .HasKey(key => new {key.BarNavn, key.BrugerNavn});

            modelBuilder.Entity<Anmeldelse>()
                .HasOne(a => a.Kunde)
                .WithMany(k => k.Anmeldelser)
                .HasForeignKey(a => a.BrugerNavn);

            modelBuilder.Entity<Anmeldelse>()
                .HasOne(a => a.Bar)
                .WithMany(b => b.Anmeldelser)
                .HasForeignKey(a => a.BarNavn);

            #endregion


            #region Bar

            modelBuilder.Entity<Bar>()
                .HasKey(b => b.BarNavn);

            #endregion

            #region BarEvent

            modelBuilder.Entity<BarEvent>()
                .HasKey(key => new {key.BarNavn, key.EventNavn});

            modelBuilder.Entity<BarEvent>()
                .HasOne(b => b.Bar)
                .WithMany(e => e.BarEvents)
                .HasForeignKey(b => b.BarNavn);

            #endregion

            #region Barrepræsentant
            
            modelBuilder.Entity<Barrepræsentant>()
                .HasOne(a => a.Bar)
                .WithMany(b => b.Barrepræsentanter)
                .HasForeignKey(c => c.BarNavn);

            modelBuilder.Entity<Barrepræsentant>()
                .HasKey(a => a.BrugerNavn);


            #endregion

            #region Drikkevare

            modelBuilder.Entity<Drikkevare>()
                .HasOne(d => d.Bar)
                .WithMany(b => b.Drikkevarer)
                .HasForeignKey(d => d.BarNavn);

            modelBuilder.Entity<Drikkevare>()
                .HasKey(a => new {a.BarNavn, a.DrinksNavn});


            #endregion

            #region Kunde

            modelBuilder.Entity<Kunde>()
                .HasKey(a => a.BrugerNavn);

            modelBuilder.Entity<Kunde>()
                .HasIndex(a => a.Email).IsUnique();

            #endregion

            #region RabatKupon

            modelBuilder.Entity<RabatKupon>()
                .HasOne(b => b.Bar)
                .WithMany(a => a.RabatKuponer)
                .HasForeignKey(b => b.BarNavn);

            modelBuilder.Entity<RabatKupon>()
                .HasKey(a => new {a.RabatKuponID, a.BarNavn});


            #endregion

        }
    }
}