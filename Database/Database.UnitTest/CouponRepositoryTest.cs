using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Repository_Implementations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    [TestFixture]
    class CouponRepositoryTest
    {
        private CouponRepository _uut;
        private DbContextOptions<BarOMeterContext> _options;
        private BarOMeterContext _context;
        private SqliteConnection _connection;

        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("Datasource=:memory:");
            _connection.Open();
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseSqlite(_connection).Options;
            _context = new BarOMeterContext(_options);
            _uut = new CouponRepository(_context);
            _context.Database.EnsureCreated();
        }

        [Test]
        public void CouponRepository_EditExistingCoupon_CouponIsEdited()
        {
     
            var coupon = new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "Coupon123Test",
                ExpirationDate = new DateTime(1997,01,01),
            };
            _uut.Add(coupon);
            _context.SaveChanges();

            var editedCoupon = new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "Coupon123Test",
                ExpirationDate = new DateTime(2019,12,12),
            };
            _uut.Edit(editedCoupon);
            _context.SaveChanges();

            Assert.AreEqual(new DateTime(2019,12,12), 
                    _uut.Get("Coupon123Test", "Katrines Kælder").ExpirationDate);

        }


        [Test]
        public void Edit_Entity_WithoutChangingAnyInformation()
        {
            var coupon = new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "Coupon123Test",
                ExpirationDate = new DateTime(1997, 01, 01),
            };
            _uut.Add(coupon);
            _context.SaveChanges();

            var editedCoupon = new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "Coupon123Test",
                ExpirationDate = new DateTime(1997, 01, 01),
            };
            _uut.Edit(editedCoupon);
            _context.SaveChanges();

            Assert.AreEqual(new DateTime(1997, 01, 01),
                _uut.Get("Coupon123Test", "Katrines Kælder").ExpirationDate);
        }

        [Test]
        public void Add_Multiple_Coupons()
        {
            var coupon = new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "Coupon123Test",
                ExpirationDate = new DateTime(1997, 01, 01),
            };
            _uut.Add(coupon);
            

            var coupon2 = new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "Coupon123Test2",
                ExpirationDate = new DateTime(2010, 01, 01),
            };
            _uut.Add(coupon2);

            Assert.AreEqual(2,_context.SaveChanges());
        }

        [Test]
        public void Constraint_On_CouponID_And_BarName()
        {
            var coupon = new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "Coupon123Test",
                ExpirationDate = new DateTime(1997, 01, 01),
            };
            _uut.Add(coupon);


            var coupon2 = new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "Coupon123Test",
                ExpirationDate = new DateTime(2010, 01, 01),
            };
            ;

            Assert.That(() => _uut.Add(coupon2), Throws.Exception);
            
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }

    }
}
