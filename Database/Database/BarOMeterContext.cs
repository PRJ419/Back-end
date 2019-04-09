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

        /// <summary>
        /// Empty constructor so we can create a context
        /// </summary>
        public BarOMeterContext()
        { }


        /// <summary>
        /// Constructor which accepts a DbContextOptions as parameter. Primarily used for testing purposes.
        /// </summary>
        /// <param name="options"></param>
        public BarOMeterContext(DbContextOptions<BarOMeterContext> options) : base(options)
        { }


        /// <summary>
        /// Overwriting of the OnConfiguring from DbContext, so that we can define our own connectionstring
        /// to a database. If the database already is configured, we won't set it to this new connectionstring.
        /// </summary>
        /// <param name="optionsBuilder">
        /// Used for specifying which database we want to connect to.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var connection = ConfigurationManager.ConnectionStrings["AndreasPC"].ConnectionString;
                var connection = @"Data Source=DESKTOP-UGIDUH3;Initial Catalog=PRJ4Database;Integrated Security=True";
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connection);
            }
        }


        /// <summary>
        /// Used by ef core to create the proper database by applying the Fluent API configuration files that's
        /// been defined in this project.
        /// </summary>
        /// <param name="modelBuilder">
        /// Used for building the database model
        /// </param>
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