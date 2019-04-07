using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Database.EntityConfigurations;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Database { 
    public class BarOMeterContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bar> Bar { get; set; }
        public DbSet<BarRepresentative> BarRepresentatives { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BarEvent> BarEvents { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Drink> Drinks { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Change the "AndreasPC" to the name of another connectionstring in app.config.
            // To see an example of how you set up another connectionstring, go into app.config -->
            // connectionstrings --> see the example with "AndreasPC"

            //TODO: sorry andy.   Jeg har hardcodet min connection string  - jeg kan fra mit projekt ikke få fat i TwinksPC
            //var connection = ConfigurationManager.ConnectionStrings["AndreasPC"].ConnectionString;
            var connection = @"Data Source=DESKTOP-UGIDUH3;Initial Catalog=PRJ4Database;Integrated Security=True";
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BarConfiguration());
            modelBuilder.ApplyConfiguration(new BarEventConfiguration());
            modelBuilder.ApplyConfiguration(new BarRepresentativeConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new DrinkConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());

        }
    }
}