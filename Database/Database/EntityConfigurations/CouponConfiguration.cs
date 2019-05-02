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

            #region Dataseeding

            builder.HasData(new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "123ØL",
                ExpirationDate = new DateTime(2019,06,20)
            });

            builder.HasData(new Coupon()
            {
                BarName = "Medicinsk Fredagsbar - Umbilicus",
                CouponID = "VarmØlNuTak",
                ExpirationDate = new DateTime(2019,07,10)
            });

            builder.HasData(new Coupon()
            {
                BarName = "Tågekammeret",
                CouponID = "20MemLeak",
                ExpirationDate = new DateTime(2019,10,15)
            });

            #endregion

        }
    }
}
