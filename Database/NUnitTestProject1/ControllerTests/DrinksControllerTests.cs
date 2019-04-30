using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Database;
using Database.Interfaces;
using Database.Repository_Implementations;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.Drinks;

namespace WebApi.Test.UnitTest.ControllerTests
{
    [TestFixture]
    public class DrinksControllerTests
    {

        private DrinksController uut;
        private IUnitOfWork mockUnitOfWork;
        private IMapper mapper;
        private List<Drink> defaultList;
        private Drink defaultDrink;
        private List<DrinkDto> correctResultList;

        [SetUp]
        public void Setup()
        {
            mockUnitOfWork = Substitute.For<IUnitOfWork>();

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);

            uut = new DrinksController(mockUnitOfWork, mapper);

            defaultList = new List<Drink>()
            {
                new Drink()
                {
                    BarName = "TestBar",
                    DrinksName = "Øl",
                    Image = "øl.jpg",
                    Price = 12,
                    Bar = null,
                },
                new Drink()
                {
                    BarName = "TestBar",
                    DrinksName = "Fadøl",
                    Image = "fadøl.jpg",
                    Price = 20,
                    Bar = null,
                }
            };
            defaultDrink = defaultList[0];

            // Direct conversion without navigational property
            correctResultList = new List<DrinkDto>()
            {
                new DrinkDto()
                {
                    BarName = "TestBar",
                    DrinksName = "Øl",
                    Image = "øl.jpg",
                    Price = 12,
                },
                new DrinkDto()
                {
                    BarName = "TestBar",
                    DrinksName = "Fadøl",
                    Image = "fadøl.jpg",
                    Price = 20,
                }
            };
        }


        [Test]
        public void GetDrinks_GetDrinksCorrectParam_ReturnsCorrectType()
        {
            string parameter = "TestBar";
          
            // Nsubstitute did not allow for search on TestBar, this behavior is not what
            // these unit tests responsibility is anyway.                     
            mockUnitOfWork.DrinkRepository.Find(Arg.Any<Expression<Func<Drink, bool>>>())
                .Returns(defaultList);
            
            var result = uut.GetDrinks(parameter);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

    }
}