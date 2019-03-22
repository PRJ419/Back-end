using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }
    }
}
