using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityConfigurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder
                .HasOne(b => b.Bar)
                .WithMany(a => a.Coupons)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(b => b.BarName);

            builder
                .HasKey(a => new { a.CouponID, a.BarName });

        }
    }
}
