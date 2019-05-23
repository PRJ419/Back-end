using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Database;
using Database.Entities;
using Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.DTOs.AutoMapping;
using WebApi.DTOs.BarRepresentative;
using WebApi.DTOs.ReviewDto;

namespace WebApi.Test.UnitTest.ControllerTests
{
    [TestFixture]
    class ReviewControllerTests
    {
        private ReviewController uut;
        private IUnitOfWork mockUnitOfWork;
        private IMapper mapper;
        private List<Review> defaultList;
        private Review defaultReview;
        private ReviewDto defaultReviewDto;
        private List<ReviewDto> correctResultList;


        [SetUp]
        public void Setup()
        {
            mockUnitOfWork = Substitute.For<IUnitOfWork>();

            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);

            uut = new ReviewController(mockUnitOfWork, mapper);

            defaultList = new List<Review>()
            {
                new Review()
                {
                    BarName = "TestBar",
                    BarPressure = 5,
                    Username = "brugernavn", 
                    Bar = null,
                    Customer = null,
                },
                new Review()
                {
                    BarName = "TestBar",
                    BarPressure = 3,
                    Username = "brugernavn2",
                    Bar = null,
                    Customer = null,
                }
            };
            defaultReview = defaultList[0];

            // Direct conversion without navigational property
            correctResultList = new List<ReviewDto>()
            {
                new ReviewDto()
                {
                    BarName = "TestBar",
                    BarPressure = 5,
                    Username = "brugernavn",
                },
                new ReviewDto()
                {
                    BarName = "TestBar",
                    BarPressure = 5,
                    Username = "brugernavn2",
                }
            };
            defaultReviewDto = correctResultList[0];
        }

        [Test]
        public void GetReviews_UnitOfWorkReturnsReviews_UutReturnsCorrectType()
        {
            mockUnitOfWork.ReviewRepository
                .Find(Arg.Any<Expression<Func<Review, bool>>>())
                .Returns(defaultList);

            var result = uut.GetReviews(defaultReviewDto.BarName);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetReviews_UnitOfWorkReturnsEmptyList_UutReturnsCorrectType()
        {
            mockUnitOfWork.ReviewRepository
                .Find(Arg.Any<Expression<Func<Review, bool>>>())
                .Returns(new List<Review>());

            var result = uut.GetReviews(defaultReviewDto.BarName);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
        [Test]
        public void GetReviews_UnitOfWorkReturnsReviews_UutReturnsCorrectList()
        {
            mockUnitOfWork.ReviewRepository
                .Find(Arg.Any<Expression<Func<Review, bool>>>())
                .Returns(defaultList);

            var result = uut.GetReviews(defaultReviewDto.BarName);
            var resultObj = (result as OkObjectResult).Value as List<ReviewDto>;
            Assert.That(resultObj[0].BarName, Is.EqualTo(correctResultList[0].BarName));
            Assert.That(resultObj[0].BarPressure, Is.EqualTo(correctResultList[0].BarPressure));
            Assert.That(resultObj[0].Username, Is.EqualTo(correctResultList[0].Username));
        }

        [Test]
        public void GetUserReview_UnitOfWorkReturnsReview_UutReturnsCorrectType()
        {
            var key = new object[] { defaultReviewDto.BarName, defaultReviewDto.Username};
            mockUnitOfWork.ReviewRepository.Get(key)
                .Returns(defaultReview);

            var result = uut.GetUserReview(defaultReviewDto.Username, defaultReviewDto.BarName);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void GetUserReview_UnitOfWorkReturnsNull_UutReturnsCorrectType()
        {
            var key = new object[] { defaultReviewDto.BarName, defaultReviewDto.Username };
            mockUnitOfWork.ReviewRepository.Get(key)
                .ReturnsNull();

            var result = uut.GetUserReview(defaultReviewDto.Username, defaultReviewDto.BarName);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void GetUserReview_UnitOfWorkReturnsReview_UutReturnsCorrectProperties()
        {
            var key = new object[] { defaultReviewDto.BarName, defaultReviewDto.Username };
            mockUnitOfWork.ReviewRepository.Get(key)
                .Returns(defaultReview);

            var result = uut.GetUserReview(defaultReviewDto.Username, defaultReviewDto.BarName);
            var resultObj = (result as OkObjectResult).Value as ReviewDto;

            Assert.That(resultObj.BarName, Is.EqualTo(defaultReviewDto.BarName));
            Assert.That(resultObj.BarPressure, Is.EqualTo(defaultReviewDto.BarPressure));
            Assert.That(resultObj.Username, Is.EqualTo(defaultReviewDto.Username));
        }

        [Test]
        public void EditReview_CorrectInput_ReturnsCreated()
        {
            var result = uut.EditUserReview(defaultReviewDto);
            Assert.That(result, Is.TypeOf<CreatedResult>());
        }

        [Test]
        public void EditReview_CorrectInput_ReturnsRouteAndObject()
        {
            var result = uut.EditUserReview(defaultReviewDto);
            var resultObj = (result as CreatedResult);

            Assert.That(resultObj.Location,
                Is.EqualTo($"api/bars/{defaultReviewDto.BarName}/reviews/{defaultReviewDto.Username}"));
            Assert.That(resultObj.Value, Is.TypeOf<ReviewDto>());
        }

        [Test]
        public void EditReview_CorrectInput_ReturnsEquivalentObject()
        {
            var result = uut.EditUserReview(defaultReviewDto);
            var resultObj = (result as CreatedResult).Value as ReviewDto;

            Assert.That(resultObj.BarName, Is.EqualTo(defaultReviewDto.BarName));
            Assert.That(resultObj.BarPressure, Is.EqualTo(defaultReviewDto.BarPressure));
            Assert.That(resultObj.Username, Is.EqualTo(defaultReviewDto.Username));
        }

        [Test]
        public void EditReview_BadInput_ReturnsBadRequest()
        {
            mockUnitOfWork.ReviewRepository
                .When(repo => repo.Edit(Arg.Any<Review>()))
                .Do(x => throw new Exception());
            var result = uut.EditUserReview(defaultReviewDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void EditReview_CorrectInput_AllCorrectCalls()
        {
            var result = uut.EditUserReview(defaultReviewDto);

            // Nsubstitute assertions
            mockUnitOfWork.ReviewRepository.Received(1).Edit(Arg.Any<Review>());
            mockUnitOfWork.Received(1).UpdateBarRating(defaultReviewDto.BarName);
            mockUnitOfWork.Received(2).Complete();
        }

        [Test]
        public void AddUserReview_CorrectInput_AllCorrectCalls()
        {
            var result = uut.AddUserReview(defaultReviewDto);

            // Nsubstitute assertions
            mockUnitOfWork.ReviewRepository.Received(1).Add(Arg.Any<Review>());
            mockUnitOfWork.Received(1).UpdateBarRating(defaultReviewDto.BarName);
            mockUnitOfWork.Received(2).Complete();
        }

        [Test]
        public void AddUserReview_UnitOfWorkAcceptsModel_UutReturnsCreatedWithRouteAndObject()
        {
            var result = uut.AddUserReview(defaultReviewDto);
            var resultObj = result as CreatedResult;

            Assert.That(result, Is.TypeOf<CreatedResult>());
            Assert.That(resultObj.Location, Is.EqualTo($"api/bars/{defaultReviewDto.BarName}/reviews/{defaultReviewDto.Username}"));
            Assert.That(resultObj.Value, Is.TypeOf<ReviewDto>());
        }

        [Test]
        public void AddUserReview_UnitOfWorkThrowsException_ReturnsBadRequest()
        {
            mockUnitOfWork.ReviewRepository
                .When(repo => repo.Add(Arg.Any<Review>()))
                .Do(x => throw new Exception());

            var result = uut.AddUserReview(defaultReviewDto);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void DeleteUserReview_UnitOfWorkDoesntThrow_ReturnsCorrectType()
        {
            var result = uut.DeleteUserReview(defaultReviewDto.BarName, defaultReviewDto.Username);
            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void DeleteUserReview_UnitOfWorkDoesntThrow_UnitOfWorkReceivesCorrectCalls()
        {
            var result = uut.DeleteUserReview(defaultReviewDto.BarName, defaultReviewDto.Username);
            mockUnitOfWork.ReviewRepository.Received(1)
                .Delete(new object[] {defaultReviewDto.BarName, defaultReviewDto.Username});
            mockUnitOfWork.Received(1).UpdateBarRating(defaultReviewDto.BarName);
            mockUnitOfWork.Received(2).Complete();
        }

        [Test]
        public void DeleteUserReview_UnitOfWorkThrowsException_ReturnsCorrectType()
        {
            var key = new object[] {defaultReviewDto.BarName, defaultReviewDto.Username};
            mockUnitOfWork.ReviewRepository
                .When(repo => { repo.Delete(key); })
                .Do(x => throw new Exception());

            var result = uut.DeleteUserReview(defaultReviewDto.BarName, defaultReviewDto.Username);
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }



    }
}
