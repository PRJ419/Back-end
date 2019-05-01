using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Database.UnitTest
{
    [TestFixture]
    class UnitOfWorkTest
    {
        private DbContextOptions<BarOMeterContext> _options;
        private UnitOfWork _uow;
        private Bar _bar1;
        private Bar _bar2;
        private Drink _drink1;
        private Review _review1;
        private Review _review2;
        private BarEvent _barEvent;
        private BarRepresentative _barRepresentative;
        private Coupon _coupon;
        private Customer _customer;

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
                BarName = "FakeAddress",
                DrinksName = "Fadoel",
                Price = 20,
            };

            _review1 = new Review()
            {
                BarName = "FakeBar",
                BarPressure = 5,
                Username = "FakeCustomer",
            };

            _review2 = new Review()
            {
                BarName = "FakeBar",
                BarPressure = 3,
                Username = "FakeCustomer2",
            };

            _barEvent = new BarEvent()
            {
                BarName = "FakeBar",
                Date = new DateTime(2019,5,29),
                EventName = "FakeEvent",
            };

            _barRepresentative = new BarRepresentative()
            {
                BarName = "FakeBar", 
                Name = "FakeBarRepresentative",
                Username = "FakeBarRepUsername",
            };

            _coupon = new Coupon()
            {
                BarName = "FakeBar",
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
        }


        [Test]
        public void UnitOfWorkComplete_AddsOne_ReceivesOne()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "AddOneReceiveOne")
                    .Options;
            _uow = new UnitOfWork(_options);

            _uow.BarRepository.Add(_bar1);
            
            Assert.AreEqual(1, _uow.Complete());
        }

        [Test]
        public void UnitOfWorkComplete_AddsTwo_ReceivesTwo()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "AddTwoReceiveTwo")
                    .Options;
            _uow = new UnitOfWork(_options);

            _uow.BarRepository.Add(_bar1);
            _uow.BarRepository.Add(_bar2);
            Assert.AreEqual(2, _uow.Complete());
        }

        [Test]
        public void UnitOfWorkComplete_AddsZero_ReceivesZero()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "AddZeroReceiveZero")
                    .Options;
            _uow = new UnitOfWork(_options);
            
            Assert.AreEqual(0, _uow.Complete());
        }

        [Test]
        public void UnitOfWorkComplete_AddToTwoRepo_ReceivesTwo()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "TwoRepoAddReceiveTwo")
                    .Options;
            _uow = new UnitOfWork(_options);

            _uow.BarRepository.Add(_bar1);
            _uow.DrinkRepository.Add(_drink1);

            Assert.AreEqual(2, _uow.Complete());
        }

        [Test]
        public void UnitOfWorkException_AddToTwoDuplicates_ThrowsException()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "TwoDuplicateThrowsException")
                    .Options;
            _uow = new UnitOfWork(_options);

            _uow.BarRepository.Add(_bar1);
            _uow.Complete();

            _uow.BarRepository.Add(_bar1);
            Assert.That(() => _uow.Complete(), Throws.Exception);
        }

        [Test]
        public void UnitOfWorkBarEventRepo_AddOne_ReceivesOne()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "BarEventRepoAddOneReceiveOne")
                    .Options;
            _uow = new UnitOfWork(_options);

            _uow.BarEventRepository.Add(_barEvent);

            Assert.AreEqual(1, _uow.Complete());
        }

        [Test]
        public void UnitOfWorkBarRepRepo_AddOne_ReceivesOne()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>()
                    .UseInMemoryDatabase(databaseName: "BarRepRepoAddOneReceiveOne")
                    .Options;
            _uow = new UnitOfWork(_options);

            _uow.BarRepRepository.Add(_barRepresentative);

            Assert.AreEqual(1, _uow.Complete());
        }

        [Test]
        public void UnitOfWorkCouponRepo_AddOne_ReceivesOne()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>()
                    .UseInMemoryDatabase(databaseName: "CouponRepoAddOneReceiveOne")
                    .Options;
            _uow = new UnitOfWork(_options);

            _uow.CouponRepository.Add(_coupon);

            Assert.AreEqual(1, _uow.Complete());
        }

        [Test]
        public void UnitOfWorkCustomerRepo_AddOne_ReceivesOne()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>()
                    .UseInMemoryDatabase(databaseName: "CustomerRepoAddOneReceiveOne")
                    .Options;
            _uow = new UnitOfWork(_options);

            _uow.CustomerRepository.Add(_customer);

            Assert.AreEqual(1, _uow.Complete());
        }

        [Test]
        public void UnitOfWorkReviewRepo_AddOne_ReceivesOne()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>()
                    .UseInMemoryDatabase(databaseName: "ReviewRepoAddOneReceiveOne")
                    .Options;
            _uow = new UnitOfWork(_options);

            _uow.ReviewRepository.Add(_review1);

            Assert.AreEqual(1, _uow.Complete());
        }

        [Test]
        public void UnitOfWorkUpdateBarRating_AddThreeAndFive_ReceivesFour()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>()
                    .UseInMemoryDatabase(databaseName: "UpdateBarRatingThreeAndFiveGivesFour")
                    .Options;
            _uow = new UnitOfWork(_options);
            _uow.BarRepository.Add(_bar1);
            _uow.ReviewRepository.Add(_review1);
            _uow.ReviewRepository.Add(_review2);
            _uow.Complete();
            _uow.UpdateBarRating(_review2.BarName);
            _uow.Complete();

            var bar = _uow.BarRepository.Get("FakeBar");

            Assert.AreEqual(4, bar.AvgRating);
        }

        [Test]
        public void UnitOfWorkUpdateBarRating_UpdateNonExisting_ThrowsException()
        {
            _options =
                new DbContextOptionsBuilder<BarOMeterContext>()
                    .UseInMemoryDatabase(databaseName: "UpdateBarRatingThrowsException")
                    .Options;
            _uow = new UnitOfWork(_options);
            
            Assert.That(() => _uow.UpdateBarRating("NonExistingBar"), Throws.Exception);
        }

    }
}
