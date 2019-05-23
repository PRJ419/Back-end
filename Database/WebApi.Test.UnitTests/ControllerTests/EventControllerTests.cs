using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Database;
using Database.Entities;
using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.BarEvent;

namespace WebApi.Test.UnitTest.ControllerTests
{
    [TestFixture]
    public class EventControllerTests
    {

        private EventController uut;
        private IUnitOfWork mockUnitOfWork;
        private IMapper mapper;
        private List<BarEvent> defaultList;
        private BarEvent defaultEvent;
        private BarEventDto defaultEventDto;
        private List<BarEventDto> correctResultList;


        [SetUp]
        public void Setup()
        {
            mockUnitOfWork = Substitute.For<IUnitOfWork>();

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);

            uut = new EventController(mockUnitOfWork, mapper);

            defaultList = new List<BarEvent>()
            {
                new BarEvent()
                {
                    BarName = "TestBar",
                    EventName = "Høst Fest",
                    Image = "fest.jpg",
                    Date = DateTime.Now,
                    Bar = null,
                },
                new BarEvent()
                {
                    BarName = "TestBar",
                    EventName = "Fadøl til en 10'er",
                    Image = "fadøl.jpg",
                    Date = DateTime.Now,
                    Bar = null,
                }
            };
            defaultEvent = defaultList[0];

            // Direct conversion without navigational property
            correctResultList = new List<BarEventDto>()
            {
                new BarEventDto()
                {
                    BarName = "TestBar",
                    EventName = "Høst Fest",
                    Image = "fest.jpg",
                    Date = DateTime.Now,
                },
                new BarEventDto()
                {
                    BarName = "TestBar",
                    EventName = "Fadøl til en 10'er",
                    Image = "fadøl.jpg",
                    Date = DateTime.Now,
                }
            };
            defaultEventDto = correctResultList[0];
        }


        [Test]
        public void GetEvents_UnitOfWorkReturnsList_UutReturnsCorrectType()
        {
            string parameter = "TestBar";

                                
            mockUnitOfWork.BarEventRepository
                .Find(Arg.Any<Expression<Func<BarEvent, bool>>>())
                .Returns(defaultList);

            var result = uut.GetEvents(parameter);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetEvents_UnitOfWorkReturnsEmptyList_UutReturnsCorrectType()
        {
            mockUnitOfWork.BarEventRepository
                .Find(Arg.Any<Expression<Func<BarEvent, bool>>>())
                .Returns(new List<BarEvent>());

            var result = uut.GetEvents("BadString");
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void GetEvents_UnitOfWorkReturnsList_UutReturnsCorrectDtoList()
        {
            string parameter = "TestBar";

            mockUnitOfWork.BarEventRepository.Find(Arg.Any<Expression<Func<BarEvent, bool>>>())
                .Returns(defaultList);

            var objectResult = uut.GetEvents(parameter);
            var result = (objectResult as OkObjectResult).Value as List<BarEventDto>;

            Assert.That(result[0].BarName, Is.EqualTo(defaultList[0].BarName));
            Assert.That(result[0].EventName, Is.EqualTo(defaultList[0].EventName));
            Assert.That(result[0].Image, Is.EqualTo(defaultList[0].Image));
            Assert.That(result[0].Date, Is.EqualTo(defaultList[0].Date));

            Assert.That(result[1].BarName, Is.EqualTo(defaultList[1].BarName));
            Assert.That(result[1].EventName, Is.EqualTo(defaultList[1].EventName));
            Assert.That(result[1].Image, Is.EqualTo(defaultList[1].Image));
            Assert.That(result[1].Date, Is.EqualTo(defaultList[1].Date));
        }

        [Test]
        public void AddEvent_UnitOfWorkAcceptsModel_UutReturnsCreatedWithRouteAndObject()
        {
            var barEventDto = mapper.Map<BarEventDto>(defaultEvent);
            var result = uut.AddEvent(barEventDto);
            var resultObj = result as CreatedResult;

            Assert.That(result, Is.TypeOf<CreatedResult>());
            Assert.That(resultObj.Location, Is.EqualTo($"api/bars/{barEventDto.BarName}/events"));
            Assert.That(resultObj.Value, Is.TypeOf<BarEventDto>());
        }

        [Test]
        public void AddEvent_UnitOfWorkThrowsException_ReturnsBadRequest()
        {
            mockUnitOfWork.BarEventRepository
                .When(repo => repo.Add(Arg.Any<BarEvent>()))
                .Do(x => throw new Exception());

            var result = uut.AddEvent(defaultEventDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void DeleteEvent_UnitOfWorkDoesntThrow_Success()
        {
            var result = uut.DeleteEvent(defaultEventDto.EventName,
                defaultEventDto.BarName);
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void DeleteEvent_UnitOfWorkThrowsException_NotDeleted()
        {
            string barName = defaultEvent.BarName;
            string eventName = defaultEvent.EventName;
            mockUnitOfWork.BarEventRepository
                .When(repo =>
                {
                    repo.Delete((new object[] { barName, eventName }));
                })
                .Do(x => throw new Exception());

            var result = uut.DeleteEvent(eventName, barName);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void EditEvent_CorrectInput_ReturnsCreated()
        {
            var result = uut.EditEvent(defaultEventDto);
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public void EditEvent_CorrectInput_ReturnsRouteAndObject()
        {
            var result = uut.EditEvent(defaultEventDto);
            var resultObj = (result as CreatedResult);
            Assert.That(resultObj.Location, Is.EqualTo($"api/bars/{defaultEventDto.BarName}/events"));
            Assert.That(resultObj.Value, Is.TypeOf<BarEventDto>());
        }

        [Test]
        public void EditEvent_CorrectInput_ReturnsEquivalentObject()
        {
            var result = uut.EditEvent(defaultEventDto);
            var resultObj = (result as CreatedResult).Value as BarEventDto;

            Assert.That(resultObj.BarName,      Is.EqualTo(defaultEventDto.BarName));
            Assert.That(resultObj.EventName,    Is.EqualTo(defaultEventDto.EventName));
            Assert.That(resultObj.Image,        Is.EqualTo(defaultEventDto.Image));
            Assert.That(resultObj.Date,         Is.EqualTo(defaultEventDto.Date));
        }

        [Test]
        public void EditEvent_BadInput_ReturnsBadRequest()
        {
            mockUnitOfWork.BarEventRepository
                .When(repo => repo.Edit(Arg.Any<BarEvent>()))
                .Do(x => throw new Exception());
            var result = uut.EditEvent(defaultEventDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

    }
}