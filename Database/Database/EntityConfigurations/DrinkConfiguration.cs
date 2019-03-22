using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class DrinkConfiguration : IEntityTypeConfiguration<Drink>
    {
        public void Configure(EntityTypeBuilder<Drink> builder)
        {
            builder
                .HasOne(d => d.Bar)
                .WithMany(b => b.Drinks)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.BarName);

            builder
                .HasKey(a => new { a.BarName, a.DrinksName });
        }
    }
}
