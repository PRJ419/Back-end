using Database;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebApi.DTOs.Bars;

namespace WebApi.Test.UnitTest.DtoConverterTests
{
    [TestFixture]
    public class BarDtoConverterTests
    {
        private Bar defaultBar;
        private BarDto defaultBarDto;
        
        [SetUp]
        public void Setup()
        {
            defaultBar = new Bar()
            {
                AvgRating = 4,
                BarName = "TestBar",
                Picture = "www.google.com/image1",
                ShortDescription = "Kort beskrivelse",
                AgeLimit = 18,
                Address = "Fakestreet 8000, Aarhus",
                CVR = 1,
                Educations = "IKT",
                Email = "email@email.com",
                LongDescription = "long",
                PhoneNumber = 88888888,
                Drinks = null,
                BarEvents = null,
                Coupons = null,
                Reviews = null,
                BarRepresentatives = null,
            };

            defaultBarDto = new BarDto()
            {
                AvgRating = 4,
                BarName = "TestBar",
                Picture = "www.google.com/image1",
                ShortDescription = "Kort beskrivelse",
                AgeLimit = 18,
                Address = "Fakestreet 8000, Aarhus",
                CVR = 1,
                Educations = "IKT",
                Email = "email@email.com",
                LongDescription = "long",
                PhoneNumber = 88888888,
            };

        }

        [Test]
        public void ToDto_AllPropertiesSupplied_DtoEquivalentReturned()
        {
            var result = BarDtoConverter.ToDto(defaultBar);
            Assert.That(result.BarName,     Is.EqualTo(defaultBar.BarName));
            Assert.That(result.AvgRating,   Is.EqualTo(defaultBar.AvgRating));
            Assert.That(result.Picture,     Is.EqualTo(defaultBar.Picture));
            Assert.That(result.ShortDescription, Is.EqualTo(defaultBar.ShortDescription));
            Assert.That(result.AgeLimit,    Is.EqualTo(defaultBar.AgeLimit));
            Assert.That(result.Address,     Is.EqualTo(defaultBar.Address));
            Assert.That(result.CVR,         Is.EqualTo(defaultBar.CVR));
            Assert.That(result.Educations,  Is.EqualTo(defaultBar.Educations));
            Assert.That(result.Email,       Is.EqualTo(defaultBar.Email));
            Assert.That(result.LongDescription, Is.EqualTo(defaultBar.LongDescription));
            Assert.That(result.PhoneNumber, Is.EqualTo(defaultBar.PhoneNumber));
        }

        [Test]
        public void ToDto_OnlyNameSupplied_AllNullExceptName()
        {
            var result = BarDtoConverter.ToDto(new Bar()
            {
                BarName = "TestBar"
            });
            Assert.That(result.BarName, Is.EqualTo("TestBar"));
            Assert.That(result.AvgRating, Is.EqualTo(0));
            Assert.That(result.Picture, Is.Null);
            Assert.That(result.ShortDescription, Is.Null);
            Assert.That(result.AgeLimit, Is.EqualTo(0));
            Assert.That(result.Address, Is.Null);
            Assert.That(result.CVR, Is.EqualTo(0));
            Assert.That(result.Educations, Is.Null);
            Assert.That(result.Email, Is.Null);
            Assert.That(result.LongDescription, Is.Null);
            Assert.That(result.PhoneNumber, Is.EqualTo(0));
        }

        [Test]
        public void ToBar_AllProperties_AllSetExceptNavigationalProps()
        {
            var result = BarDtoConverter.ToBar(defaultBarDto);
            Assert.That(result.BarName, Is.EqualTo(defaultBarDto.BarName));
            Assert.That(result.AvgRating, Is.EqualTo(defaultBarDto.AvgRating));
            Assert.That(result.Picture, Is.EqualTo(defaultBarDto.Picture));
            Assert.That(result.ShortDescription, Is.EqualTo(defaultBarDto.ShortDescription));
            Assert.That(result.AgeLimit, Is.EqualTo(defaultBarDto.AgeLimit));
            Assert.That(result.Address, Is.EqualTo(defaultBarDto.Address));
            Assert.That(result.CVR, Is.EqualTo(defaultBarDto.CVR));
            Assert.That(result.Educations, Is.EqualTo(defaultBarDto.Educations));
            Assert.That(result.Email, Is.EqualTo(defaultBarDto.Email));
            Assert.That(result.LongDescription, Is.EqualTo(defaultBarDto.LongDescription));
            Assert.That(result.PhoneNumber, Is.EqualTo(defaultBarDto.PhoneNumber));
            Assert.That(result.BarEvents, Is.Null);
            Assert.That(result.Drinks, Is.Null);
            Assert.That(result.Coupons, Is.Null);
            Assert.That(result.BarRepresentatives, Is.Null);
            Assert.That(result.Reviews, Is.Null);
        }

        [Test]
        public void ToBar_OnlyNameSupplied_OthersAreNull()
        {
            var result = BarDtoConverter.ToBar(new BarDto()
            {
                BarName = "TestBar"
            });
            Assert.That(result.BarName, Is.EqualTo("TestBar"));
            Assert.That(result.AvgRating, Is.EqualTo(0));
            Assert.That(result.Picture, Is.Null);
            Assert.That(result.ShortDescription, Is.Null);
            Assert.That(result.AgeLimit, Is.EqualTo(0));
            Assert.That(result.Address, Is.Null);
            Assert.That(result.CVR, Is.EqualTo(0));
            Assert.That(result.Educations, Is.Null);
            Assert.That(result.Email, Is.Null);
            Assert.That(result.LongDescription, Is.Null);
            Assert.That(result.PhoneNumber, Is.EqualTo(0));
            Assert.That(result.Reviews, Is.Null);
        }

        
    }
}