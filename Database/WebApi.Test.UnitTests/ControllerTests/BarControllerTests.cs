using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.Bars;
using WebApi.Utility;

namespace WebApi.Test.UnitTest.ControllerTests
{
    [TestFixture]
    public class BarControllerTests
    {
        private BarController uut;
        private IUnitOfWork mockUnitOfWork;
        private IMapper mapper;
        private List<Bar> defaultList;
        private Bar defaultBar;
        private List<BarSimpleDto> correctResultList;

        [SetUp]
        public void Setup()
        {
            mockUnitOfWork = Substitute.For<IUnitOfWork>();

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);

            uut = new BarController(mockUnitOfWork, mapper);
            defaultList = new List<Bar>();
            defaultList.Add(new Bar()
            {
                AvgRating = 4,
                BarName = "TestBar",
                Image = "www.google.com/image1",
                ShortDescription = "Kort beskrivelse",
                AgeLimit = 18,
                Address = "Fakestreet 8000, Aarhus",
                CVR = 1,
                Educations = "IKT",
                Email = "email@email.com",
                LongDescription = "long",
                PhoneNumber = 88888888,
            });
            defaultList.Add(new Bar()
            {
                AvgRating = 2.5,
                BarName = "TestBar2",
                Image = "www.google.com/image1",
                ShortDescription = "Kort beskrivelse",
                AgeLimit = 18,
                Address = "Fakestreet 8000, Aarhus",
                CVR = 1,
                Educations = "IKT",
                Email = "email@email.com",
                LongDescription = "long",
                PhoneNumber = 88888888,
            });
            defaultBar = defaultList[0];
            correctResultList = Converter.GenericListConvert
                <Bar, BarSimpleDto>(defaultList, mapper);

        }

        [Test]
        public void GetBestBars_NonEmptyList_ReturnsOk()
        {
            // Arrange
            mockUnitOfWork.BarRepository.GetBestBars().Returns(defaultList);

            // Act
            IActionResult result = uut.GetBestBars();

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetBestBars_EmptyList_ReturnsNotFound()
        {
            // Arrange
            mockUnitOfWork.BarRepository.GetBestBars().Returns(new List<Bar>());

            // Act
            var result = uut.GetBestBars();

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void GetBestBars_NonEmptyList_ReturnsCorrectList()
        {
            // Arrange
            mockUnitOfWork.BarRepository.GetBestBars().Returns(defaultList);

            // Act
            var result = uut.GetBestBars() as OkObjectResult;
            var resultList = result.Value as List<BarSimpleDto>;

            // Assert
            Assert.That(resultList[0].BarName, Is.EqualTo(correctResultList[0].BarName));
            Assert.That(resultList[1].BarName, Is.EqualTo(correctResultList[1].BarName));
            Assert.That(resultList[0].AvgRating, Is.EqualTo(correctResultList[0].AvgRating));
            Assert.That(resultList[1].AvgRating, Is.EqualTo(correctResultList[1].AvgRating));
            Assert.That(resultList[0].Image, Is.EqualTo(correctResultList[0].Image));
            Assert.That(resultList[1].Image, Is.EqualTo(correctResultList[1].Image));
            Assert.That(resultList[0].ShortDescription, Is.EqualTo(correctResultList[0].ShortDescription));
            Assert.That(resultList[1].ShortDescription, Is.EqualTo(correctResultList[1].ShortDescription));
        }

        [Test]
        public void GetBar_SearchForExistingBar_ReturnsBar()
        {
            // Arrange
            mockUnitOfWork.BarRepository.Get("TestBar").Returns(defaultBar);
            var correctBar = mapper.Map<BarDto>(defaultBar);

            // Act
            var resultObject = uut.GetBar("TestBar") as OkObjectResult;
            var result = resultObject.Value as BarDto;
            // Assert
            Assert.That(result.BarName, Is.EqualTo(correctBar.BarName));
            Assert.That(result.Address, Is.EqualTo(correctBar.Address));
            Assert.That(result.AgeLimit, Is.EqualTo(correctBar.AgeLimit));
            Assert.That(result.AvgRating, Is.EqualTo(correctBar.AvgRating));
        }

        [Test]
        public void GetBar_SearchForNonExistingBar_ReturnsNull()
        {
            // Arrange
            mockUnitOfWork.BarRepository.Get("typo").ReturnsNull();

            // Act
            var resultObject = uut.GetBar("typo");
            // Assert
            Assert.That(resultObject, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void AddBar_SuppliedValidBar_ReturnsCreated()
        {
            // Arrange
            var receivedBarDto = mapper.Map<BarDto>(defaultBar);
            // Act
            var result = uut.AddBar(receivedBarDto);
            // Assert
            mockUnitOfWork.BarRepository.Received(1).Add(Arg.Any<Bar>());
            mockUnitOfWork.Received(1).Complete();
            Assert.That(receivedBarDto.BarName, Is.EqualTo(defaultBar.BarName));
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public void AddBar_NonValidModelReceived_ReturnsBadRequest()
        {
            // Arrange
            mockUnitOfWork.BarRepository
                .When(x => x.Add(Arg.Any<Bar>()))
                .Do(x => throw new Exception());

            var badBarDto = new BarDto()
            {
                BarName = "This Bar Only has a name which isn't enough",
            };

            // Act
            var result = uut.AddBar(badBarDto);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());

        }

        //[Test]
        //public void AddBar_BarWithSameKeyExists_BadRequestDublicateKeyReturned()
        //{
        //    mockUnitOfWork.BarRepository
        //        .When(x => x.Add(Arg.Any<Bar>()))
        //        .Do(x =>
        //        {
        //            var innerException = System.Runtime.Serialization
        //                .FormatterServices.GetUninitializedObject(typeof(SqlException)) as SqlException;

        //            //var innerException = Substitute.For<SqlException>();
        //            //innerException.Number.Returns(2627);
        //            var exception = new DbUpdateException("", innerException);
        //            throw exception;
        //        });
        //    var badBarDto = new BarDto()
        //    {
        //        BarName = "This name could e.g. be duplicate",
        //    };
        //    var result = uut.AddBar(badBarDto);
        //    Assert.That(result, Is.TypeOf<BadRequestResult>());
        //    Assert.That((result as BadRequestResult), Is.EqualTo("Duplicate Key"));
        //}

        [Test]
        public void DeleteBar_DeleteExistingBar_ReturnsOk()
        {
            // Arrange
            string receivedId = "TestBar";

            // Act
            var result = uut.DeleteBar(receivedId);

            // Assert
            Assert.That(result, Is.TypeOf<OkResult>());
            mockUnitOfWork.BarRepository.Received(1).Delete(receivedId);
        }

        [Test]
        public void DeleteBar_DeleteNonExistingBar_ReturnsBadRequest()
        {
            // Arrange
            string receivedId = "Typo!";
            mockUnitOfWork.BarRepository
                .When(x => x.Delete(receivedId))
                .Do(x => throw new Exception());

            // Act
            var result = uut.DeleteBar(receivedId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void UpdateBar_ValidModel_ReturnsSuccess()
        {
            // Arrange
            var receivedDto = mapper.Map<BarDto>(defaultBar);

            // Act
            var result = uut.UpdateBar(receivedDto);

            // Assert
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public void UpdateBar_NonValidModel_ReturnsBadRequest()
        {
            // Arrange
            var receivedDto = mapper.Map<BarDto>(defaultBar);
            mockUnitOfWork.BarRepository
                .When(repo => repo.Edit(Arg.Any<Bar>()))
                .Do(x => throw new Exception());

            // Act
            var result = uut.UpdateBar(receivedDto);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void GetWorstBars_ListNotEmpty_ReturnsList()
        {
            // Arrange - Doesn't matter what is returned - that is UOW's responsibility
            mockUnitOfWork.BarRepository.GetWorstBars().Returns(defaultList);

            // Act
            var result = uut.GetWorstBars();
            var resultObj = result as OkObjectResult;
            var resultList = resultObj.Value as List<BarSimpleDto>;
            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(resultList.Count, Is.EqualTo(2));
            Assert.That(resultList.First().BarName,
                        Is.EqualTo(correctResultList.First().BarName));
        }
        [Test]
        public void GetWorstBars_EmptyList_ReturnNotFound()
        {
            // Arrange
            mockUnitOfWork.BarRepository.GetWorstBars().Returns(new List<Bar>());

            // Act
            var result = uut.GetWorstBars();

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
            mockUnitOfWork.BarRepository.Received(1).GetWorstBars();
        }

        [Test]
        public void GetRangeOfBars_ListHolds2_Returns1()
        {
            // Arrange
            var fromIndex = 0;
            var length = 1;
            var rangeBars = new List<Bar>();
            rangeBars.Add(defaultBar);
            mockUnitOfWork.BarRepository.GetXBars(fromIndex, length)
                .Returns(rangeBars);

            // Act
            var result = uut.GetRangeOfBars(fromIndex, length);
            var objectResult = result as OkObjectResult;
            var listResult = objectResult.Value as List<BarSimpleDto>;
            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            mockUnitOfWork.BarRepository.Received(1).GetXBars(fromIndex, length);
            Assert.That(listResult[0].BarName, Is.EqualTo(correctResultList[0].BarName));
            Assert.That(listResult.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetRangeOfBars_ListIsEmpty()
        {
            // Arrange
            var fromIndex = 0;
            var length = 1;
            mockUnitOfWork.BarRepository.GetXBars(fromIndex, length)
                .Returns(new List<Bar>());

            // Act
            var result = uut.GetRangeOfBars(fromIndex, length);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
            mockUnitOfWork.BarRepository.Received(1).GetXBars(fromIndex, length);
        }
    }
}