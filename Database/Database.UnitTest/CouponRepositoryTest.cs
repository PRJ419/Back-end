using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Repository_Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    class CouponRepositoryTest
    {
        private CouponRepository _repository;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;

        [Test]
        public void CouponRepository_EditExistingCoupon_CouponIsEdited()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "EditCoupon")
                    .Options;
            _context = new BarOMeterContext(_options);
            _repository = new CouponRepository(_context);

            var coupon = new Coupon()
            {
                BarName = "Testbar",
                CouponID = "Coupon123Test",
                ExpirationDate = new DateTime(1997,01,01),
            };
            _repository.Add(coupon);
            _context.SaveChanges();

            var editedCoupon = new Coupon()
            {
                BarName = "Testbar",
                CouponID = "Coupon123Test",
                ExpirationDate = new DateTime(2019,12,12),
            };
            _repository.Edit(editedCoupon);
            _context.SaveChanges();

            Assert.AreEqual(new DateTime(2019,12,12), 
                    _repository.Get("Coupon123Test", "Testbar").ExpirationDate);

        }
    }
}
