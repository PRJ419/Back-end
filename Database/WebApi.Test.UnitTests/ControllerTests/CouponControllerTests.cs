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
using WebApi.DTOs.Coupon;

namespace WebApi.Test.UnitTest.ControllerTests
{
    [TestFixture]
    public class CouponControllerTests
    {

        private CouponController uut;
        private IUnitOfWork mockUnitOfWork;
        private IMapper mapper;
        private List<Coupon> defaultList;
        private Coupon defaultCoupon;
        private CouponDto defaultCouponDto;
        private List<CouponDto> correctResultList;

        [SetUp]
        public void Setup()
        {
            mockUnitOfWork = Substitute.For<IUnitOfWork>();

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);

            uut = new CouponController(mockUnitOfWork, mapper);

            defaultList = new List<Coupon>()
            {
                new Coupon()
                {
                    BarName = "TestBar",
                    CouponID = "10 Kr af alt",
                    ExpirationDate = DateTime.Now,
                    Bar = null, 
                },
                new Coupon()
                {
                    BarName = "TestBar",
                    CouponID = "5 Kr af alt",
                    ExpirationDate = DateTime.Now,
                    Bar = null,
                }
            };
            defaultCoupon = defaultList[0];

            // Direct conversion without navigational property
            correctResultList = new List<CouponDto>()
            {
                new CouponDto()
                {
                    BarName = "TestBar",
                    CouponID = "10 Kr af alt",
                    ExpirationDate = DateTime.Now,
                },
                new CouponDto()
                {
                    BarName = "TestBar",
                    CouponID = "5 Kr af alt",
                    ExpirationDate = DateTime.Now,
                }
            };
            defaultCouponDto = correctResultList[0];
        }


        [Test]
        public void GetCoupons_UnitOfWorkReturnsList_UutReturnsCorrectType()
        {
            string parameter = "TestBar";


            mockUnitOfWork.CouponRepository
                .Find(Arg.Any<Expression<Func<Coupon, bool>>>())
                .Returns(defaultList);

            var result = uut.GetCoupons(parameter);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetCoupons_UnitOfWorkReturnsEmptyList_UutReturnsCorrectType()
        {
            mockUnitOfWork.CouponRepository
                .Find(Arg.Any<Expression<Func<Coupon, bool>>>())
                .Returns(new List<Coupon>());

            var result = uut.GetCoupons("BadString");
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void GetCoupons_UnitOfWorkReturnsList_UutReturnsCorrectDtoList()
        {
            string parameter = "TestBar";

            mockUnitOfWork.CouponRepository.Find(Arg.Any<Expression<Func<Coupon, bool>>>())
                .Returns(defaultList);

            var objectResult = uut.GetCoupons(parameter);
            var result = (objectResult as OkObjectResult).Value as List<CouponDto>;

            Assert.That(result[0].BarName, Is.EqualTo(defaultList[0].BarName));
            Assert.That(result[0].CouponID, Is.EqualTo(defaultList[0].CouponID));
            Assert.That(result[0].ExpirationDate, Is.EqualTo(defaultList[0].ExpirationDate));

            Assert.That(result[1].BarName, Is.EqualTo(defaultList[1].BarName));
            Assert.That(result[1].CouponID, Is.EqualTo(defaultList[1].CouponID));
            Assert.That(result[1].ExpirationDate, Is.EqualTo(defaultList[1].ExpirationDate));
        }

        [Test]
        public void GetCoupon_UnitOfWorkReturnsCoupon_UutReturnsCorrectType()
        {
            var key = new object[] {defaultCoupon.BarName, defaultCoupon.CouponID};
            mockUnitOfWork.CouponRepository.Get(key)
                .Returns(defaultCoupon);

            var result = uut.GetCoupon(defaultCouponDto.CouponID,
                defaultCouponDto.BarName);
            Assert.That(result,Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetCoupon_UnitOfWorkReturnsCoupon_UutReturnsCorrectCouponDto()
        {
            var key = new object[] { defaultCoupon.BarName, defaultCoupon.CouponID };
            mockUnitOfWork.CouponRepository.Get(key)
                .Returns(defaultCoupon);

            var result = uut.GetCoupon(defaultCouponDto.CouponID,
                defaultCouponDto.BarName);
            var resultObj = (result as OkObjectResult).Value as CouponDto;
            Assert.That(resultObj.BarName, Is.EqualTo(defaultCoupon.BarName));
            Assert.That(resultObj.CouponID, Is.EqualTo(defaultCoupon.CouponID));
            Assert.That(resultObj.ExpirationDate, Is.EqualTo(defaultCoupon.ExpirationDate));
        }

        [Test]
        public void GetCoupon_UnitOfWorkReturnsNull_UutReturnsBadRequest()
        {
            var key = new object[] { defaultCoupon.CouponID, defaultCoupon.BarName };
            mockUnitOfWork.CouponRepository.Get(key)
                .ReturnsNull();

            var result = uut.GetCoupon(defaultCouponDto.CouponID,
                defaultCouponDto.BarName);
            
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void AddCoupon_UnitOfWorkAcceptsModel_UutReturnsCreatedWithRouteAndObject()
        {
            var result = uut.AddCoupon(defaultCouponDto);
            var resultObj = result as CreatedResult;

            Assert.That(result, Is.TypeOf<CreatedResult>());
            Assert.That(resultObj.Location, Is.EqualTo($"api/bars/{defaultCouponDto.BarName}/coupons/{defaultCouponDto.CouponID}"));
            Assert.That(resultObj.Value, Is.TypeOf<CouponDto>());
        }

        [Test]
        public void AddCoupon_UnitOfWorkThrowsException_ReturnsBadRequest()
        {
            mockUnitOfWork.CouponRepository
                .When(repo => repo.Add(Arg.Any<Coupon>()))
                .Do(x => throw new Exception());

            var result = uut.AddCoupon(defaultCouponDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void DeleteCoupon_UnitOfWorkDoesntThrow_Success()
        {
            var result = uut.DeleteCoupon(defaultCouponDto.CouponID,
                 defaultCouponDto.BarName);
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void DeleteCoupon_UnitOfWorkThrowsException_NotDeleted()
        {
            string barName = defaultCoupon.BarName;
            string couponId = defaultCoupon.CouponID;
            mockUnitOfWork.CouponRepository
                .When(repo =>
                {
                    repo.Delete((new object[] { couponId, barName }));
                })
                .Do(x => throw new Exception());

            var result = uut.DeleteCoupon(barName, couponId);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void EditCoupon_CorrectInput_ReturnsCreated()
        {
            var result = uut.EditCoupon(defaultCouponDto);
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public void EditCoupon_CorrectInput_ReturnsRouteAndObject()
        {
            var result = uut.EditCoupon(defaultCouponDto);
            var resultObj = (result as CreatedResult);
            Assert.That(resultObj.Location, 
                Is.EqualTo($"api/bars/{defaultCouponDto.BarName}/coupons/{defaultCouponDto.CouponID}"));
            Assert.That(resultObj.Value, Is.TypeOf<CouponDto>());
        }

        [Test]
        public void EditCoupon_CorrectInput_ReturnsEquivalentObject()
        {
            var result = uut.EditCoupon(defaultCouponDto);
            var resultObj = (result as CreatedResult).Value as  CouponDto;

            Assert.That(resultObj.BarName,          Is.EqualTo(defaultCouponDto.BarName));
            Assert.That(resultObj.CouponID,         Is.EqualTo(defaultCouponDto.CouponID));
            Assert.That(resultObj.ExpirationDate,   Is.EqualTo(defaultCouponDto.ExpirationDate));
        }

        [Test]
        public void EditCoupon_BadInput_ReturnsBadRequest()
        {
            mockUnitOfWork.CouponRepository
                .When(repo => repo.Edit(Arg.Any<Coupon>()))
                .Do(x => throw new Exception());
            var result = uut.EditCoupon(defaultCouponDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }
    }
}