using System;
using System.Collections.Generic;
using AutoMapper;
using Database;
using Database.Interfaces;
using NSubstitute;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.BarRepresentative;
using WebApi.DTOs.Coupon;

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


    }
}