using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Database.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.DTOs.Bars;


namespace BackEnd.IntegrationTest
{
    [TestFixture]
    class ITLastStep
    {
        private UnitOfWork _uow;
        private string _connection;
        private DbContextOptions<BarOMeterContext> _options;
        private IMapper _mapper;

        /// <summary>
        /// Need to be able to create new unit of work to mimic proper mvc behavior.
        /// If the tests use e.g. The same BarController and UOW, then a bar will be fetched from memory, not the DB!
        /// </summary>
        /// <returns>
        /// A new instance of unit of work.
        /// </returns>
        private IUnitOfWork CreateUnitOfWork()
        {
            var options = new DbContextOptionsBuilder<BarOMeterContext>().UseLazyLoadingProxies().UseSqlServer(_connection).Options;
            return new UnitOfWork(options);
        }

        private BarController CreateBarController()
        {
            return new BarController(CreateUnitOfWork(), _mapper);
        }

        [SetUp]
        public void SetUp()
        {
            _connection = @"Data Source=localhost;Initial Catalog=PRJTestDatabase2;Integrated Security=True";

            _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseLazyLoadingProxies().UseSqlServer(_connection).Options;
            _uow = new UnitOfWork(_options);

            #region Clearing All Tables in test-database
            
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
            #endregion

            // Setup for automapper
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);

            _uow.Complete();
        }

        [Test]
        public void GetBestBars_NoBarsInDatabase_ReturnsNotFound()
        {
            var barController = new BarController(_uow, _mapper);
            var httpResult = barController.GetBestBars(); 
            Assert.That(httpResult, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void AddBarToDatabase_BarIsAddedAndCanBeRetrieved()
        {
            var barName = "TestBar";
            var barController = new BarController(_uow, _mapper);
            var addResult = barController.AddBar(new BarDto()
            {
                BarName = barName, AvgRating = 3.4, ShortDescription = "SD",
                AgeLimit = 18, CVR = 1235, Image = "png.jpg", LongDescription = "short",
                Address = "123 street", Email = "gmail@hotmail.com", Educations = "IKT", PhoneNumber = 0000,
            });

            var secondBarController = CreateBarController();
            var retrievedResult = secondBarController.GetBar(barName);
            var retrievedObject = (retrievedResult as OkObjectResult).Value as BarDto;
            Assert.That(addResult, Is.TypeOf<CreatedResult>());
            Assert.That(retrievedResult, Is.TypeOf<OkObjectResult>());
            Assert.That(retrievedObject.BarName, Is.EqualTo(barName));
        }

        [Test]
        public void AddBarToDatabase_InvalidModel_BarNotAdded()
        {
            var barController = new BarController(_uow, _mapper);
            var name = "Only a barname is insufficient!";

            var addResult = barController.AddBar(new BarDto()
            {
                BarName = name
            });

            // To mimic MVC behavior a new controller is instantiated, with a new UnitOfWork
            // The bar is not added to database, but is saved in _uow's memory. 
            var secondBarController = CreateBarController();

            var retrievedResult = secondBarController.GetBar(name);
           
            Assert.That(addResult, Is.TypeOf<BadRequestResult>());
            Assert.That(retrievedResult, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Add2Bars_GetBarsReturnsAList()
        {
            var barController = new BarController(_uow, _mapper);

            barController.AddBar(new BarDto()
            {
                BarName = "Bar1",
                AvgRating = 3.4,
                ShortDescription = "SD",
                AgeLimit = 18,
                CVR = 1235,
                Image = "png.jpg",
                LongDescription = "short",
                Address = "123 street",
                Email = "gmail@hotmail.com",
                Educations = "IKT",
                PhoneNumber = 0000,
            });
            barController.AddBar(new BarDto()
            {
                BarName = "Bar2",
                AvgRating = 4.6,
                ShortDescription = "SD",
                AgeLimit = 18,
                CVR = 12356,
                Image = "png.jpg",
                LongDescription = "short",
                Address = "123 street",
                Email = "gmail2@hotmail.com",
                Educations = "IKT",
                PhoneNumber = 0000,
            });

            var secondBarController = CreateBarController();
            var retrieveResult = secondBarController.GetBestBars();
            var retrievedObject = (retrieveResult as OkObjectResult).Value as List<BarSimpleDto>;
            Assert.That(retrieveResult, Is.TypeOf<OkObjectResult>());
            Assert.That(retrievedObject.Count, Is.EqualTo(2));
            Assert.That(retrievedObject[0].BarName, Is.EqualTo("Bar2"));
            Assert.That(retrievedObject[1].BarName, Is.EqualTo("Bar1"));

        }

        [TearDown]
        public void TearDown()
        {
            
        }

    }
}
