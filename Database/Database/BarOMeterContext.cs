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
            

        }
    }
}