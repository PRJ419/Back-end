//using System.Collections.Generic;
//using NUnit.Framework;
//using NUnit.Framework.Internal;
//using Database;
//using Database.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using NSubstitute;
//using WebApi.Controllers;

//namespace WevApi.Test.UnitTests
//{
//    [TestFixture]
//    public class BarControllerTests
//    {
//        private BarController uut;
//        private IUnitOfWork mockUnitOfWork;
//        private List<Bar> defaultList;

//        [SetUp]
//        public void Setup()
//        {
//            mockUnitOfWork = Substitute.For<IUnitOfWork>();
//            uut = new BarController(mockUnitOfWork);
//            defaultList = new List<Bar>();
//            defaultList.Add(new Bar()
//            {
//                AvgRating = 4,
//                BarName = "TestBar",
//                Picture = "www.google.com/image1",
//                ShortDescription = "Kort beskrivelse",
//                AgeLimit = 18,
//                Address = "Fakestreet 8000, Aarhus",
//                CVR = 1,
//                Educations = "IKT",
//                Email = "email@email.com",
//                LongDescription = "long",
//                PhoneNumber = 88888888,
//            });
//            defaultList.Add(new Bar()
//            {
//                AvgRating = 2.5,
//                BarName = "TestBar2",
//                Picture = "www.google.com/image1",
//                ShortDescription = "Kort beskrivelse",
//                AgeLimit = 18,
//                Address = "Fakestreet 8000, Aarhus",
//                CVR = 1,
//                Educations = "IKT",
//                Email = "email@email.com",
//                LongDescription = "long",
//                PhoneNumber = 88888888,
//            });

//        }

//        [Test]
//        public void guf()
//        {
//            Assert.True(true);
//        }
//        [Test]
//        public void GetBestBars_NonEmptyList_ReturnsOk()
//        {
//            // Arrange
//            mockUnitOfWork.BarRepository.GetBestBars().Returns(defaultList);

//            // Act
//            IActionResult result = uut.GetBestBars();

//            // Assert
//            Assert.That(result, Is.TypeOf<IActionResult>());
//        }
//    }
//}