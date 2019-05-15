using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.BarRepresentative;

namespace WebApi.Test.UnitTest.ControllerTests
{
    [TestFixture]
    public class BarRepresentativeControllerTests
    {
        private BarRepresentativeController uut;
        private IUnitOfWork mockUnitOfWork;
        private IMapper mapper;
        private List<BarRepresentative> defaultList;
        private BarRepresentative defaultBarRep;
        private BarRepresentativeDto defaultBarRepDto;
        private List<BarRepresentativeDto> correctResultList;


        [SetUp]
        public void Setup()
        {
            mockUnitOfWork = Substitute.For<IUnitOfWork>();

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);

            uut = new BarRepresentativeController(mockUnitOfWork, mapper);

            defaultList = new List<BarRepresentative>()
            {
                new BarRepresentative()
                {
                    BarName = "TestBar",
                    Name = "Navn1",
                    Username = "BarRep1",
                    Bar = null,
                },
                new BarRepresentative()
                {
                    BarName = "TestBar2",
                    Name = "Navn2",
                    Username = "BarRep2",
                    Bar = null,
                }
            };
            defaultBarRep = defaultList[0];

            // Direct conversion without navigational property
            correctResultList = new List<BarRepresentativeDto>()
            {
                new BarRepresentativeDto()
                {
                    BarName = "TestBar",
                    Name = "Navn1",
                    Username = "BarRep1",
                },
                new BarRepresentativeDto()
                {
                    BarName = "TestBar2",
                    Name = "Navn2",
                    Username = "BarRep2",
                }
            };
            defaultBarRepDto = correctResultList[0];
        }


        [Test]
        public void GetBarRepresentatives_UnitOfWorkReturnsList_UutReturnsCorrectType()
        {
            mockUnitOfWork.BarRepRepository.GetAll().Returns(defaultList);

            var result = uut.GetBarRepresentatives();
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetBarRepresentatives_UnitOfWorkReturnsEmptyList_UutReturnsCorrectType()
        {
            mockUnitOfWork.BarRepRepository.GetAll();

            var result = uut.GetBarRepresentatives();
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void GetBarRepresentatives_UnitOfWorkReturnsList_UutReturnsCorrectDtoList()
        {
            mockUnitOfWork.BarRepRepository.GetAll()
                .Returns(defaultList);

            var objectResult = uut.GetBarRepresentatives();
            var result = (objectResult as OkObjectResult).Value as List<BarRepresentativeDto>;

            Assert.That(result[0].BarName, Is.EqualTo(defaultList[0].BarName));
            Assert.That(result[0].Name, Is.EqualTo(defaultList[0].Name));
            Assert.That(result[0].Username, Is.EqualTo(defaultList[0].Username));

            Assert.That(result[1].BarName, Is.EqualTo(defaultList[1].BarName));
            Assert.That(result[1].Name, Is.EqualTo(defaultList[1].Name));
            Assert.That(result[1].Username, Is.EqualTo(defaultList[1].Username));
        }

        [Test]
        public void GetBarRepresentative_UnitOfWorkReturnsBarRep_UutReturnsCorrectType()
        {
            var key = defaultBarRepDto.Username;
            mockUnitOfWork.BarRepRepository.Get(key)
                .Returns(defaultBarRep);

            var result = uut.GetBarRepresentative(defaultBarRepDto.Username);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetBarRepresentative_UnitOfWorkReturnsBarRep_UutReturnsCorrectBarRepresentativeDto()
        {
            var key = defaultBarRep.Username;
            mockUnitOfWork.BarRepRepository.Get(key)
                .Returns(defaultBarRep);

            var result = uut.GetBarRepresentative(defaultBarRepDto.Username);
            var resultObj = (result as OkObjectResult).Value as BarRepresentativeDto;
            Assert.That(resultObj.BarName, Is.EqualTo(defaultBarRep.BarName));
            Assert.That(resultObj.Username, Is.EqualTo(defaultBarRep.Username));
            Assert.That(resultObj.Name, Is.EqualTo(defaultBarRep.Name));
        }

        [Test]
        public void GetBarRepresentative_UnitOfWorkReturnsNull_UutReturnsBadRequest()
        {
            var key = "This key cannot be found";
            mockUnitOfWork.BarRepRepository.Get(key)
                .ReturnsNull();

            var result = uut.GetBarRepresentative(key);

            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void AddBarRepresentative_UnitOfWorkAcceptsModel_UutReturnsCreatedWithRouteAndObject()
        {
            var result = uut.AddBarRepresentative(defaultBarRepDto);
            var resultObj = result as CreatedResult;

            Assert.That(result, Is.TypeOf<CreatedResult>());
            Assert.That(resultObj.Location, Is.EqualTo($"api/BarRepresentative/{defaultBarRepDto.Username}"));
            Assert.That(resultObj.Value, Is.TypeOf<BarRepresentativeDto>());
        }

        [Test]
        public void AddBarRepresentative_UnitOfWorkThrowsException_ReturnsBadRequest()
        {
            mockUnitOfWork.BarRepRepository
                .When(repo => repo.Add(Arg.Any<BarRepresentative>()))
                .Do(x => throw new Exception());

            var result = uut.AddBarRepresentative(defaultBarRepDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void DeleteBarRepresentative_UnitOfWorkDoesntThrow_Success()
        {
            var result = uut.DeleteBarRepresentative(defaultBarRepDto.Username);
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void DeleteBarRepresentative_UnitOfWorkThrowsException_NotDeleted()
        {
            var key = defaultBarRepDto.Username;
            mockUnitOfWork.BarRepRepository
                .When(repo => { repo.Delete(key); })
                .Do(x => throw new Exception());

            var result = uut.DeleteBarRepresentative(key);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void EditBarRepresentative_CorrectInput_ReturnsCreated()
        {
            var result = uut.EditBarRepresentative(defaultBarRepDto);
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public void EditBarRepresentative_CorrectInput_ReturnsRouteAndObject()
        {
            var result = uut.EditBarRepresentative(defaultBarRepDto);
            var resultObj = (result as CreatedResult);
            Assert.That(resultObj.Location,
                Is.EqualTo($"api/BarRepresentative/{defaultBarRepDto.Username}"));
            Assert.That(resultObj.Value, Is.TypeOf<BarRepresentativeDto>());
        }

        [Test]
        public void EditBarRepresentative_CorrectInput_ReturnsEquivalentObject()
        {
            var result = uut.EditBarRepresentative(defaultBarRepDto);
            var resultObj = (result as CreatedResult).Value as BarRepresentativeDto;

            Assert.That(resultObj.BarName, Is.EqualTo(defaultBarRep.BarName));
            Assert.That(resultObj.Name, Is.EqualTo(defaultBarRep.Name));
            Assert.That(resultObj.Username, Is.EqualTo(defaultBarRep.Username));
        }

        [Test]
        public void EditBarRepresentative_BadInput_ReturnsBadRequest()
        {
            mockUnitOfWork.BarRepRepository
                .When(repo => repo.Edit(Arg.Any<BarRepresentative>()))
                .Do(x => throw new Exception());
            var result = uut.EditBarRepresentative(defaultBarRepDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }
    }
}