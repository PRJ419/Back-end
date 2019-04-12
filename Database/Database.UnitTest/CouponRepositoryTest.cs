using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    class CouponRepositoryTest
    {
        [Test]
        public void CouponRepository_EditExistingCoupon_CouponIsEdited()
        {
            var options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditBarEvent")
                    .Options;

            using (var uow = new UnitOfWork(options))
            {
                var coupon = new Coupon()
                {
                    BarName = "Testbar",
                    CouponID = "Coupon123Test",
                    ExpirationDate = new DateTime(1997,01,01),
                };
                uow.CouponRepository.Add(coupon);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                var editedCoupon = new Coupon()
                {
                    BarName = "Testbar",
                    CouponID = "Coupon123Test",
                    ExpirationDate = new DateTime(2019,12,12),
                };
                uow.CouponRepository.Edit(editedCoupon);
                uow.Complete();
            }

            using (var uow = new UnitOfWork(options))
            {
                Assert.AreEqual(new DateTime(2019,12,12), 
                    uow.CouponRepository.Get("Coupon123Test", "Testbar").ExpirationDate);
            }
        }
    }
}
