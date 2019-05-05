using AutoMapper;
using NUnit.Framework;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.Bars;

namespace IntegrationTest
{
    [TestFixture]
    public class IntegrationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // As it was not possible to throw SQL exceptions in the Unit Test of WebApi, 
        // we will test that the WebApi reacts correctly to these. 

        [Test]
        public void AddABar_InvalidModel_DatabaseThrows_ReturnsBadRequest()
        {
            var _options =
                new DbContextOptionsBuilder<BarOMeterContext>().UseInMemoryDatabase(databaseName: "AddBadBar")
                    .Options;
            var _uow = new UnitOfWork(_options);
            IMapper mapper;
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);
            var barController = new BarController(_uow, mapper);

             barController.AddBar(new BarDto()
            {
                BarName = "test",
            });
             var result = barController.AddBar(new BarDto()
             {
                 BarName = "test",
             });
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }
    }
}