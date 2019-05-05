using AutoMapper;
using NUnit.Framework;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.Bars;
using Microsoft.Data.Sqlite;


namespace IntegrationTest
{
    [TestFixture]
    public class IntegrationTests
    {

        private DbContextOptions<BarOMeterContext> _options;
        private SqliteConnection _connection;
        IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private BarController _barController;


        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("Datasource=:memory:");
            _connection.Open();
            var _context = new BarOMeterContext();
            _context.Database.EnsureCreated();
            _options = new DbContextOptionsBuilder<BarOMeterContext>().UseSqlite(_connection).Options;
            
            _unitOfWork = new UnitOfWork(_options);

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);

            _barController = new BarController(_unitOfWork, _mapper);
            


        }

        // As it was not possible to throw SQL exceptions in the Unit Test of WebApi, 
        // we will test that the WebApi reacts correctly to these. 

        [Test]
        public void AddABar_InvalidModel_DatabaseThrows_ReturnsBadRequest()
        {
           
            var result = _barController.AddBar(new BarDto()
            {
                BarName = "TestBar",
                Address = "FakeStreet",
                AgeLimit = 18,
                CVR = 0,
                AvgRating = 5,
                Email = "email@gmail.com",
                Educations = "IKT, EE, E",
                Image = "picture.jpg",
                LongDescription = "lang",
                ShortDescription = "kort",
                PhoneNumber = 88888888,
            });
            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}