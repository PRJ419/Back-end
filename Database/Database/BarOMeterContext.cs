using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Database { 
    public class BarOMeterContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Barrepresentative> Barrepresentatives { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BarEvent> BarEvents { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Drink> Drinks { get; set; }


        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-APD51SV;Initial Catalog=BarOMeter_Database_Test;Integrated Security=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Review

            modelBuilder.Entity<Review>()
                .HasKey(key => new {key.BarName, key.Username});

            modelBuilder.Entity<Review>()
                .HasOne(a => a.Customer)
                .WithMany(k => k.Reviews)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(a => a.Username);

            modelBuilder.Entity<Review>()
                .HasOne(a => a.Bar)
                .WithMany(b => b.Reviews)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(a => a.BarName);

            #endregion


            #region Bar

            modelBuilder.Entity<Bar>()
                .HasKey(b => b.BarName);

            #endregion

            #region BarEvent

            modelBuilder.Entity<BarEvent>()
                .HasKey(key => new {key.BarName, key.EventName});

            modelBuilder.Entity<BarEvent>()
                .HasOne(b => b.Bar)
                .WithMany(e => e.BarEvents)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(b => b.BarName);

            #endregion

            #region Barrepresentative
            
            modelBuilder.Entity<Barrepresentative>()
                .HasOne(a => a.Bar)
                .WithMany(b => b.Barrepresentatives)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(c => c.BarName);

            modelBuilder.Entity<Barrepresentative>()
                .HasKey(a => a.Username);


            #endregion

            #region Drink

            modelBuilder.Entity<Drink>()
                .HasOne(d => d.Bar)
                .WithMany(b => b.Drinks)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.BarName);

            modelBuilder.Entity<Drink>()
                .HasKey(a => new {a.BarName, a.DrinksName});


            #endregion

            #region Customer

            modelBuilder.Entity<Customer>()
                .HasKey(a => a.Username);

            modelBuilder.Entity<Customer>()
                .HasIndex(a => a.Email)
                .IsUnique();
      
            #endregion

            #region Coupon

            modelBuilder.Entity<Coupon>()
                .HasOne(b => b.Bar)
                .WithMany(a => a.Coupons)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(b => b.BarName);

            modelBuilder.Entity<Coupon>()
                .HasKey(a => new {a.CouponID, a.BarName});


            #endregion

        }
    }
}