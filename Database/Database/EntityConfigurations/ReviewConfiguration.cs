using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasKey(key => new { key.BarName, key.Username });


            builder
                .HasOne(a => a.Customer)
                .WithMany(k => k.Reviews)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(a => a.Username);

            builder
                .HasOne(a => a.Bar)
                .WithMany(b => b.Reviews)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(a => a.BarName);

            #region Dataseeding

            builder.HasData(new Review()
            {
                BarPressure = 5,
                Username = "Bodega Bent",
                BarName = "Katrines Kælder"
            });

            builder.HasData(new Review()
            {
                BarPressure = 3,
                Username = "Bodega Bent",
                BarName = "Medicinsk Fredagsbar - Umbilicus"
            });

            builder.HasData(new Review()
            {
                BarPressure = 5,
                Username = "Dehydrerede Dennis",
                BarName = "Katrines Kælder",
            });

            builder.HasData(new Review()
            {
                BarPressure = 3,
                Username = "Dehydrerede Dennis",
                BarName = "Medicinsk Fredagsbar - Umbilicus"
            });

            #endregion
        }
    }
}
