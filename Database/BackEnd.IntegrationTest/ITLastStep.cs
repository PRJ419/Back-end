using System;
using System.Collections.Generic;
using System.Text;
using Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebApi.Controllers;

namespace BackEnd.IntegrationTest
{
    [TestFixture]
    class ITLastStep
    {
        private UnitOfWork _uow;
        private string _connection;
        private DbContextOptions<BarOMeterContext> _options;

        [SetUp]
        public void SetUp()
        {
            _connection = @"Data Source=DESKTOP-QND3SFP\MSSQLSERVER03;Initial Catalog=PRJTestDatabase2;Integrated Security=True";

            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseLazyLoadingProxies().UseSqlServer(_connection).Options;
            _uow = new UnitOfWork(_options);

            // Cleanse the DB of all bars
            var listOfBars = _uow.BarRepository.GetAll();
            foreach (var bar in listOfBars)
            {
                _uow.BarRepository.Delete(bar.BarName);
            }

            // Cleanse the DB of all drinks
            var listOfDrinks = _uow.DrinkRepository.GetAll();
            foreach (var drink in listOfDrinks)
            {
                _uow.DrinkRepository.Delete(drink.BarName, drink.DrinksName);
            }

            // Cleanse the DB of all events
            var listOfEvents = _uow.BarEventRepository.GetAll();
            foreach (var barEvent in listOfEvents)
            {
                _uow.BarEventRepository.Delete(barEvent.BarName, barEvent.EventName);
            }

            // Cleanse the DB of all coupons
            var listOfCoupons = _uow.CouponRepository.GetAll();
            foreach (var coupon in listOfCoupons)
            {
                _uow.CouponRepository.Delete(coupon.BarName, coupon.CouponID);
            }

            // Cleanse the DB of all BarRepresentatives
            var listOfRepresentatives = _uow.BarRepRepository.GetAll();
            foreach (var representative in listOfRepresentatives)
            {
                _uow.BarRepRepository.Delete(representative.Username);
            }

            // Cleanse the DB of all customers
            var listOfCustomers = _uow.CustomerRepository.GetAll();
            foreach (var customer in listOfCustomers)
            {
                _uow.CustomerRepository.Delete(customer.Username);
            }

            // Cleanse the DB of all reviews
            var listOfReviews = _uow.ReviewRepository.GetAll();
            foreach (var review in listOfReviews)
            {
                _uow.ReviewRepository.Delete(review.BarName, review.Username);
            }

            _uow.Complete();
        }

        [Test]
        public void firsttest()
        {
        }

        [TearDown]
        public void TearDown()
        {
            
        }

    }
}
