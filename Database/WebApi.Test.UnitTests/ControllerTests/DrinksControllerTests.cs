using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Database;
using Database.Entities;
using Database.Interfaces;
using Database.Repository_Implementations;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal;
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

        [Test]
        public void GetDrinks_GetDrinksWrongParam_ReturnsCorrectType()
        {
            mockUnitOfWork.DrinkRepository.Find(Arg.Any<Expression<Func<Drink, bool>>>())
                .Returns(new List<Drink>());
            var result = uut.GetDrinks("BadString");
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void GetDrinks_GetDrinksCorrectParam_ReturnsCorrectList()
        {
            string parameter = "TestBar";

            mockUnitOfWork.DrinkRepository.Find(Arg.Any<Expression<Func<Drink, bool>>>())
                .Returns(defaultList);

            var objectResult = uut.GetDrinks(parameter);
            var result = (objectResult as OkObjectResult).Value as List<DrinkDto>;

            Assert.That(result[0].BarName, Is.EqualTo(defaultList[0].BarName));
            Assert.That(result[0].DrinksName, Is.EqualTo(defaultList[0].DrinksName));
            Assert.That(result[0].Image, Is.EqualTo(defaultList[0].Image));
            Assert.That(result[0].Price, Is.EqualTo(defaultList[0].Price));

            Assert.That(result[1].BarName, Is.EqualTo(defaultList[1].BarName));
            Assert.That(result[1].DrinksName, Is.EqualTo(defaultList[1].DrinksName));
            Assert.That(result[1].Image, Is.EqualTo(defaultList[1].Image));
            Assert.That(result[1].Price, Is.EqualTo(defaultList[1].Price));
        }

        [Test]
        public void AddDrink_UnitOfWorkAcceptsModel_ReturnsCreatedWithRouteAndObject()
        {
            var drinkDto = mapper.Map<DrinkDto>(defaultDrink);
            var result = uut.AddDrink(drinkDto);
            var resultObj = result as CreatedResult;
            
            Assert.That(result, Is.TypeOf<CreatedResult>());
            Assert.That(resultObj.Location, Is.EqualTo($"api/bars/{drinkDto.BarName}/Drinks"));
            Assert.That(resultObj.Value, Is.TypeOf<DrinkDto>());
        }

        

        [Test]
        public void AddDrink_UnitOfWorkThrowsException_ReturnsBadRequest()
        {
            var drinkDto = mapper.Map<DrinkDto>(defaultDrink);
            mockUnitOfWork.DrinkRepository
                .When(repo => repo.Add(Arg.Any<Drink>()))
                .Do(x => throw new Exception());

            var result = uut.AddDrink(drinkDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());

        }

        [Test]
        public void DeleteDrink_CorrectKey_Deleted()
        {
            var result = uut.DeleteDrink("TestBar", "Beer");
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void DeleteDrink_WrongKeyOrNotFound_NotDeleted()
        {
            string barName = "TestBar";
            string drinkName = "Beer";
            mockUnitOfWork.DrinkRepository
                .When(repo =>
                {
                    repo.Delete((new object[] {barName, drinkName}));
                })
                .Do( x => throw new Exception());

            var result = uut.DeleteDrink(barName, drinkName);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void EditDrink_CorrectInput_ReturnsCreated()
        {
            var drinktDto = mapper.Map<DrinkDto>(defaultDrink);
            var result = uut.EditDrink(drinktDto);
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public void EditDrink_CorrectInput_ReturnsRouteAndObject()
        {
            var drinktDto = mapper.Map<DrinkDto>(defaultDrink);
            var result = uut.EditDrink(drinktDto);
            var resultObj = (result as CreatedResult);
            Assert.That(resultObj.Location, Is.EqualTo($"api/bars/{drinktDto.BarName}/Drinks"));
            Assert.That(resultObj.Value, Is.TypeOf<DrinkDto>());
        }

        [Test]
        public void EditDrink_CorrectInput_ReturnsEquivalentObject()
        {
            var drinktDto = mapper.Map<DrinkDto>(defaultDrink);
            var result = uut.EditDrink(drinktDto);
            var resultObj = (result as CreatedResult).Value as DrinkDto;
            Assert.That(resultObj.BarName, Is.EqualTo(drinktDto.BarName));
            Assert.That(resultObj.DrinksName, Is.EqualTo(drinktDto.DrinksName));
            Assert.That(resultObj.Image, Is.EqualTo(drinktDto.Image));
            Assert.That(resultObj.Price, Is.EqualTo(drinktDto.Price));
        }

        [Test]
        public void EditDrink_BadInput_ReturnsBadRequest()
        {
            var drinkDto = mapper.Map<DrinkDto>(defaultDrink);
            mockUnitOfWork.DrinkRepository
                .When(repo => repo.Edit(Arg.Any<Drink>()))
                .Do(x => throw new Exception());
            var result = uut.EditDrink(drinkDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

    }
}