using System.Collections.Generic;
using AutoMapper;
using Database;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.Drinks;
using WebApi.Utility;


namespace WebApi.Test.UnitTest.DtoConverterTests
{
    /// <summary>
    /// Since the mapper is already tested, in that it converts correctly between types,
    /// i find it sufficient to only test the GenericListConvert function in this class, with
    /// Drink and DrinkDto convertion. 
    /// </summary>
    [TestFixture]
    public class ConverterTests
    {
        private List<Drink> drinkList;
        private List<DrinkDto> drinkDtoList;
        private IMapper mapper;
        

        [SetUp]
        public void Setup()
        {
            drinkList = new List<Drink>();
            drinkDtoList = new List<DrinkDto>();
            drinkList.Add(new Drink()
            {
                BarName = "TestBar",
                DrinksName = "Øl",
                Image = "fil.jpg",
                Price = 10,
                Bar = null
            });
            drinkList.Add(new Drink()
            {
                BarName = "TestBar",
                DrinksName = "Fadøl",
                Image = "fil2.jpg",
                Price = 20,
                Bar = null,
            });

            drinkDtoList.Add(new DrinkDto()
            {
                BarName = "DtoTestBar",
                DrinksName = "Øl",
                Image = "fil.jpg",
                Price = 10,
            });
            drinkDtoList.Add(new DrinkDto()
            {
                BarName = "DtoTestBar",
                DrinksName = "Fadøl",
                Image = "fil2.jpg",
                Price = 20,
            });

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);

        }
        [Test]
        public void GenericListConvert_ConvertFromDrinkListToDrinkDtoList_CorrectType()
        {
            var input = drinkList as IEnumerable<Drink>;
            var converted = Converter.GenericListConvert<Drink, DrinkDto>(input, mapper);
            Assert.That(converted, Is.TypeOf<List<DrinkDto>>());
        }

        [Test]
        public void GenericListConvert_ConvertFromDrinkListToDrinkDtoList_CorrectProperties()
        {
            
            var converted = Converter.GenericListConvert<Drink, DrinkDto>(drinkList, mapper);
            Assert.That(converted[0].BarName, Is.EqualTo(drinkList[0].BarName));
            Assert.That(converted[1].BarName, Is.EqualTo(drinkList[1].BarName));
            Assert.That(converted[0].DrinksName, Is.EqualTo(drinkList[0].DrinksName));
            Assert.That(converted[1].DrinksName, Is.EqualTo(drinkList[1].DrinksName));
            Assert.That(converted[0].Image, Is.EqualTo(drinkList[0].Image));
            Assert.That(converted[1].Image, Is.EqualTo(drinkList[1].Image));
            Assert.That(converted[0].Price, Is.EqualTo(drinkList[0].Price));
            Assert.That(converted[1].Price, Is.EqualTo(drinkList[1].Price));
        }

        [Test]
        public void GenericListConvert_ConvertFromDrinkDtoToDrink_CorrectType()
        {
            var input = drinkDtoList as IEnumerable<DrinkDto>;
            var converted = Converter.GenericListConvert<DrinkDto, Drink>(input, mapper);
            Assert.That(converted, Is.TypeOf<List<Drink>>());
        }

        [Test]
        public void GenericListConvert_ConvertFromDrinkDtoToDrink_CorrectValues()
        {
            
            var converted = Converter.GenericListConvert<DrinkDto, Drink>(drinkDtoList, mapper);

            Assert.That(converted[0].BarName, Is.EqualTo(drinkDtoList[0].BarName));
            Assert.That(converted[1].BarName, Is.EqualTo(drinkDtoList[1].BarName));
            Assert.That(converted[0].DrinksName, Is.EqualTo(drinkDtoList[0].DrinksName));
            Assert.That(converted[1].DrinksName, Is.EqualTo(drinkDtoList[1].DrinksName));
            Assert.That(converted[0].Image, Is.EqualTo(drinkDtoList[0].Image));
            Assert.That(converted[1].Image, Is.EqualTo(drinkDtoList[1].Image));
            Assert.That(converted[0].Price, Is.EqualTo(drinkDtoList[0].Price));
            Assert.That(converted[1].Price, Is.EqualTo(drinkDtoList[1].Price));
            Assert.That(converted[0].Bar, Is.Null);
            Assert.That(converted[1].Bar, Is.Null);
        }

    }
}