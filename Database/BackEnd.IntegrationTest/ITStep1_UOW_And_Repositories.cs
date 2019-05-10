using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    [TestFixture]
    class ITStep1_UOW_And_Repositories
    {
        private UnitOfWork _uut;
        private Bar _bar1;
        private Bar _bar2;
        private Drink _drink1;
        private Review _review1;
        private Review _review2;
        private BarEvent _barEvent;
        private BarRepresentative _barRepresentative;
        private Coupon _coupon;
        private Customer _customer;
        private SqliteConnection _connection;
        private DbContextOptions<BarOMeterContext> _options;


        [SetUp]
        public void Setup()
        {
            _bar1 = new Bar()
            {
                Address = "FakeAddress",
                AgeLimit = 18,
                AvgRating = 0,
                BarName = "FakeBar",
                CVR = 12345678,
                Educations = "FakeEdu",
                Email = "Fake@Mail.org",
                Image = "FakeImg",
                PhoneNumber = 88888888,
                ShortDescription = "Fake Short Desc",
                LongDescription = "Fake Long Desc",
            };
            _bar2 = new Bar()
            {
                Address = "FakeAddress2",
                AgeLimit = 21,
                AvgRating = 4,
                BarName = "FakeBar2",
                CVR = 12345679,
                Educations = "FakeEdu2",
                Email = "Fake2@Mail.org",
                Image = "FakeImg2",
                PhoneNumber = 88888889,
                ShortDescription = "Fake Short Desc2",
                LongDescription = "Fake Long Desc2",
            };

            _drink1 = new Drink()
            {
                BarName = "Katrines Kælder",
                DrinksName = "Fadoel",
                Price = 20,
            };

            _review1 = new Review()
            {
                BarName = "FakeBar",
                BarPressure = 5,
                Username = "Bodega Bent",
            };

            _review2 = new Review()
            {
                BarName = "FakeBar",
                BarPressure = 3,
                Username = "Dehydrerede Dennis",
            };

            _barEvent = new BarEvent()
            {
                BarName = "Katrines Kælder",
                Date = new DateTime(2019,5,29),
                EventName = "FakeEvent",
            };

            _barRepresentative = new BarRepresentative()
            {
                BarName = "Katrines Kælder", 
                Name = "FakeBarRepresentative",
                Username = "FakeBarRepUsername",
            };

            _coupon = new Coupon()
            {
                BarName = "Katrines Kælder",
                CouponID = "FakeCouponID",
                ExpirationDate = new DateTime(2019,12,12),
            };

            _customer = new Customer()
            {
                DateOfBirth = new DateTime(1997, 2,5),
                Email = "FakeCustomer@Mail.org",
                FavoriteDrink = "Beer",
                FavoriteBar = "Katrines Kælder",
                Name = "Andreas Vorgaard",
            };
            _connection = new SqliteConnection("Datasource=:memory:");
            _connection.Open();
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseSqlite(_connection).Options;
            _uut = new UnitOfWork(_options);
        }


        [Test]
        public void UnitOfWorkComplete_AddsOne_ReceivesOne()
        {

            _uut.BarRepository.Add(_bar1);
            
            Assert.AreEqual(1, _uut.Complete());
        }

        [Test]
        public void UnitOfWorkComplete_AddsTwo_ReceivesTwo()
        {


            _uut.BarRepository.Add(_bar1);
            _uut.BarRepository.Add(_bar2);
            Assert.AreEqual(2, _uut.Complete());
        }

        [Test]
        public void UnitOfWorkComplete_AddsZero_ReceivesZero()
        {
            Assert.AreEqual(0, _uut.Complete());
        }

        [Test]
        public void UnitOfWorkComplete_AddToTwoRepo_ReceivesTwo()
        {

            _uut.BarRepository.Add(_bar1);
            _uut.DrinkRepository.Add(_drink1);

            Assert.AreEqual(2, _uut.Complete());
        }

        [Test]
        public void UnitOfWorkException_AddToTwoDuplicates_ThrowsException()
        {

            _uut.BarRepository.Add(_bar1);
            _uut.Complete();

            _uut.BarRepository.Add(_bar1);
            Assert.That(() => _uut.Complete(), Throws.Exception);
        }

        [Test]
        public void UnitOfWorkBarEventRepo_AddOne_ReceivesOne()
        {

            _uut.BarEventRepository.Add(_barEvent);

            Assert.AreEqual(1, _uut.Complete());
        }

        [Test]
        public void UnitOfWorkBarRepRepo_AddOne_ReceivesOne()
        {
            _uut.BarRepRepository.Add(_barRepresentative);

            Assert.AreEqual(1, _uut.Complete());
        }

        [Test]
        public void UnitOfWorkCouponRepo_AddOne_ReceivesOne()
        {
            _uut.CouponRepository.Add(_coupon);

            Assert.AreEqual(1, _uut.Complete());
        }

        [Test]
        public void UnitOfWorkCustomerRepo_AddOne_ReceivesOne()
        {
            _uut.CustomerRepository.Add(_customer);

            Assert.AreEqual(1, _uut.Complete());
        }

        [Test]
        public void UnitOfWorkReviewRepo_AddOne_ReceivesOne()
        {
            _uut.BarRepository.Add(_bar1);
            _uut.Complete();
            _uut.ReviewRepository.Add(_review1);

            Assert.AreEqual(1, _uut.Complete());
        }

        [Test]
        public void UnitOfWorkUpdateBarRating_AddThreeAndFive_ReceivesFour()
        {
            _uut.BarRepository.Add(_bar1);
            _uut.ReviewRepository.Add(_review1);
            _uut.ReviewRepository.Add(_review2);
            _uut.Complete();
            _uut.UpdateBarRating(_review2.BarName);
            _uut.Complete();

            var bar = _uut.BarRepository.Get("FakeBar");

            Assert.AreEqual(4, bar.AvgRating);
        }

        [Test]
        public void UnitOfWorkUpdateBarRating_UpdateNonExisting_ThrowsException()
        {
            Assert.That(() => _uut.UpdateBarRating("NonExistingBar"), Throws.Exception);
        }

        [TearDown]
        public void TearDown()
        {
            _connection.Close();
        }

    }
}
